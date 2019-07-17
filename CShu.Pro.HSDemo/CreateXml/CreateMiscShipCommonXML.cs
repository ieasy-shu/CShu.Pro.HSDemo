using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    /// <summary>
    /// 杂发
    /// </summary>
    public class CreateMiscShipCommonXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "MiscShipment2";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //单号
            xmlBuilder.Append("<PrivateDescSeg3>10ZF190223003</PrivateDescSeg3>");
            //日期
            xmlBuilder.Append("<PrivateDescSeg4>2019-1-11</PrivateDescSeg4>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg5>admin</PrivateDescSeg5>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg6>2019-1-11</PrivateDescSeg6>");
            xmlBuilder.Append("</head>");
            //单据明细信息
            xmlBuilder.Append("<details>");
            
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //行号
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //库存数量
            xmlBuilder.Append("<PrivateDescSeg4>120</PrivateDescSeg4>");
            //存储地点编码
            xmlBuilder.Append("<PrivateDescSeg5>10J01</PrivateDescSeg5>");
            //项目编码
            xmlBuilder.Append("<PrivateDescSeg6></PrivateDescSeg6>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg7>admin</PrivateDescSeg7>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg8>2019-1-11</PrivateDescSeg8>");
            //单行 全局段5（WMS传入单号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>HS201810101</DescFlexField_PrivateDescSeg5>");
            //单行 全局段6（WMS传入行号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>10</DescFlexField_PrivateDescSeg6>");
            //单行 全局段7（WMS传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>true</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/

            /***第二行开始***/
            xmlBuilder.Append("<detail>");
            //行号
            xmlBuilder.Append("<PrivateDescSeg3>15</PrivateDescSeg3>");
            //库存数量
            xmlBuilder.Append("<PrivateDescSeg4>22</PrivateDescSeg4>");
            //存储地点编码
            xmlBuilder.Append("<PrivateDescSeg5>10B01</PrivateDescSeg5>");
            //项目编码
            xmlBuilder.Append("<PrivateDescSeg6></PrivateDescSeg6>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg7>admin</PrivateDescSeg7>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg8>2019-1-11</PrivateDescSeg8>");
            //单行 全局段5（WMS传入单号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>HS201810102</DescFlexField_PrivateDescSeg5>");
            //单行 全局段6（WMS传入行号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>20</DescFlexField_PrivateDescSeg6>");
            //单行 全局段7（WMS传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>true</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("</detail>");
            /***第二行结束***/

            ///***第三行开始***/
            //xmlBuilder.Append("<detail>");
            ////行号
            //xmlBuilder.Append("<PrivateDescSeg3>30</PrivateDescSeg3>");
            ////库存数量
            //xmlBuilder.Append("<PrivateDescSeg4>6</PrivateDescSeg4>");
            ////存储地点编码
            //xmlBuilder.Append("<PrivateDescSeg5>10B01</PrivateDescSeg5>");
            ////项目编码
            //xmlBuilder.Append("<PrivateDescSeg6></PrivateDescSeg6>");
            ////修改人
            //xmlBuilder.Append("<PrivateDescSeg7>cshu</PrivateDescSeg7>");
            ////修改时间
            //xmlBuilder.Append("<PrivateDescSeg8>2018-10-10</PrivateDescSeg8>");
            ////单行 全局段5（WMS传入单号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>HS201810103</DescFlexField_PrivateDescSeg5>");
            ////单行 全局段6（WMS传入行号）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>30</DescFlexField_PrivateDescSeg6>");
            ////单行 全局段7（WMS传入）
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>true</DescFlexField_PrivateDescSeg7>");
            //xmlBuilder.Append("</detail>");
            ///***第三行结束***/
            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
