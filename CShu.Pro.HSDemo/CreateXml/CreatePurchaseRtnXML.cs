using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    public class CreatePurchaseRtnXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "RcvRtn";
            string actionType = "Modify";
            DateTime time = DateTime.Now;
            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //单号
            xmlBuilder.Append("<PrivateDescSeg3>10SH181215004</PrivateDescSeg3>");
            //确认日期
            xmlBuilder.Append("<PrivateDescSeg4>" + time + "</PrivateDescSeg4>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg5>cshu</PrivateDescSeg5>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg6>" + time + "</PrivateDescSeg6>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //行号
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //拒收数量
            xmlBuilder.Append("<PrivateDescSeg4>10</PrivateDescSeg4>");
            //退扣数量
            xmlBuilder.Append("<PrivateDescSeg5>0</PrivateDescSeg5>");
            //退补数量
            xmlBuilder.Append("<PrivateDescSeg6>10</PrivateDescSeg6>");
            //存储地点编码
            xmlBuilder.Append("<PrivateDescSeg7>10B01</PrivateDescSeg7>");
            //项目编码
            xmlBuilder.Append("<PrivateDescSeg8>HSQD30001</PrivateDescSeg8>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg9>cshu</PrivateDescSeg9>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg10>" + time + "</PrivateDescSeg10>");
            //全局段6（WMS传入单号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>111111</DescFlexField_PrivateDescSeg6>");
            //全局段7（WMS传入行号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>1</DescFlexField_PrivateDescSeg7>");
            //全局段8（WMS传入)
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/

            ///***第一行开始***/
            //xmlBuilder.Append("<detail>");
            ////行号
            //xmlBuilder.Append("<PrivateDescSeg3>20</PrivateDescSeg3>");
            ////拒收数量
            //xmlBuilder.Append("<PrivateDescSeg4>2</PrivateDescSeg4>");
            ////退扣数量
            //xmlBuilder.Append("<PrivateDescSeg5>0</PrivateDescSeg5>");
            ////退补数量
            //xmlBuilder.Append("<PrivateDescSeg6>2</PrivateDescSeg6>");
            ////存储地点编码
            //xmlBuilder.Append("<PrivateDescSeg7>10B01</PrivateDescSeg7>");
            ////项目编码
            //xmlBuilder.Append("<PrivateDescSeg8>HSQD30001</PrivateDescSeg8>");
            ////修改人
            //xmlBuilder.Append("<PrivateDescSeg9>cshu</PrivateDescSeg9>");
            ////修改时间
            //xmlBuilder.Append("<PrivateDescSeg10>" + time + "</PrivateDescSeg10>");
            ////全局段6（WMS传入单号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>111111</DescFlexField_PrivateDescSeg6>");
            ////全局段7（WMS传入行号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>1</DescFlexField_PrivateDescSeg7>");
            ////全局段8（WMS传入)
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8>");
            //xmlBuilder.Append("</detail>");
            ///***第一行结束***/

            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
