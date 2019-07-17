using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFSoft.UBF.PL;
using UFIDA.U9.Base;
using UFIDA.U9.Base.FlexField.ValueSet;
using UFIDA.U9.PM.PO;
using UFIDA.U9.PM.POChange;
using UFIDA.U9.Base.Profile.Proxy;
using UFIDA.U9.Base.Profile;
using UFIDA.U9.CBO.SCM.ProjectTask;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using UFSoft.UBF.Util.Log;

namespace YY.U9.Cust.ZS.AppPlugIn
{
    class Common
    {
        private static readonly ILogger logger = LoggerManager.GetLogger(typeof(Common));
        //是否接口流程
        public static string iProfileCode = "ZS001";
        //创建OA流程服务地址
        public static string csProfileCode = "ZS002";
        //更新OA流程服务器地址
        public static string msProfileCode = "ZS003";
        //ERP地址
        public static string ipProfileCode = "ZS005";

        /// <summary>
        /// 获取参数设置
        /// </summary>
        /// <param name="profileCode"></param>
        /// <param name="org"></param>
        /// <returns></returns>
        public static string GetProfileValue(string profileCode, long org)
        {
            string profileValue = "";
            GetProfileValueProxy getProfileValueProxy = new GetProfileValueProxy();
            getProfileValueProxy.ProfileCode = profileCode;
            getProfileValueProxy.ProfileOrg = org;
            PVDTOData pVDTOData = new PVDTOData();
            pVDTOData = getProfileValueProxy.Do();
            if (pVDTOData != null)
            {
                if (string.IsNullOrEmpty(pVDTOData.ProfileValue) == false)
                {
                    profileValue = pVDTOData.ProfileValue;
                }
            }
            return profileValue;
        }

        /// <summary>
        /// 更新参数设置值
        /// </summary>
        /// <param name="profileCode"></param>
        /// <param name="pValue"></param>
        /// <param name="org"></param>
        public static void ModifyProfileValue(string profileCode, string pValue, long org)
        {
            ProfileValue profileValue = ProfileValue.Finder.Find("Profile.Code=@Code,", new OqlParam[] { new OqlParam(profileCode) });
            profileValue.Value = pValue;
        }

        /// <summary>
        /// 创建范围OA审批流Xml报文
        /// </summary>
        /// <param name="requestName"></param>
        /// <param name="userID"></param>
        /// <param name="workflowId"></param>
        /// <param name="isPO"></param>
        /// <param name="polines"></param>
        /// <param name="poModifyLines"></param>
        /// <returns></returns>
        public static string CreateXmlString(string requestName, string userID, string workflowId, bool isPO, POLine.EntityList polines, POModifyLine.EntityList poModifyLines)
        {
            string tableXml = CreateMainXmlString(userID, workflowId, polines);
            if(isPO==false)
            {
                tableXml = CreateMainXmlString(userID, workflowId, poModifyLines);
            }
            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.Append("<WorkflowRequestInfo>");
            xmlBuilder.Append("<requestName>").Append(requestName).Append("</requestName>");
            xmlBuilder.Append("<requestLevel>0</requestLevel>");
            xmlBuilder.Append("<workflowBaseInfo>");
            xmlBuilder.Append("<workflowId>").Append(workflowId).Append("</workflowId>");
            xmlBuilder.Append("</workflowBaseInfo>");
            xmlBuilder.Append("<creatorId>").Append(userID).Append("</creatorId>");//用户ID
            xmlBuilder.Append("<isnextflow>0</isnextflow>");//不默认提交流程
            xmlBuilder.Append("<canView>false</canView>");
            xmlBuilder.Append("<canEdit>false</canEdit>");
            xmlBuilder.Append("<mustInputRemark>false</mustInputRemark>");
            xmlBuilder.Append("<needAffirmance>false</needAffirmance>");
            xmlBuilder.Append(tableXml);
            xmlBuilder.Append("</WorkflowRequestInfo>");
            return xmlBuilder.ToString();
        }

