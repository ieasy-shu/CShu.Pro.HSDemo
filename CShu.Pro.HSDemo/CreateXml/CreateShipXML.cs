using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    /// <summary>
    /// 标准出货
    /// </summary>
    public class CreateShipXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "Ship";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //材料入出库单头 单号
            //客户编码				
            xmlBuilder.Append("<PrivateDescSeg2>0911</PrivateDescSeg2>");
            //日期				
            xmlBuilder.Append("<PrivateDescSeg3>2018-10-24</PrivateDescSeg3>");
            //出货确认日
            xmlBuilder.Append("<PrivateDescSeg4>2018-10-24</PrivateDescSeg4>");
            //创建人				
            xmlBuilder.Append("<PrivateDescSeg5>cshu</PrivateDescSeg5>");
            //创建时间				
            xmlBuilder.Append("<PrivateDescSeg6>2018-10-24</PrivateDescSeg6>");
            //修改人				
            xmlBuilder.Append("<PrivateDescSeg7>cshu</PrivateDescSeg7>");
            //修改时间				
            xmlBuilder.Append("<PrivateDescSeg8>2018-10-24</PrivateDescSeg8>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");

            //行号
            xmlBuilder.Append("<PrivateDescSeg1>10</PrivateDescSeg1>");
            //来源单据类型2-出货计划
            xmlBuilder.Append("<PrivateDescSeg2>2</PrivateDescSeg2>");
            //单行 来源单据号				
            xmlBuilder.Append("<PrivateDescSeg3>10SH181024004</PrivateDescSeg3>");
            //单行 来源计划行号				
            xmlBuilder.Append("<PrivateDescSeg4>10</PrivateDescSeg4>");
            //单行 料号				
            xmlBuilder.Append("<PrivateDescSeg5>11010040053</PrivateDescSeg5>");
            //单行 数量				
            xmlBuilder.Append("<PrivateDescSeg6>1</PrivateDescSeg6>");
            //单行 存储地点编码				
            xmlBuilder.Append("<PrivateDescSeg7>10B01</PrivateDescSeg7>");
            //单行 项目编码				
            xmlBuilder.Append("<PrivateDescSeg8></PrivateDescSeg8>");
            //单行 创建人				
            xmlBuilder.Append("<PrivateDescSeg9>cshu</PrivateDescSeg9>");
            //单行 创建时间				
            xmlBuilder.Append("<PrivateDescSeg10>2018-10-24</PrivateDescSeg10>");
            //单行 修改人				
            xmlBuilder.Append("<PrivateDescSeg11>cshu</PrivateDescSeg11>");
            //单行 修改时间				
            xmlBuilder.Append("<PrivateDescSeg12>2018-10-24</PrivateDescSeg12>");
            //单行 全局段3（WMS传入单号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg3>S2018102401</DescFlexField_PrivateDescSeg3>");
            //单行 全局段4（WMS传入行号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg4>10</DescFlexField_PrivateDescSeg4>");
            //单行 全局段5（WMS传入）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>true</DescFlexField_PrivateDescSeg5>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/
            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
