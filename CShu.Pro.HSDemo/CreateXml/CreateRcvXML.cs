using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    public class CreateRcvXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "Rcv";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //单号
            xmlBuilder.Append("<PrivateDescSeg3>10SH1904130006</PrivateDescSeg3>");
            //入库时间
            xmlBuilder.Append("<PrivateDescSeg4>2019.04.13</PrivateDescSeg4>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg5>admin</PrivateDescSeg5>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg6>2019.04.13</PrivateDescSeg6>");
            xmlBuilder.Append("</head>");
            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //行号
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //实到数量1
            xmlBuilder.Append("<PrivateDescSeg4>100</PrivateDescSeg4>");
            //点收数量1
            xmlBuilder.Append("<PrivateDescSeg5>100</PrivateDescSeg5>");
            //实收数量1
            xmlBuilder.Append("<PrivateDescSeg6>100</PrivateDescSeg6>");
            //退扣数量1
            xmlBuilder.Append("<PrivateDescSeg7>0</PrivateDescSeg7>");
            //退补数量1
            xmlBuilder.Append("<PrivateDescSeg8>0</PrivateDescSeg8>");
            //存储地点编码
            xmlBuilder.Append("<PrivateDescSeg9>10A01</PrivateDescSeg9>");
            //项目编码
            xmlBuilder.Append("<PrivateDescSeg10></PrivateDescSeg10>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg11>admin</PrivateDescSeg11>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg12>2019/04/13</PrivateDescSeg12>");
            //全局段6（WMS传入单号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>110001</DescFlexField_PrivateDescSeg6>");
            //全局段7（WMS传入行号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>10</DescFlexField_PrivateDescSeg7>");
            //全局段8（WMS传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/

            /////***第二行开始***/
            //xmlBuilder.Append("<detail>");
            ////行号
            //xmlBuilder.Append("<PrivateDescSeg3>20</PrivateDescSeg3>");
            ////实到数量1
            //xmlBuilder.Append("<PrivateDescSeg4>5</PrivateDescSeg4>");
            ////点收数量1
            //xmlBuilder.Append("<PrivateDescSeg5>5</PrivateDescSeg5>");
            ////实收数量1
            //xmlBuilder.Append("<PrivateDescSeg6>5</PrivateDescSeg6>");
            ////退扣数量1
            //xmlBuilder.Append("<PrivateDescSeg7>0</PrivateDescSeg7>");
            ////退补数量1
            //xmlBuilder.Append("<PrivateDescSeg8>0</PrivateDescSeg8>");
            ////存储地点编码
            //xmlBuilder.Append("<PrivateDescSeg9>10A01</PrivateDescSeg9>");
            ////项目编码
            //xmlBuilder.Append("<PrivateDescSeg10></PrivateDescSeg10>");
            ////修改人
            //xmlBuilder.Append("<PrivateDescSeg11>cshu</PrivateDescSeg11>");
            ////修改时间
            //xmlBuilder.Append("<PrivateDescSeg12>2019/01/08</PrivateDescSeg12>");
            ////全局段6（WMS传入单号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>11002</DescFlexField_PrivateDescSeg6>");
            ////全局段7（WMS传入行号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>20</DescFlexField_PrivateDescSeg7>");
            ////全局段8（WMS传入）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8>");
            //xmlBuilder.Append("</detail>");
            //***第二行结束***/


            ///***第三行开始***/
            //xmlBuilder.Append("<detail>");
            ////行号
            //xmlBuilder.Append("<PrivateDescSeg3>30</PrivateDescSeg3>");
            ////实到数量1
            //xmlBuilder.Append("<PrivateDescSeg4>40</PrivateDescSeg4>");
            ////点收数量1
            //xmlBuilder.Append("<PrivateDescSeg5>20</PrivateDescSeg5>");
            ////实收数量1
            //xmlBuilder.Append("<PrivateDescSeg6>20</PrivateDescSeg6>");
            ////退扣数量1
            //xmlBuilder.Append("<PrivateDescSeg7>20</PrivateDescSeg7>");
            ////退补数量1
            //xmlBuilder.Append("<PrivateDescSeg8>0</PrivateDescSeg8>");
            ////存储地点编码
            //xmlBuilder.Append("<PrivateDescSeg9>10A01</PrivateDescSeg9>");
            ////项目编码
            //xmlBuilder.Append("<PrivateDescSeg10></PrivateDescSeg10>");
            ////修改人
            //xmlBuilder.Append("<PrivateDescSeg11>cshu</PrivateDescSeg11>");
            ////修改时间
            //xmlBuilder.Append("<PrivateDescSeg12>2019/01/08</PrivateDescSeg12>");
            ////全局段6（WMS传入单号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>11003</DescFlexField_PrivateDescSeg6>");
            ////全局段7（WMS传入行号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>30</DescFlexField_PrivateDescSeg7>");
            ////全局段8（WMS传入）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8>");
            //xmlBuilder.Append("</detail>");
            //***第三行结束***/
            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