        /// <summary>
        /// 采购订单OA审批流Xml
        /// </summary>
        /// <returns></returns>
        public static string CreateMainXmlString(string userID, string workflowId, POLine.EntityList polines)
        {
            StringBuilder xmlBuilder = new StringBuilder();
            Hashtable ht = new Hashtable();
            string orgCode = Context.LoginOrg.Code;
            string orgName = Context.LoginOrg.Name;
            string docTypeName = polines[0].PurchaseOrder.DocType.Name;
            string id = polines[0].PurchaseOrder.ID.ToString();
            string docNo = polines[0].PurchaseOrder.DocNo;
            string businessDate = polines[0].PurchaseOrder.BusinessDate.ToString("yyyy-MM-dd");
            string suppCode = polines[0].PurchaseOrder.Supplier.Code;
            string suppName = polines[0].PurchaseOrder.Supplier.Name.Trim();
            string suppShortName = polines[0].PurchaseOrder.Supplier.ShortName;
            string purOperCode = polines[0].PurchaseOrder.PurOper != null ? polines[0].PurchaseOrder.PurOper.Code : string.Empty;
            string purOperName = polines[0].PurchaseOrder.PurOper != null ? polines[0].PurchaseOrder.PurOper.Name : string.Empty;
            string purDeptCode = polines[0].PurchaseOrder.PurDept != null ? polines[0].PurchaseOrder.PurDept.Code : string.Empty;
            string purDeptName = polines[0].PurchaseOrder.PurDept != null ? polines[0].PurchaseOrder.PurDept.Name : string.Empty;
            string currencyName = polines[0].PurchaseOrder.TC.Name;
            string poTotalMnyTC = polines[0].PurchaseOrder.TC.MoneyRound.GetRoundValue(polines[0].PurchaseOrder.TotalMnyTC).ToString();
            string poTotalMnyFC = polines[0].PurchaseOrder.TC.MoneyRound.GetRoundValue(polines[0].PurchaseOrder.TotalMnyFC).ToString();
            //采购方式
            DefineValue dv = DefineValue.Finder.Find("ValueSetDef.Code='Z24' and Code=@Code", new OqlParam[] { new OqlParam(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg14) });
            string purWay = string.Empty;
            if (dv != null)
            {
                purWay = dv.Name;
            }
            string suppShortName1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg1;
            string suppShortName2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg9;
            string IsChange = "0";
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg17.ToLower() == "true")
            {
                IsChange = "1";
            }
            string isFixedContract = "0";
            string signingReason = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg13;
            string payWay = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg15;
            string beforeBargainTotalPrice1 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg2) == false)
            {
                beforeBargainTotalPrice1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg2;
            }
            string beforeBargainTotalPrice2 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg10) == false)
            {
                beforeBargainTotalPrice2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg10;
            }
            string beforeBargainTotalPrice3 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg21) == false)
            {
                beforeBargainTotalPrice3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg21;
            }

            string afterBargainTotalPrice1 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg3) == false)
            {
                afterBargainTotalPrice1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg3;
            }
            string afterBargainTotalPrice2 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg11) == false)
            {
                afterBargainTotalPrice2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg11;
            }
            string afterBargainTotalPrice3 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg22) == false)
            {
                afterBargainTotalPrice3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg22;
            }
            string afterBargainAccPrice1 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg4) == false)
            {
                afterBargainAccPrice1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg4;
            }
            string afterBargainAccPrice2 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg12) == false)
            {
                afterBargainAccPrice2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg12;
            }
            string afterBargainAccPrice3 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg23) == false)
            {
                afterBargainAccPrice3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg23;
            }
            string bargainDeliveryDate1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg6;
            string bargainDeliveryDate2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg18;
            string bargainDeliveryDate3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg25;
            string assignSupp1 = "否";
            string assignSupp2 = "否";
            string assignSupp3 = "否";
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg7.ToLower() == "true")
            {
                assignSupp1 = "是";
            }
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg19.ToLower() == "true")
            {
                assignSupp2 = "是";
            }
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg26.ToLower() == "true")
            {
                assignSupp3 = "是";
            }
            string manufacturer1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg5;
            string manufacturer2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg16;
            string manufacturer3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg24;
            string srcDocNo = polines[0].SrcDocInfo != null ? polines[0].SrcDocInfo.SrcDocNo : string.Empty;
            string projectCode = polines[0].Project != null ? polines[0].Project.Code : string.Empty;
            string projectName = polines[0].Project != null ? polines[0].Project.Name : string.Empty;
            string projectDesc = polines[0].Project != null ? polines[0].Project.Description : string.Empty;
            string headDeliveryDate = polines[0].POShiplines[0].DeliveryDate.ToString("yyyy-MM-dd");
            string projectGroup = polines[0].Project != null ? polines[0].Project.DescFlexField.PubDescSeg34 : string.Empty;
            string itemDesc = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg27;
            string headChangeState = "变更前";
            
            ht.Add("erpzzbm", orgCode);//组织编码
            ht.Add("erpzzmc", orgName);
            ht.Add("erpdjlx", docTypeName);
            ht.Add("cgddid", id);
            ht.Add("htbh", docNo);
            ht.Add("sqrq", businessDate);
            ht.Add("gysbm", suppCode);
            ht.Add("tjgf", suppName);
            ht.Add("gfjc1", suppShortName);
            ht.Add("sqr", purOperCode);
            ht.Add("bz2", currencyName);
            ht.Add("cjj", poTotalMnyTC);
            ht.Add("cjjrmb", poTotalMnyFC);
            ht.Add("cgfs", purWay);
            ht.Add("gfjc2", suppShortName1);
            ht.Add("gfjc3", suppShortName2);
            ht.Add("httksfbg", IsChange);
            ht.Add("sfgdzj", isFixedContract);
            ht.Add("qyly", signingReason);
            ht.Add("fkfs", payWay);
            ht.Add("yjqzj", beforeBargainTotalPrice1);
            ht.Add("yjqzj2", beforeBargainTotalPrice2);
            ht.Add("yjqzj3", beforeBargainTotalPrice3);
            ht.Add("yjhxhj", afterBargainTotalPrice1);
            ht.Add("yjhxhj2", afterBargainTotalPrice2);
            ht.Add("yjhxhj3", afterBargainTotalPrice3);
            ht.Add("yjhcdj", afterBargainAccPrice1);
            ht.Add("yjhcdj2", afterBargainAccPrice2);
            ht.Add("yjhcdj3", afterBargainAccPrice3);
            ht.Add("xjjq1", bargainDeliveryDate1);
            ht.Add("xjjq2", bargainDeliveryDate2);
            ht.Add("xjjq3", bargainDeliveryDate3);
            ht.Add("yzzdgf1", assignSupp1);
            ht.Add("yzzdgf2", assignSupp2);
            ht.Add("yzzdgf3", assignSupp3);
            ht.Add("sccplcj1", manufacturer1);
            ht.Add("sccplcj2", manufacturer2);
            ht.Add("sccplcj3", manufacturer3);
            ht.Add("qgdh", srcDocNo);
            ht.Add("jyhth", projectName);
            ht.Add("xmmc", projectDesc);
            ht.Add("htjhq", headDeliveryDate);
            ht.Add("xmfl", projectGroup);
            ht.Add("wlms", itemDesc);
            ht.Add("ChangeState", headChangeState);

            #region Head

            xmlBuilder.Append("<workflowMainTableInfo>");
            xmlBuilder.Append("<requestRecords>");
            xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableRecord>");
            xmlBuilder.Append("<recordOrder>0</recordOrder>");
            xmlBuilder.Append("<workflowRequestTableFields>");
            foreach (string key in ht.Keys)
            {
                string strValue = ht[key].ToString();
                strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("\'", "&apos;");

                xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableField>");
                xmlBuilder.Append("<fieldType></fieldType>");
                xmlBuilder.Append("<fieldName>").Append(key).Append("</fieldName>");
                xmlBuilder.Append("<fieldValue>").Append(strValue).Append("</fieldValue>");
                xmlBuilder.Append("<fieldOrder>0</fieldOrder>");
                xmlBuilder.Append("<isView>true</isView>");
                xmlBuilder.Append("<isEdit>true</isEdit>");
                xmlBuilder.Append("<isMand>false</isMand>");
                xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableField>");
            }
            xmlBuilder.Append("</workflowRequestTableFields>");
            xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableRecord>");
            xmlBuilder.Append("</requestRecords>");
            xmlBuilder.Append("</workflowMainTableInfo>");

            #endregion Head

            #region 明细

            xmlBuilder.Append("<workflowDetailTableInfos>");
            xmlBuilder.Append("<weaver.workflow.webservices.WorkflowDetailTableInfo>");
            xmlBuilder.Append("<workflowRequestTableRecords>");
            //明细

            foreach (POLine line in polines)
            {
                int docLineNo = line.DocLineNo;
                string itemCode = line.ItemInfo.ItemCode;
                string itemName = line.ItemInfo.ItemName;
                string itemSpecs = line.ItemInfo.ItemID.SPECS;
                string itemGrade = UFIDA.U9.CBO.SCM.Item.GradeEnum.GetFromValue(line.ItemInfo.ItemGrade.Value).Name;
                string itemNo = line.DescFlexSegments.PubDescSeg6;
                string purReq = line.DescFlexSegments.PubDescSeg5;
                string uomName = line.TradeUOM.Name;
                string deliveryDate = line.POShiplines[0].DeliveryDate.ToString("yyyy-MM-dd");
                string purQtyPU = line.TradeUOM.Round.GetRoundValue(line.PurQtyPU).ToString();
                string finallyPriceTC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.FinallyPriceTC).ToString();
                string finallyPriceFC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.FinallyPriceFC).ToString();
                string totalMnyTC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.TotalMnyTC).ToString();
                string totalMnyFC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.TotalMnyFC).ToString();
                string netMnyTC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.NetMnyTC).ToString();
                string netMnyFC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.NetMnyFC).ToString();
                string changeState = "变更前";
                
                Hashtable htDetail = new Hashtable();
                htDetail.Add("hh", docLineNo);
                htDetail.Add("lh", itemCode);
                htDetail.Add("pm", itemName);
                htDetail.Add("gg", itemSpecs);
                htDetail.Add("bz", itemGrade);
                htDetail.Add("jh", itemNo);
                htDetail.Add("cgyq", purReq);
                htDetail.Add("dw", uomName);
                htDetail.Add("jhsj", deliveryDate);
                htDetail.Add("sl", purQtyPU);
                htDetail.Add("dj", finallyPriceTC);
                htDetail.Add("zzjbb", finallyPriceFC);
                htDetail.Add("je", totalMnyTC);
                htDetail.Add("jshjbb", totalMnyFC);
                htDetail.Add("wseyb", netMnyTC);
                htDetail.Add("wsebb", netMnyFC);
                htDetail.Add("ChangeState", changeState);

                xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableRecord>");
                xmlBuilder.Append("<recordOrder>0</recordOrder>");

                xmlBuilder.Append("<workflowRequestTableFields>");
                //明细字段
                foreach (string key in htDetail.Keys)
                {
                    string strValue = htDetail[key].ToString();
                    strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("\'", "&apos;");

                    xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableField>");
                    xmlBuilder.Append("<fieldName>").Append(key).Append("</fieldName>");
                    xmlBuilder.Append("<fieldValue>").Append(strValue).Append("</fieldValue>");
                    xmlBuilder.Append("<fieldOrder>0</fieldOrder>");
                    xmlBuilder.Append("<isView>true</isView>");
                    xmlBuilder.Append("<isEdit>true</isEdit>");
                    xmlBuilder.Append("<isMand>false</isMand>");
                    xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableField>");
                }
                xmlBuilder.Append("</workflowRequestTableFields>");
                xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableRecord>");
            }

            xmlBuilder.Append("</workflowRequestTableRecords>");
            xmlBuilder.Append("</weaver.workflow.webservices.WorkflowDetailTableInfo>");
            xmlBuilder.Append("</workflowDetailTableInfos>");

            #endregion 明细

            return xmlBuilder.ToString();
        }

        /// <summary>
        /// 采购订单变更单OA审批流Xml
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="workflowId"></param>
        /// <param name="poModifyLines"></param>
        /// <returns></returns>
        public static string CreateMainXmlString(string userID, string workflowId, POModifyLine.EntityList poModifyLines)
        {
            Hashtable ht = new Hashtable();
            Hashtable htDetail = new Hashtable();
            Hashtable htMult = new Hashtable();
            foreach (POModifyLine line in poModifyLines)
            {
                //获取头部
                if (line.ModifiedEntityName == "UFIDA.U9.PM.PO.PurchaseOrder")
                {
                    string orgCode = Context.LoginOrg.Code;
                    string orgName = Context.LoginOrg.Name;
                    string docTypeName = line.POModify.PO.DocType.Name;
                    string id = line.POModify.PO.ID.ToString();
                    string docNo = line.POModify.PO.DocNo;
                    string businessDate = line.POModify.PO.BusinessDate.ToString("yyyy-MM-dd");
                    string suppCode = line.POModify.PO.Supplier.Code;
                    string suppName = line.POModify.PO.Supplier.Name.Trim();
                    string suppShortName = line.POModify.PO.Supplier.ShortName;
                    string purOperCode = line.POModify.PO.PurOper != null ? line.POModify.PO.PurOper.Code : string.Empty;
                    string purOperName = line.POModify.PO.PurOper != null ? line.POModify.PO.PurOper.Name : string.Empty;
                    string purDeptCode = line.POModify.PO.PurDept != null ? line.POModify.PO.PurDept.Code : string.Empty;
                    string purDeptName = line.POModify.PO.PurDept != null ? line.POModify.PO.PurDept.Name : string.Empty;
                    string currencyName = line.POModify.PO.AC.Name;
                    string poTotalMnyTC = line.POModify.PO.AC.MoneyRound.GetRoundValue(line.POModify.PO.TotalMnyTC).ToString();
                    string poTotalMnyFC = line.POModify.PO.AC.MoneyRound.GetRoundValue(line.POModify.PO.TotalMnyFC).ToString();
                    //采购方式
                    DefineValue dv = DefineValue.Finder.Find("ValueSetDef.Code='Z24' and Code=@Code", new OqlParam[] { new OqlParam(line.POModify.PO.DescFlexField.PrivateDescSeg14) });
                    string purWay = string.Empty;
                    if (dv != null)
                    {
                        purWay = dv.Name;
                    }
                    string suppShortName1 = line.POModify.PO.DescFlexField.PrivateDescSeg1;
                    string suppShortName2 = line.POModify.PO.DescFlexField.PrivateDescSeg9;
                    string isChange = "0";
                    if (line.POModify.PO.DescFlexField.PrivateDescSeg17.ToLower() == "true")
                    {
                        isChange = "1";
                    }
                    string isFixedContract = "0";
                    string signingReason = line.POModify.PO.DescFlexField.PrivateDescSeg13;
                    string payWay = line.POModify.PO.DescFlexField.PrivateDescSeg15;
                    string beforeBargainTotalPrice1 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg2) == false)
                    {
                        beforeBargainTotalPrice1 = line.POModify.PO.DescFlexField.PrivateDescSeg2;
                    }
                    string beforeBargainTotalPrice2 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg10) == false)
                    {
                        beforeBargainTotalPrice2 = line.POModify.PO.DescFlexField.PrivateDescSeg10;
                    }
                    string beforeBargainTotalPrice3 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg21) == false)
                    {
                        beforeBargainTotalPrice3 = line.POModify.PO.DescFlexField.PrivateDescSeg21;
                    }
                    string afterBargainTotalPrice1 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg3) == false)
                    {
                        afterBargainTotalPrice1 = line.POModify.PO.DescFlexField.PrivateDescSeg3;
                    }
                    string afterBargainTotalPrice2 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg11) == false)
                    {
                        afterBargainTotalPrice2 = line.POModify.PO.DescFlexField.PrivateDescSeg11;
                    }
                    string afterBargainTotalPrice3 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg22) == false)
                    {
                        afterBargainTotalPrice3 = line.POModify.PO.DescFlexField.PrivateDescSeg22;
                    }
                    string afterBargainAccPrice1 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg4) == false)
                    {
                        afterBargainAccPrice1 = line.POModify.PO.DescFlexField.PrivateDescSeg4;
                    }
                    string afterBargainAccPrice2 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg12) == false)
                    {
                        afterBargainAccPrice2 = line.POModify.PO.DescFlexField.PrivateDescSeg12;
                    }
                    string afterBargainAccPrice3 = "0";
                    if (string.IsNullOrEmpty(line.POModify.PO.DescFlexField.PrivateDescSeg23) == false)
                    {
                        afterBargainAccPrice3 = line.POModify.PO.DescFlexField.PrivateDescSeg23;
                    }
                    string bargainDeliveryDate1 = line.POModify.PO.DescFlexField.PrivateDescSeg6;
                    string bargainDeliveryDate2 = line.POModify.PO.DescFlexField.PrivateDescSeg18;
                    string bargainDeliveryDate3 = line.POModify.PO.DescFlexField.PrivateDescSeg25;
                    string assignSupp1 = "否";
                    string assignSupp2 = "否";
                    string assignSupp3 = "否";
                    if (line.POModify.PO.DescFlexField.PrivateDescSeg7.ToLower() == "true")
                    {
                        assignSupp1 = "是";
                    }
                    if (line.POModify.PO.DescFlexField.PrivateDescSeg19.ToLower() == "true")
                    {
                        assignSupp2 = "是";
                    }
                    if (line.POModify.PO.DescFlexField.PrivateDescSeg26.ToLower() == "true")
                    {
                        assignSupp3 = "是";
                    }
                    string manufacturer1 = line.POModify.PO.DescFlexField.PrivateDescSeg5;
                    string manufacturer2 = line.POModify.PO.DescFlexField.PrivateDescSeg16;
                    string manufacturer3 = line.POModify.PO.DescFlexField.PrivateDescSeg24;
                    string srcDocNo = string.Empty;
                    string projectCode = string.Empty;
                    string projectName = string.Empty;
                    string projectDesc = string.Empty;
                    string headDeliveryDate = string.Empty;
                    string projectGroup = string.Empty;
                    string itemDesc = line.POModify.PO.DescFlexField.PrivateDescSeg27;
                    string headChangeState = "变更后";

                    if (ht.ContainsKey("erpzzbm") == false)
                    {
                        ht.Add("erpzzbm", Context.LoginOrg.Code);//组织编码
                    }
                    if (ht.ContainsKey("erpzzmc") == false)
                    {
                        ht.Add("erpzzmc", Context.LoginOrg.Name);
                    }
                    if (ht.ContainsKey("erpdjlx") == false)
                    {
                        ht.Add("erpdjlx", docTypeName);
                    }
                    if (ht.ContainsKey("cgddid") == false)
                    {
                        ht.Add("cgddid", id);
                    }
                    if (ht.ContainsKey("htbh") == false)
                    {
                        ht.Add("htbh", docNo);
                    }
                    if (ht.ContainsKey("sqrq") == false)
                    {
                        ht.Add("sqrq", businessDate);
                    }
                    if (ht.ContainsKey("gysbm") == false)
                    {
                        ht.Add("gysbm", suppCode);
                    }
                    if (ht.ContainsKey("tjgf") == false)
                    {
                        ht.Add("tjgf", suppName);
                    }
                    if (ht.ContainsKey("gfjc1") == false)
                    {
                        ht.Add("gfjc1", suppShortName);
                    }
                    if (ht.ContainsKey("gfjc1") == false)
                    {
                        ht.Add("gfjc1", purOperCode);
                    }
                    if (ht.ContainsKey("bz2") == false)
                    {
                        ht.Add("bz2", currencyName);
                    }
                    if (ht.ContainsKey("cjj") == false)
                    {
                        ht.Add("cjj", poTotalMnyTC);
                    }
                    if (ht.ContainsKey("cjjrmb") == false)
                    {
                        ht.Add("cjjrmb", poTotalMnyFC);
                    }
                    if (ht.ContainsKey("cgfs") == false)
                    {
                        ht.Add("cgfs", purWay);
                    }
                    if (ht.ContainsKey("gfjc2") == false)
                    {
                        ht.Add("gfjc2", suppShortName1);
                    }
                    if (ht.ContainsKey("gfjc3") == false)
                    {
                        ht.Add("gfjc3", suppShortName2);
                    }
                    if (ht.ContainsKey("httksfbg") == false)
                    {
                        ht.Add("httksfbg", isChange);
                    }
                    if (ht.ContainsKey("sfgdzj") == false)
                    {
                        ht.Add("sfgdzj", isFixedContract);
                    }
                    if (ht.ContainsKey("qyly") == false)
                    {
                        ht.Add("qyly", signingReason);
                    }
                    if (ht.ContainsKey("fkfs") == false)
                    {
                        ht.Add("fkfs", payWay);
                    }
                    if (ht.ContainsKey("yjqzj") == false)
                    {
                        ht.Add("yjqzj", beforeBargainTotalPrice1);
                    }
                    if (ht.ContainsKey("yjqzj2") == false)
                    {
                        ht.Add("yjqzj2", beforeBargainTotalPrice2);
                    }
                    if (ht.ContainsKey("yjqzj3") == false)
                    {
                        ht.Add("yjqzj3", beforeBargainTotalPrice3);
                    }
                    if (ht.ContainsKey("yjhxhj") == false)
                    {
                        ht.Add("yjhxhj", afterBargainTotalPrice1);
                    }
                    if (ht.ContainsKey("yjhxhj2") == false)
                    {
                        ht.Add("yjhxhj2", afterBargainTotalPrice1);
                    }
                    if (ht.ContainsKey("yjhxhj3") == false)
                    {
                        ht.Add("yjhxhj3", afterBargainTotalPrice1);
                    }
                    if (ht.ContainsKey("yjhcdj") == false)
                    {
                        ht.Add("yjhcdj", afterBargainAccPrice1);
                    }
                    if (ht.ContainsKey("yjhcdj2") == false)
                    {
                        ht.Add("yjhcdj2", afterBargainAccPrice2);
                    }
                    if (ht.ContainsKey("yjhcdj3") == false)
                    {
                        ht.Add("yjhcdj3", afterBargainAccPrice3);
                    }
                    if (ht.ContainsKey("xjjq1") == false)
                    {
                        ht.Add("xjjq1", bargainDeliveryDate1);
                    }
                    if (ht.ContainsKey("xjjq2") == false)
                    {
                        ht.Add("xjjq2", bargainDeliveryDate2);
                    }
                    if (ht.ContainsKey("xjjq3") == false)
                    {
                        ht.Add("xjjq3", bargainDeliveryDate3);
                    }
                    if (ht.ContainsKey("yzzdgf1") == false)
                    {
                        ht.Add("yzzdgf1", assignSupp1);
                    }
                    if (ht.ContainsKey("yzzdgf2") == false)
                    {
                        ht.Add("yzzdgf2", assignSupp2);
                    }
                    if (ht.ContainsKey("yzzdgf3") == false)
                    {
                        ht.Add("yzzdgf3", assignSupp3);
                    }
                    if (ht.ContainsKey("sccplcj1") == false)
                    {
                        ht.Add("sccplcj1", manufacturer1);
                    }
                    if (ht.ContainsKey("sccplcj2") == false)
                    {
                        ht.Add("sccplcj2", manufacturer2);
                    }
                    if (ht.ContainsKey("sccplcj3") == false)
                    {
                        ht.Add("sccplcj3", manufacturer3);
                    }
                    //ht.Add("qgdh", srcDocNo);
                    //ht.Add("jyhth", projectName);
                    //ht.Add("xmmc", projectDesc);
                    //ht.Add("htjhq", headDeliveryDate);
                    //ht.Add("xmfl", projectGroup);
                    if (ht.ContainsKey("wlms") == false)
                    {
                        ht.Add("wlms", itemDesc);
                    }
                    if (ht.ContainsKey("ChangeState") == false)
                    {
                        ht.Add("ChangeState", headChangeState);
                    }
                    //赋值
                    if (line.ModifiedData == "DocType")
                    {
                        docTypeName = line.NameAfterModifeid;
                    }
                    if (line.ModifiedData == "BusinessDate")
                    {
                        businessDate = Convert.ToDateTime(line.DataAfterModified).ToString("yyyy-MM-dd");
                    }
                    if (line.ModifiedData == "PurOper")
                    {
                        purOperCode = line.CodeAfterModified;
                        purOperName = line.NameAfterModifeid;
                    }
                    if (line.ModifiedData == "PurDept")
                    {
                        purOperCode = line.CodeAfterModified;
                        purOperName = line.NameAfterModifeid;
                    }
                    if (line.ModifiedData == "TC")
                    {
                        currencyName = line.NameAfterModifeid;
                    }
                    if (line.ModifiedData == "TotalMnyTC")
                    {
                        poTotalMnyTC = line.NameAfterModifeid;
                    }
                    if (line.ModifiedData == "TotalMnyFC")
                    {
                        poTotalMnyFC = line.POModify.PO.TotalMnyFC.ToString();
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg14")
                    {
                        purWay = line.NameAfterModifeid;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg1")
                    {
                        suppShortName1 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg9")
                    {
                        suppShortName2 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg17")
                    {
                        if (line.DataAfterModified.ToLower() == "true")
                        {
                            isChange = "1";
                        }
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg13")
                    {
                        signingReason = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg15")
                    {
                        payWay = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg2")
                    {
                        beforeBargainTotalPrice1 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg10")
                    {
                        beforeBargainTotalPrice2 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg21")
                    {
                        beforeBargainTotalPrice3 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg3")
                    {
                        afterBargainTotalPrice1 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg11")
                    {
                        afterBargainTotalPrice2 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg22")
                    {
                        afterBargainTotalPrice3 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg4")
                    {
                        afterBargainAccPrice1 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg12")
                    {
                        afterBargainAccPrice2 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg23")
                    {
                        afterBargainAccPrice3 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg6")
                    {
                        bargainDeliveryDate1 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg18")
                    {
                        bargainDeliveryDate2 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg25")
                    {
                        bargainDeliveryDate2 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg7")
                    {
                        if (line.DataAfterModified.ToLower() == "true")
                        {
                            assignSupp1 = "是";
                        }
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg19")
                    {
                        if (line.DataAfterModified.ToLower() == "true")
                        {
                            assignSupp1 = "是";
                        }
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg26")
                    {
                        if (line.DataAfterModified.ToLower() == "true")
                        {
                            assignSupp3 = "是";
                        }
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg5")
                    {
                        manufacturer1 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg16")
                    {
                        manufacturer2 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg16")
                    {
                        manufacturer3 = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexField.PrivateDescSeg27")
                    {
                        manufacturer3 = line.DataAfterModified;
                    }
                }
                //获取明细行
                if (line.ModifiedEntityName == "UFIDA.U9.PM.PO.POLine")
                {
                    POLine poLine = POLine.Finder.FindByID(line.POLineID);

                    string key = line.POModify.PO.DocNo + "_" + line.POLineDocLineNo.ToString();
                    string srcDocNo = poLine.SrcDocInfo != null ? poLine.SrcDocInfo.SrcDocNo : string.Empty;
                    string projectCode = poLine.Project != null ? poLine.Project.Code : string.Empty;
                    string projectName = poLine.Project != null ? poLine.Project.Name : string.Empty;
                    string projectDesc = poLine.Project != null ? poLine.Project.Description : string.Empty;
                    string projectGroup = poLine.Project != null ? poLine.Project.DescFlexField.PubDescSeg34 : string.Empty;
                    int docLineNo = line.POLineDocLineNo;
                    string itemCode = line.ItemInfo.Code;
                    string itemName = line.ItemInfo.Name;
                    string itemSpecs = line.ItemInfo.SPECS;
                    string itemGrade = UFIDA.U9.CBO.SCM.Item.GradeEnum.GetFromValue(poLine.ItemInfo.ItemGrade.Value).Name;
                    string itemNo = poLine.DescFlexSegments.PubDescSeg6;
                    string purReq = poLine.DescFlexSegments.PubDescSeg5;
                    string uomName = poLine.TradeUOM.Name;
                    string purQtyPU = poLine.TradeUOM.Round.GetRoundValue(poLine.PurQtyPU).ToString();
                    string finallyPriceTC = poLine.PurchaseOrder.AC.MoneyRound.GetRoundValue(poLine.FinallyPriceTC).ToString();
                    string finallyPriceFC = poLine.PurchaseOrder.AC.MoneyRound.GetRoundValue(poLine.FinallyPriceFC).ToString();
                    string totalMnyTC = poLine.PurchaseOrder.AC.MoneyRound.GetRoundValue(poLine.TotalMnyTC).ToString();
                    string totalMnyFC = poLine.PurchaseOrder.AC.MoneyRound.GetRoundValue(poLine.TotalMnyFC).ToString();
                    string netMnyTC = poLine.PurchaseOrder.AC.MoneyRound.GetRoundValue(poLine.NetMnyTC).ToString();
                    string netMnyFC = poLine.PurchaseOrder.AC.MoneyRound.GetRoundValue(poLine.NetMnyFC).ToString();
                    string changeState = "变更前";

                    if (ht.ContainsKey("qgdh") == false)
                    {
                        ht.Add("qgdh", srcDocNo);//单头来源单据号
                    }
                    if (ht.ContainsKey("jyhth") == false)
                    {
                        ht.Add("jyhth", projectName);//单头项目名称
                    }
                    if (ht.ContainsKey("xmmc") == false)
                    {
                        ht.Add("xmmc", projectDesc);//单头项目描述
                    }
                    if (ht.ContainsKey("xmfl") == false)
                    {
                        ht.Add("xmfl", projectGroup);//项目分组
                    }
                    if (ht.ContainsKey("hh") == false)
                    {
                        htDetail.Add("hh", docLineNo);
                    }
                    if (ht.ContainsKey("lh") == false)
                    {
                        htDetail.Add("lh", itemCode);
                    }
                    if (ht.ContainsKey("pm") == false)
                    {
                        htDetail.Add("pm", itemName);
                    }
                    if (ht.ContainsKey("gg") == false)
                    {
                        htDetail.Add("gg", itemSpecs);
                    }
                    if (ht.ContainsKey("bz") == false)
                    {
                        htDetail.Add("bz", itemGrade);
                    }
                    if (ht.ContainsKey("jh") == false)
                    {
                        htDetail.Add("jh", itemNo);
                    }
                    if (ht.ContainsKey("cgyq") == false)
                    {
                        htDetail.Add("cgyq", purReq);
                    }
                    if (ht.ContainsKey("dw") == false)
                    {
                        htDetail.Add("dw", uomName);
                    }
                    if (ht.ContainsKey("sl") == false)
                    {
                        htDetail.Add("sl", purQtyPU);
                    }
                    if (ht.ContainsKey("dj") == false)
                    {
                        htDetail.Add("dj", finallyPriceTC);
                    }
                    if (ht.ContainsKey("zzjbb") == false)
                    {
                        htDetail.Add("zzjbb", finallyPriceFC);
                    }
                    if (ht.ContainsKey("je") == false)
                    {
                        htDetail.Add("je", totalMnyTC);
                    }
                    if (ht.ContainsKey("jshjbb") == false)
                    {
                        htDetail.Add("jshjbb", totalMnyFC);
                    }
                    if (ht.ContainsKey("wseyb") == false)
                    {
                        htDetail.Add("wseyb", netMnyTC);
                    }
                    if (ht.ContainsKey("wsebb") == false)
                    {
                        htDetail.Add("wsebb", netMnyFC);
                    }
                    if (ht.ContainsKey("ChangeState") == false)
                    {
                        htDetail.Add("ChangeState", changeState);
                    }

                    #region 根据字段获取修改后的值

                    if (line.ModifiedData == "Project")
                    {
                        Project project = Project.Finder.FindByID(line.DataAfterModified);
                        projectCode = line.CodeAfterModified;
                        projectName = line.NameAfterModifeid;
                        projectDesc = poLine.Project != null ? project.Description : string.Empty;
                        projectGroup = poLine.Project != null ? project.DescFlexField.PubDescSeg34 : string.Empty;
                    }
                    if (line.ModifiedData == "DescFlexSegments.PubDescSeg6")
                    {
                        itemNo = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "DescFlexSegments.PubDescSeg6")
                    {
                        purReq = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "TradeUOM")
                    {
                        uomName = line.NameAfterModifeid;
                    }
                    if (line.ModifiedData == "PurQtyPU")
                    {
                        purQtyPU = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "FinallyPriceTC")
                    {
                        finallyPriceTC = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "FinallyPriceFC")
                    {
                        finallyPriceFC = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "TotalMnyTC")
                    {
                        totalMnyTC = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "TotalMnyFC")
                    {
                        totalMnyFC = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "NetMnyTC")
                    {
                        netMnyTC = line.DataAfterModified;
                    }
                    if (line.ModifiedData == "NetMnyFC")
                    {
                        netMnyFC = line.DataAfterModified;
                    }

                    if(htMult.ContainsKey(key)==false)
                    {
                        htMult.Add(key, htDetail);
                    }
                    else
                    {
                        htMult[key] = htDetail;
                    }

                    #endregion
                }
                //获取子行
                if (line.ModifiedEntityName == "UFIDA.U9.PM.PO.POShipLine")
                {
                    string poDocNo = line.POModify.PO.DocNo;
                    int poDocLineNo = line.POLineDocLineNo;
                    string key = line.POModify.PO.DocNo + "_" + line.POLineDocLineNo.ToString(); 
                    if (line.ModifiedData == "DeliveryDate")
                    {
                        POShipLine poShipLine = POShipLine.Finder.FindByID(line.POShipLineID);
                        string deliveryDate = poShipLine.DeliveryDate.ToString("yyyy-MM-dd");

                        if (htMult.ContainsKey(key))
                        {
                            Hashtable htD = htMult[key] as Hashtable;
                            if (ht.ContainsKey("htjhq") == false)
                            {
                                ht.Add("htjhq", deliveryDate);
                            }
                            else
                            {
                                ht["htjhq"] = deliveryDate;
                            }
                            if (htDetail.ContainsKey("jhsj") == false)
                            {
                                htDetail.Add("jhsj", deliveryDate);
                            }
                            else
                            {
                                htDetail["jhsj"] = deliveryDate;
                            }
                        }
                    }
                }
            }


            #region Head

            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.Append("<workflowMainTableInfo>");
            xmlBuilder.Append("<requestRecords>");
            xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableRecord>");
            xmlBuilder.Append("<recordOrder>0</recordOrder>");
            xmlBuilder.Append("<workflowRequestTableFields>");
            foreach (string key in ht.Keys)
            {
                xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableField>");
                xmlBuilder.Append("<fieldType></fieldType>");
                xmlBuilder.Append("<fieldName>").Append(key).Append("</fieldName>");
                string strValue = ht[key].ToString();
                strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("\'", "&apos;");
                xmlBuilder.Append("<fieldValue>").Append(strValue).Append("</fieldValue>");
                xmlBuilder.Append("<fieldOrder>0</fieldOrder>");
                xmlBuilder.Append("<isView>true</isView>");
                xmlBuilder.Append("<isEdit>true</isEdit>");
                xmlBuilder.Append("<isMand>false</isMand>");
                xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableField>");
            }
            xmlBuilder.Append("</workflowRequestTableFields>");
            xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableRecord>");
            xmlBuilder.Append("</requestRecords>");
            xmlBuilder.Append("</workflowMainTableInfo>");

            #endregion Head

            #region 明细

            xmlBuilder.Append("<workflowDetailTableInfos>");
            xmlBuilder.Append("<weaver.workflow.webservices.WorkflowDetailTableInfo>");
            xmlBuilder.Append("<workflowRequestTableRecords>");
            //明细
            foreach (string rootKey in htMult)
            {
                Hashtable htCurr = htMult[rootKey] as Hashtable;
                xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableRecord>");
                xmlBuilder.Append("<recordOrder>0</recordOrder>");

                xmlBuilder.Append("<workflowRequestTableFields>");
                //明细字段
                foreach (string key in htCurr.Keys)
                {
                    string strValue = htCurr[key].ToString();
                    strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("\'", "&apos;");

                    xmlBuilder.Append("<weaver.workflow.webservices.WorkflowRequestTableField>");
                    xmlBuilder.Append("<fieldName>").Append(key).Append("</fieldName>");
                    xmlBuilder.Append("<fieldValue>").Append(strValue).Append("</fieldValue>");
                    xmlBuilder.Append("<fieldOrder>0</fieldOrder>");
                    xmlBuilder.Append("<isView>true</isView>");
                    xmlBuilder.Append("<isEdit>true</isEdit>");
                    xmlBuilder.Append("<isMand>false</isMand>");
                    xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableField>");
                }
                xmlBuilder.Append("</workflowRequestTableFields>");
                xmlBuilder.Append("</weaver.workflow.webservices.WorkflowRequestTableRecord>");
            }

            xmlBuilder.Append("</workflowRequestTableRecords>");
            xmlBuilder.Append("</weaver.workflow.webservices.WorkflowDetailTableInfo>");
            xmlBuilder.Append("</workflowDetailTableInfos>");

            #endregion 明细

            return xmlBuilder.ToString();
        }

        /// <summary>
        /// 修改OA审批流
        /// </summary>
        /// <param name="requestName"></param>
        /// <param name="userID"></param>
        /// <param name="workflowId"></param>
        /// <param name="isPO"></param>
        /// <param name="polines"></param>
        /// <param name="poModifyLines"></param>
        /// <returns></returns>
        public static string CreateXmlStringForUpdate(string requestID, string userID, string workflowId, bool isPO, POLine.EntityList polines, POModifyLine.EntityList poModifyLines)
        {
            string tableXml = CreateMainXmlStringForUpdate(userID, workflowId, isPO, polines, poModifyLines);
            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.Append("<WorkflowRequestInfo>");
            xmlBuilder.Append("<requestid>").Append(requestID).Append("</requestid>");
            xmlBuilder.Append(tableXml);
            xmlBuilder.Append("</WorkflowRequestInfo>");
            return xmlBuilder.ToString();
        }

        /// <summary>
        /// 修改OA审批流程Xml
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="workflowId"></param>
        /// <param name="isPO"></param>
        /// <param name="polines"></param>
        /// <param name="poModifyLines"></param>
        /// <returns></returns>
        public static string CreateMainXmlStringForUpdate(string userID, string workflowId, bool isPO, POLine.EntityList polines, POModifyLine.EntityList poModifyLines)
        {
            StringBuilder xmlBuilder = new StringBuilder();
            Hashtable ht = new Hashtable();
            string orgCode = Context.LoginOrg.Code;
            string orgName = Context.LoginOrg.Name;
            string docTypeName = polines[0].PurchaseOrder.DocType.Name;
            string id = polines[0].PurchaseOrder.ID.ToString();
            string docNo = polines[0].PurchaseOrder.DocNo;
            string businessDate = polines[0].PurchaseOrder.BusinessDate.ToString("yyyy-MM-dd");
            string suppCode = polines[0].PurchaseOrder.Supplier.Code;
            string suppName = polines[0].PurchaseOrder.Supplier.Name.Trim();
            string suppShortName = polines[0].PurchaseOrder.Supplier.ShortName;
            string purOperCode = polines[0].PurchaseOrder.PurOper != null ? polines[0].PurchaseOrder.PurOper.Code : string.Empty;
            string purOperName = polines[0].PurchaseOrder.PurOper != null ? polines[0].PurchaseOrder.PurOper.Name : string.Empty;
            string purDeptCode = polines[0].PurchaseOrder.PurDept != null ? polines[0].PurchaseOrder.PurDept.Code : string.Empty;
            string purDeptName = polines[0].PurchaseOrder.PurDept != null ? polines[0].PurchaseOrder.PurDept.Name : string.Empty;
            string currencyName = polines[0].PurchaseOrder.AC.Name;
            string poTotalMnyTC = polines[0].PurchaseOrder.AC.MoneyRound.GetRoundValue(polines[0].PurchaseOrder.TotalMnyTC).ToString();
            string poTotalMnyFC = polines[0].PurchaseOrder.AC.MoneyRound.GetRoundValue(polines[0].PurchaseOrder.TotalMnyFC).ToString();
            //采购方式
            DefineValue dv = DefineValue.Finder.Find("ValueSetDef.Code='Z24' and Code=@Code", new OqlParam[] { new OqlParam(polines[0].DescFlexSegments.PrivateDescSeg14) });
            string purWay = string.Empty;
            if (dv != null)
            {
                purWay = dv.Name;
            }
            string suppShortName1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg1;
            string suppShortName2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg9;
            string IsChange = "0";
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg17.ToLower() == "true")
            {
                IsChange = "1";
            }
            string isFixedContract = "0";
            string signingReason = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg13;
            string payWay = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg15;
            string beforeBargainTotalPrice1 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg2) == false)
            {
                beforeBargainTotalPrice1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg2;
            }
            string beforeBargainTotalPrice2 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg10) == false)
            {
                beforeBargainTotalPrice2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg10;
            }
            string beforeBargainTotalPrice3 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg21) == false)
            {
                beforeBargainTotalPrice3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg21;
            }

            string afterBargainTotalPrice1 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg3) == false)
            {
                afterBargainTotalPrice1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg3;
            }
            string afterBargainTotalPrice2 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg11) == false)
            {
                afterBargainTotalPrice2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg11;
            }
            string afterBargainTotalPrice3 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg22) == false)
            {
                afterBargainTotalPrice3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg22;
            }
            string afterBargainAccPrice1 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg4) == false)
            {
                afterBargainAccPrice1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg4;
            }
            string afterBargainAccPrice2 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg12) == false)
            {
                afterBargainAccPrice2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg12;
            }
            string afterBargainAccPrice3 = "0";
            if (string.IsNullOrEmpty(polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg23) == false)
            {
                afterBargainAccPrice3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg23;
            }
            string bargainDeliveryDate1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg6;
            string bargainDeliveryDate2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg18;
            string bargainDeliveryDate3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg25;
            string assignSupp1 = "否";
            string assignSupp2 = "否";
            string assignSupp3 = "否";
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg7.ToLower() == "true")
            {
                assignSupp1 = "是";
            }
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg19.ToLower() == "true")
            {
                assignSupp2 = "是";
            }
            if (polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg26.ToLower() == "true")
            {
                assignSupp3 = "是";
            }
            string manufacturer1 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg5;
            string manufacturer2 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg16;
            string manufacturer3 = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg24;
            string srcDocNo = polines[0].SrcDocInfo != null ? polines[0].SrcDocInfo.SrcDocNo : string.Empty;
            string projectCode = polines[0].Project != null ? polines[0].Project.Code : string.Empty;
            string projectName = polines[0].Project != null ? polines[0].Project.Name : string.Empty;
            string projectDesc = polines[0].Project != null ? polines[0].Project.Description : string.Empty;
            string headDeliveryDate = polines[0].POShiplines[0].DeliveryDate.ToString("yyyy-MM-dd");
            string projectGroup = polines[0].Project != null ? polines[0].Project.DescFlexField.PubDescSeg34 : string.Empty;
            string itemDesc = polines[0].PurchaseOrder.DescFlexField.PrivateDescSeg27;
            string headChangeState = "变更前";
            
            ht.Add("erpzzbm", orgCode);//组织编码
            ht.Add("erpzzmc", orgName);
            ht.Add("erpdjlx", docTypeName);
            ht.Add("cgddid", id);
            ht.Add("htbh", docNo);
            ht.Add("sqrq", businessDate);
            ht.Add("gysbm", suppCode);
            ht.Add("tjgf", suppName);
            ht.Add("gfjc1", suppShortName);
            ht.Add("sqr", purOperCode);
            ht.Add("bz2", currencyName);
            ht.Add("cjj", poTotalMnyTC);
            ht.Add("cjjrmb", poTotalMnyFC);
            ht.Add("cgfs", purWay);
            ht.Add("gfjc2", suppShortName1);
            ht.Add("gfjc3", suppShortName2);
            ht.Add("httksfbg", IsChange);
            ht.Add("sfgdzj", isFixedContract);
            ht.Add("qyly", signingReason);
            ht.Add("fkfs", payWay);
            ht.Add("yjqzj", beforeBargainTotalPrice1);
            ht.Add("yjqzj2", beforeBargainTotalPrice2);
            ht.Add("yjqzj3", beforeBargainTotalPrice3);
            ht.Add("yjhxhj", afterBargainTotalPrice1);
            ht.Add("yjhxhj2", afterBargainTotalPrice2);
            ht.Add("yjhxhj3", afterBargainTotalPrice3);
            ht.Add("yjhcdj", afterBargainAccPrice1);
            ht.Add("yjhcdj2", afterBargainAccPrice2);
            ht.Add("yjhcdj3", afterBargainAccPrice3);
            ht.Add("xjjq1", bargainDeliveryDate1);
            ht.Add("xjjq2", bargainDeliveryDate2);
            ht.Add("xjjq3", bargainDeliveryDate3);
            ht.Add("yzzdgf1", assignSupp1);
            ht.Add("yzzdgf2", assignSupp2);
            ht.Add("yzzdgf3", assignSupp3);
            ht.Add("sccplcj1", manufacturer1);
            ht.Add("sccplcj2", manufacturer2);
            ht.Add("sccplcj3", manufacturer3);
            ht.Add("qgdh", srcDocNo);
            ht.Add("jyhth", projectName);
            ht.Add("xmmc", projectDesc);
            ht.Add("htjhq", headDeliveryDate);
            ht.Add("xmfl", projectGroup);
            ht.Add("wlms", itemDesc);
            ht.Add("ChangeState", headChangeState);

            #region Head

            xmlBuilder.Append("<workflowMainTableInfo>");
            foreach (string key in ht.Keys)
            {
                string strValue = ht[key].ToString();
                strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("\'", "&apos;");

                xmlBuilder.Append("<WorkflowRequestTableField>");
                xmlBuilder.Append("<fieldName>").Append(key).Append("</fieldName>");
                xmlBuilder.Append("<fieldValue>").Append(strValue).Append("</fieldValue>");
                xmlBuilder.Append("</WorkflowRequestTableField>");
            }
            xmlBuilder.Append("</workflowMainTableInfo>");

            #endregion Head

            #region 明细

            xmlBuilder.Append("<workflowDetailTableInfos>");
            xmlBuilder.Append("<WorkflowDetailTableInfo>");
            foreach (POLine line in polines)
            {
                int docLineNo = line.DocLineNo;
                string itemCode = line.ItemInfo.ItemCode;
                string itemName = line.ItemInfo.ItemName;
                string itemSpecs = line.ItemInfo.ItemID.SPECS;
                string itemGrade = UFIDA.U9.CBO.SCM.Item.GradeEnum.GetFromValue(line.ItemInfo.ItemGrade.Value).Name;
                string itemNo = line.DescFlexSegments.PubDescSeg6;
                string purReq = line.DescFlexSegments.PubDescSeg5;
                string uomName = line.TradeUOM.Name;
                string deliveryDate = line.POShiplines[0].DeliveryDate.ToString("yyyy-MM-dd");
                string purQtyPU = line.TradeUOM.Round.GetRoundValue(line.PurQtyPU).ToString();
                string finallyPriceTC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.FinallyPriceTC).ToString();
                string finallyPriceFC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.FinallyPriceFC).ToString();
                string totalMnyTC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.TotalMnyTC).ToString();
                string totalMnyFC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.TotalMnyFC).ToString();
                string netMnyTC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.NetMnyTC).ToString();
                string netMnyFC = line.PurchaseOrder.AC.MoneyRound.GetRoundValue(line.NetMnyFC).ToString();
                string changeState = "变更前";
                
                Hashtable htDetail = new Hashtable();
                htDetail.Add("hh", docLineNo);
                htDetail.Add("lh", itemCode);
                htDetail.Add("pm", itemName);
                htDetail.Add("gg", itemSpecs);
                htDetail.Add("bz", itemGrade);
                htDetail.Add("jh", itemNo);
                htDetail.Add("cgyq", purReq);
                htDetail.Add("dw", uomName);
                htDetail.Add("jhsj", deliveryDate);
                htDetail.Add("sl", purQtyPU);
                htDetail.Add("dj", finallyPriceTC);
                htDetail.Add("zzjbb", finallyPriceFC);
                htDetail.Add("je", totalMnyTC);
                htDetail.Add("jshjbb", totalMnyFC);
                htDetail.Add("wseyb", netMnyTC);
                htDetail.Add("wsebb", netMnyFC);
                htDetail.Add("ChangeState", changeState);

                xmlBuilder.Append("<workflowRequestTableRecords>");
                xmlBuilder.Append("<WorkflowRequestTableRecord>");
                //明细字段
                foreach (string key in htDetail.Keys)
                {
                    string strValue = htDetail[key].ToString();
                    strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("\'", "&apos;");

                    xmlBuilder.Append("<WorkflowRequestTableField>");
                    xmlBuilder.Append("<fieldName>").Append(key).Append("</fieldName>");
                    xmlBuilder.Append("<fieldValue>").Append(strValue).Append("</fieldValue>");
                    xmlBuilder.Append("</WorkflowRequestTableField>");
                }
                xmlBuilder.Append("</WorkflowRequestTableRecord>");
                xmlBuilder.Append("</workflowRequestTableRecords>");
            }
            xmlBuilder.Append("</WorkflowDetailTableInfo>");
            xmlBuilder.Append("</workflowDetailTableInfos>");

            #endregion 明细

            return xmlBuilder.ToString();
        }

        //Get方法调用接口获取json文件内容  
        public static string GetFunction(string requestURL, string clientID, string clientSecret, string grantType, string companyid, string userName, string password)
        {
            StringBuilder svBuilder = new StringBuilder(requestURL);
            svBuilder.Append("token?client_id=").Append(clientID);
            svBuilder.Append("&client_secret=").Append(clientSecret);
            svBuilder.Append("&grant_type=").Append(grantType);
            svBuilder.Append("&companyid=").Append(companyid);
            svBuilder.Append("&username=").Append(userName);
            svBuilder.Append("&password=").Append(password);
            string serviceAddress = svBuilder.ToString();
            logger.Error("获取Token请求地址:" + serviceAddress);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string jsonString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            logger.Error("获取Token请求返回值:" + jsonString);
            //解析josn  
            JObject jo = JObject.Parse(jsonString);
            string access_token = jo["data"]["access_token"].ToString();
            return access_token;
        }

        //Post方法调用接口获取json文件内容  
        public static string PostFunction(string requestURL, string clientID, string clientSecret, string grantType, string companyid, string userName, string password)
        {
            StringBuilder svBuilder = new StringBuilder(requestURL);
            svBuilder.Append("token?client_id=").Append(clientID);
            svBuilder.Append("&client_secret=").Append(clientSecret);
            svBuilder.Append("&grant_type=").Append(grantType);
            svBuilder.Append("&companyid=").Append(companyid);
            svBuilder.Append("&username=").Append(userName);
            svBuilder.Append("&password=").Append(password);
            string serviceAddress = svBuilder.ToString();
            logger.Error("获取Token请求地址:" + serviceAddress);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(myResponseStream, Encoding.UTF8);
            string jsonString = reader.ReadToEnd();
            reader.Close();
            myResponseStream.Close();
            logger.Error("获取Token请求返回值:" + jsonString);
            //解析josn  
            JObject jo = JObject.Parse(jsonString);
            string access_token = jo["data"]["access_token"].ToString();
            return access_token;
        }
    }
}