using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    /// <summary>
    /// 材料出入库==生产领料、退料
    /// </summary>
    public class CreateRcvRptDocXML
    {
        public static string CreateRequestXml()
        {
            //string businessType = "MaterialDeliveryDoc";
            string businessType = "RcvRptDoc";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //材料入出库单头 单号
            //单据日期
            xmlBuilder.Append("<PrivateDescSeg4>2019-2-17</PrivateDescSeg4>");
            //入库时间
            xmlBuilder.Append("<PrivateDescSeg5>2019-2-17</PrivateDescSeg5>");
            //是否无完工申报直接入库
            xmlBuilder.Append("<PrivateDescSeg6>true</PrivateDescSeg6>");
            //收发类型
            xmlBuilder.Append("<PrivateDescSeg7>1</PrivateDescSeg7>");
            //创建人				
            xmlBuilder.Append("<PrivateDescSeg8>H536</PrivateDescSeg8>");
            //创建时间				
            xmlBuilder.Append("<PrivateDescSeg9>2019-2-17</PrivateDescSeg9>");
            //修改人				
            xmlBuilder.Append("<PrivateDescSeg10>H536</PrivateDescSeg10>");
            //修改时间				
            xmlBuilder.Append("<PrivateDescSeg11>2019-2-17</PrivateDescSeg11>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //单行 行号				
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //单行 生产订单				
            xmlBuilder.Append("<PrivateDescSeg4>10MO190108015</PrivateDescSeg4>");
            //单行 入库数量				
            xmlBuilder.Append("<PrivateDescSeg5>1</PrivateDescSeg5>");
            //单行 产出类型				
            xmlBuilder.Append("<PrivateDescSeg6>0</PrivateDescSeg6>");
            //单行 存储类型				
            xmlBuilder.Append("<PrivateDescSeg7>4</PrivateDescSeg7>");
            //存储地点
            xmlBuilder.Append("<PrivateDescSeg8>10E01</PrivateDescSeg8>");
            //单行 项目				
            xmlBuilder.Append("<PrivateDescSeg9></PrivateDescSeg9>");
            xmlBuilder.Append("<PrivateDescSeg10>Co-0004</PrivateDescSeg10>");
            xmlBuilder.Append("<PrivateDescSeg11>10</PrivateDescSeg11>");
           
            //单行 全局段1（WMS传入单号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg1>Co-0004</DescFlexField_PrivateDescSeg1>");
            //单行 全局段2（WMS传入行号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg2>10</DescFlexField_PrivateDescSeg2>");
            //单行 全局段3（WMS传入）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg3>true</DescFlexField_PrivateDescSeg3>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/
          
            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
