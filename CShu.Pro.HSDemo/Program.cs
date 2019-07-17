using CShu.Pro.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime dt = DateTime.Parse("2015-5-2 11:30:25");
            //string s= dt.ToString("yyyy-MM");
            //throw new Exception("");
            //Demo案列
            /*{
                UserModel userModel = new UserModel();
                userModel.CompanyId = 500000;
                userModel.Email = @"123456";
                BaseDAL.Save<UserModel>(userModel);

                string remark = UserState.Normal.GetRemark();
                
            }*/

            //string message = System.Text.RegularExpressions.Regex.Unescape(@"");

            //decimal iNum = 22.000000000M;
            //int i = 5;
            //if (iNum.ToString().Contains("."))
            //{
            //    string[] str = iNum.ToString().Split('.');
            //    string sResult = str[1].Substring(i, str[1].Length-i);
            //    int iResult = int.Parse(sResult);
            //}

            //string ss = @"""345435";
            //bool flage = ss.Contains("\"");
            //if (flage)
            //{
            //}
            //throw new Exception();


            //2.邮件发送
            //string _from, _to, _subject, _body;
            //_from = "787239831@qq.com";
            //_to = "zxw51_test_2@163.com";
            //_subject = "邮件标题！";
            //_body = "测试邮件！";

            //MailMessage _msg = new MailMessage(_from, _to, _subject, _body);

            //SmtpClient _client = new SmtpClient("smtp.yonyou.com", 25);
            //_client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //_client.Credentials = new NetworkCredential("zxw51_test_1", "765432");

            //_client.Send(_msg);


            DataTable dtNew = new DataTable("DESADV");


            List<string> listStr = new List<string>();

            //listStr.Add("");
            //listStr.Add("111");
            //if (listStr.Contains(""))
            //{
            //    Console.WriteLine("");
            //}

            //throw new Exception("");
            #region 添加表头

            //料品
            dtNew.Columns.Add("ItemCode", Type.GetType("System.String"));
            //存储地点
            dtNew.Columns.Add("Wh", Type.GetType("System.String"));
            //成本域
            dtNew.Columns.Add("CostField", Type.GetType("System.String"));
            //批号
            dtNew.Columns.Add("Lot", Type.GetType("System.String"));
            //调整方向
            dtNew.Columns.Add("AdjustTo", Type.GetType("System.String"));
            //本层材料费
            dtNew.Columns.Add("ItemAmt", Type.GetType("System.String"));
            //本层人工费
            dtNew.Columns.Add("WorkAmt", Type.GetType("System.String"));
            //本层制造费
            dtNew.Columns.Add("MFGAmt", Type.GetType("System.String"));
            //本层机器费
            dtNew.Columns.Add("MachineAmt", Type.GetType("System.String"));
            //本层外协费
            dtNew.Columns.Add("CollabAmt", Type.GetType("System.String"));
            //本层采购成本
            dtNew.Columns.Add("PurAmt", Type.GetType("System.String"));
            //下层材料费
            dtNew.Columns.Add("NextItemAmt", Type.GetType("System.String"));
            //下层人工费
            dtNew.Columns.Add("NextWorkAmt", Type.GetType("System.String"));
            //下层制造费
            dtNew.Columns.Add("NextMFGAmt", Type.GetType("System.String"));
            //下层机器费
            dtNew.Columns.Add("NextMachineAmt", Type.GetType("System.String"));
            //下层外协费
            dtNew.Columns.Add("NextCollabAmt", Type.GetType("System.String"));
            //下层采购成本
            dtNew.Columns.Add("NextPurAmt", Type.GetType("System.String"));
           
            #endregion
            

            UFSoft.UBF.Exceptions1.MessageBase[] outMessages;
            object content = GetContext.CreateContextObj();

            ExceptionHelper.TryCatch(() =>
            {
                List <string> requestInfos = new List<string>();
                //Bom物料清单
                //requestInfos.Add(CreateBomXML.CreateRequestXml());  
                //生成订单开工
                //requestInfos.Add(CreateMOStartXML.CreateRequestXml());  
                //生产订单反开工
                //requestInfos.Add(CreateAntiStartMOXML.CreateRequestXml());  
                //工时维护
                //requestInfos.Add(CreateMOWorkHourXML.CreateRequestXml());  
                //收货单
                //requestInfos.Add(CreateRcvXML.CreateRequestXml());  
                //采购退货
                //requestInfos.Add(CreatePurchaseRtnXML.CreateRequestXml());
                //杂收
                //requestInfos.Add(CreateMiscRcvTransXML.CreateRequestXml());
                //杂发
                //requestInfos.Add(CreateMiscShipCommonXML.CreateRequestXml());
                //非成套领料
                //requestInfos.Add(CreatePMIssueXML.CreateRequestXml());
                //调入
                //requestInfos.Add(CreateTransInFormXML.CreateRequestXml());
                //材料出库
                //requestInfos.Add(CreateIssueFormXML.CreateRequestXml());
                //材料入库
                //requestInfos.Add(CreateIssueFormRedFlushXML.CreateRequestXml());
                //成品出入库
                //requestInfos.Add(CreateRcvRptDocXML.CreateRequestXml());                
                //形态转换
                //requestInfos.Add(CreateTransferFormXML.CreateRequestXml());
                //出货单SM_Ship
                //requestInfos.Add(CreateShipXML.CreateRequestXml());
                //销售退回收货
                //requestInfos.Add(CreateRMAXML.CreateRequestXml());
                //测试
                //requestInfos.Add(@"<body>");
                requestInfos.Add(@"<body><item><businessType>Rcv</businessType><actionType>Add</actionType><head><PrivateDescSeg3>10SH1904300025</PrivateDescSeg3><PrivateDescSeg4>2019-05-04 00:00:00</PrivateDescSeg4><PrivateDescSeg5>h368</PrivateDescSeg5><PrivateDescSeg6>2019-05-04 00:00:00</PrivateDescSeg6></head><details><detail><PrivateDescSeg3>40</PrivateDescSeg3><PrivateDescSeg4>2.0</PrivateDescSeg4><PrivateDescSeg5>2.0</PrivateDescSeg5><PrivateDescSeg6>2.0</PrivateDescSeg6><PrivateDescSeg7>0.0</PrivateDescSeg7><PrivateDescSeg8>0.0</PrivateDescSeg8><PrivateDescSeg9>10G01</PrivateDescSeg9><PrivateDescSeg10>TK181210</PrivateDescSeg10><PrivateDescSeg11>h368</PrivateDescSeg11><PrivateDescSeg12>2019-05-04 00:00:00</PrivateDescSeg12><DescFlexField_PrivateDescSeg6>CR190504000006</DescFlexField_PrivateDescSeg6><DescFlexField_PrivateDescSeg7>40</DescFlexField_PrivateDescSeg7><DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8></detail><detail><PrivateDescSeg3>30</PrivateDescSeg3><PrivateDescSeg4>2.0</PrivateDescSeg4><PrivateDescSeg5>2.0</PrivateDescSeg5><PrivateDescSeg6>2.0</PrivateDescSeg6><PrivateDescSeg7>0.0</PrivateDescSeg7><PrivateDescSeg8>0.0</PrivateDescSeg8><PrivateDescSeg9>10G01</PrivateDescSeg9><PrivateDescSeg10>TK181210</PrivateDescSeg10><PrivateDescSeg11>h368</PrivateDescSeg11><PrivateDescSeg12>2019-05-04 00:00:00</PrivateDescSeg12><DescFlexField_PrivateDescSeg6>CR190504000006</DescFlexField_PrivateDescSeg6><DescFlexField_PrivateDescSeg7>30</DescFlexField_PrivateDescSeg7><DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8></detail><detail><PrivateDescSeg3>20</PrivateDescSeg3><PrivateDescSeg4>2.0</PrivateDescSeg4><PrivateDescSeg5>2.0</PrivateDescSeg5><PrivateDescSeg6>2.0</PrivateDescSeg6><PrivateDescSeg7>0.0</PrivateDescSeg7><PrivateDescSeg8>0.0</PrivateDescSeg8><PrivateDescSeg9>10G01</PrivateDescSeg9><PrivateDescSeg10>TK181210</PrivateDescSeg10><PrivateDescSeg11>h368</PrivateDescSeg11><PrivateDescSeg12>2019-05-04 00:00:00</PrivateDescSeg12><DescFlexField_PrivateDescSeg6>CR190504000006</DescFlexField_PrivateDescSeg6><DescFlexField_PrivateDescSeg7>20</DescFlexField_PrivateDescSeg7><DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8></detail><detail><PrivateDescSeg3>10</PrivateDescSeg3><PrivateDescSeg4>2.0</PrivateDescSeg4><PrivateDescSeg5>2.0</PrivateDescSeg5><PrivateDescSeg6>2.0</PrivateDescSeg6><PrivateDescSeg7>0.0</PrivateDescSeg7><PrivateDescSeg8>0.0</PrivateDescSeg8><PrivateDescSeg9>10G01</PrivateDescSeg9><PrivateDescSeg10>TK181210</PrivateDescSeg10><PrivateDescSeg11>h368</PrivateDescSeg11><PrivateDescSeg12>2019-05-04 00:00:00</PrivateDescSeg12><DescFlexField_PrivateDescSeg6>CR190504000006</DescFlexField_PrivateDescSeg6><DescFlexField_PrivateDescSeg7>10</DescFlexField_PrivateDescSeg7><DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8></detail><detail><PrivateDescSeg3>50</PrivateDescSeg3><PrivateDescSeg4>2.0</PrivateDescSeg4><PrivateDescSeg5>2.0</PrivateDescSeg5><PrivateDescSeg6>2.0</PrivateDescSeg6><PrivateDescSeg7>0.0</PrivateDescSeg7><PrivateDescSeg8>0.0</PrivateDescSeg8><PrivateDescSeg9>10G01</PrivateDescSeg9><PrivateDescSeg10>TK181210</PrivateDescSeg10><PrivateDescSeg11>h368</PrivateDescSeg11><PrivateDescSeg12>2019-05-04 00:00:00</PrivateDescSeg12><DescFlexField_PrivateDescSeg6>CR190504000006</DescFlexField_PrivateDescSeg6><DescFlexField_PrivateDescSeg7>50</DescFlexField_PrivateDescSeg7><DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8></detail></details></item></body>");

               

                YYU9CustHSInterfaceICommonOperateSVClient client = new YYU9CustHSInterfaceICommonOperateSVClient();
                string result = client.Do(out outMessages, content, requestInfos.ToArray());
            });
        }
    }
}


