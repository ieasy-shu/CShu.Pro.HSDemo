using System;
using System.Text;
using System.Collections.Generic;
using UFSoft.UBF.Business;
using UFSoft.UBF.PL;
using UFIDA.U9.Base.UserRole;
using UFIDA.U9.Base;
using UFIDA.U9.PM.PO;
using System.Collections;
using UFSoft.UBF.Util.Log;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using UFIDA.U9.PR.PurchaseRequest;

namespace YY.U9.Cust.ZS.AppPlugIn
{
    /// <summary>
    /// 采购订单提交
    /// </summary>
    [UFSoft.UBF.Eventing.Configuration.Failfast]
    class POSubscriber : UFSoft.UBF.Eventing.IEventSubscriber
    {
        private static readonly ILogger logger = LoggerManager.GetLogger(typeof(POSubscriber));
        public void Notify(params object[] args)
        {
            #region 从事件参数中取得当前业务实体

            //从事件参数中取得当前业务实体
            if (args == null || args.Length == 0 || !(args[0] is UFSoft.UBF.Business.EntityEvent))
                return;
            BusinessEntity.EntityKey key = ((UFSoft.UBF.Business.EntityEvent)args[0]).EntityKey;
            if (key == null)
            {
                return;
            }
            PurchaseOrder po = key.GetEntity() as PurchaseOrder;
            if (po == null)
            {
                return;
            }

            if (po.Status == PODOCStatusEnum.Approving && po.OriginalData.Status == PODOCStatusEnum.Opened)
            {
                //是否走OA接口
                string profileValue = Common.GetProfileValue(Common.iProfileCode, Context.LoginOrg.ID);
                if (profileValue.ToLower() == "true")
                {
                    #region 根据描述是否推送到OA

                    string docTypeDesc = po.DocumentType.Description;
                    if (string.IsNullOrEmpty(docTypeDesc))
                    {
                        return;
                    }
                    string returnMsg = string.Empty;
                    string businessType = "OA流程创建";
                    string requestName = "采购订单" + po.DocNo + "审批";
                    User user = User.Finder.FindByID(Context.LoginUserID);
                    string userID = user.Code;
                    string workflowId = po.DocumentType.DescFlexField.PrivateDescSeg1;
                    bool isPO = true;
                    if (po.TC.Code.ToUpper() != "C001")
                    {
                        workflowId = po.DocumentType.DescFlexField.PrivateDescSeg2;
                    }
                    string oaRquestID = po.DescFlexField.PrivateDescSeg30;
                    POLine.EntityList polines = po.POLines;

                    
                    
                    if (string.IsNullOrEmpty(oaRquestID))
                    {
                        string url = Common.GetProfileValue(Common.csProfileCode, Context.LoginOrg.ID);
                        string workflowRequestInfo = Common.CreateXmlString(requestName, userID, workflowId, isPO, polines, null);
                        WeaverOAService.WorkflowServiceXml client = new WeaverOAService.WorkflowServiceXml();
                        client.Url = url;
                        returnMsg = client.doCreateWorkflowRequest(workflowRequestInfo, userID);
                    }
                    else
                    {
                        businessType = "OA流程更新";
                        string url = Common.GetProfileValue(Common.msProfileCode, Context.LoginOrg.ID);
                        string workflowRequestInfo = Common.CreateXmlStringForUpdate(oaRquestID, userID, workflowId, isPO, polines, null);
                        WeaverOAForUpadteService.WorkflowServiceforUpadte client = new WeaverOAForUpadteService.WorkflowServiceforUpadte();
                        client.Url = url;
                        bool flag = client.RequestUpdate(workflowRequestInfo);
                        if (flag == false)
                        {
                            returnMsg = "-1";
                        }
                        else
                        {
                            returnMsg = oaRquestID;
                        }
                    }
                    if (string.IsNullOrEmpty(returnMsg) || returnMsg.StartsWith("-"))
                    {
                        string msg = businessType + "失败，请联系系统管理员！OA返回值:" + returnMsg;
                        throw new Exception(msg);
                    }
                    if (string.IsNullOrEmpty(oaRquestID))
                    {
                        po.DescFlexField.PrivateDescSeg30 = returnMsg;
                    }

                    #endregion
                }
                //是否走易派客接口
                string eprcProfileValue = Common.GetProfileValue("ZS006", Context.LoginOrg.ID);
                if (eprcProfileValue.ToLower() == "true")
                {
                    #region 回写易派客订单状态

                    //根据来源单据号取得请购单数据
                    List<long> list = new List<long>();
                    foreach (POLine line in po.POLines)
                    {
                        if (line.SrcDocInfo != null && line.SrcDocInfo.SrcDoc != null)
                        {
                            long srcDoc = line.SrcDocInfo.SrcDoc.EntityID;
                            if (list.Contains(srcDoc) == false)
                            {
                                list.Add(srcDoc);
                            }
                        }
                    }
                    foreach (long srcDoc in list)
                    {
                        PR pr = PR.Finder.FindByID(srcDoc);
                        string orderId = pr.DescFlexField.PrivateDescSeg25;
                        string purchasecompanyid = pr.DescFlexField.PrivateDescSeg26;
                        string orderStatus = "13";
                        string returnCheckTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        if(string.IsNullOrEmpty(orderId))
                        {
                            continue;
                        }
                        string epecUrl = Common.GetProfileValue("ZS007", Context.LoginOrg.ID);
                        string corpCode = Common.GetProfileValue("ZS011", Context.LoginOrg.ID);//公司代码
                        string accessToken = GetAccessToken(epecUrl);

                        StringBuilder urlBuilder = new StringBuilder(epecUrl);
                        urlBuilder.Append("/apigate/v2/order/approve");
                        urlBuilder.Append("?access_token=").Append(accessToken);
                        urlBuilder.Append("&data=");
                        urlBuilder.Append("{\"corpcode\":\"").Append(corpCode).Append("\",");
                        urlBuilder.Append("\"orderId\":\"").Append(orderId).Append("\",");
                        urlBuilder.Append("\"purchaseCompanyId\":\"").Append(purchasecompanyid).Append("\",");
                        urlBuilder.Append("\"orderStatus\":\"").Append(orderStatus).Append("\",");
                        urlBuilder.Append("\"returnCheckTime\":\"").Append(returnCheckTime).Append("\"}");

                        string serviceAddress = urlBuilder.ToString();
                        logger.Error("获取消息请求地址:" + serviceAddress);

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream myResponseStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(myResponseStream, Encoding.UTF8);
                        string jsonString = reader.ReadToEnd();

                        logger.Error("请购请求返回值:" + jsonString);
                        //解析josn  
                        JObject jo = JObject.Parse(jsonString);
                        string epecFlag = jo["success"].ToString();
                        string rtnData = jo["data"].ToString();
                        if (epecFlag.ToLower() == "false")
                        {
                            if (string.IsNullOrEmpty(rtnData) || rtnData.ToLower() == "null")
                            {
                                string code = jo["code"].ToString();
                                string exMsg = jo["msg"].ToString();
                                throw new Exception("请求易派客系统发生错误：Code=" + code + ",Msg=" + exMsg);
                            }
                            jo = JObject.Parse(rtnData);
                            string isMatch = jo["ismatch"].ToString();
                            string remark = jo["remark"].ToString();
                            if (epecFlag.ToLower() == "false")
                            {
                                string exMsg = jo["msg"].ToString();
                                throw new Exception("请求易派客系统发生错误：" + exMsg);
                            }
                            if (isMatch == "0")
                            {
                                throw new Exception("传递数据到易派客系统发生错误：" + remark);
                            }
                        }
                    }

                    #endregion
                }

            }

            #endregion
        }

        /// <summary>
        /// 获取易派客AccessToken
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetAccessToken(string url)
        {
            string requestURL = url + "/apigate/oauth/";
            string clientID = Common.GetProfileValue("ZS008", Context.LoginOrg.ID);
            string clientSecret = Common.GetProfileValue("ZS009", Context.LoginOrg.ID);
            string grantType = "password";
            string companyID = Common.GetProfileValue("ZS010", Context.LoginOrg.ID);
            string corpCode = Common.GetProfileValue("ZS011", Context.LoginOrg.ID);//公司代码
            string userName = Common.GetProfileValue("ZS012", Context.LoginOrg.ID);
            string password = Common.GetProfileValue("ZS013", Context.LoginOrg.ID);

            string accessToken = Common.PostFunction(requestURL, clientID, clientSecret, grantType, companyID, userName, password);
            if (accessToken.IndexOf("\"") > -1)
            {
                accessToken = accessToken.Replace("\"", "");
            }
            return accessToken;
        }
    }
}
