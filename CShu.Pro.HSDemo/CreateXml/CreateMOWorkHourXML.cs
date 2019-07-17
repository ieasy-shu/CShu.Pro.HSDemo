using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    public class CreateMOWorkHourXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "MOWorkHour";
            string actionType = "Modify";
            DateTime businessDate = DateTime.Now;

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //工时类型
            xmlBuilder.Append("<PrivateDescSeg3>NormalMO</PrivateDescSeg3>");
            //业务日期
            xmlBuilder.Append("<PrivateDescSeg4>2018.09.17</PrivateDescSeg4>");
            //生产订单
            xmlBuilder.Append("<PrivateDescSeg5>10MO2018-0059</PrivateDescSeg5>");
            //人工准备工时
            xmlBuilder.Append("<PrivateDescSeg6>2</PrivateDescSeg6>");
            //人工加工工时
            xmlBuilder.Append("<PrivateDescSeg7>2</PrivateDescSeg7>");
            //机器准备工时
            xmlBuilder.Append("<PrivateDescSeg8>2</PrivateDescSeg8>");
            //机器加工工时
            xmlBuilder.Append("<PrivateDescSeg9>2</PrivateDescSeg9>");
            //工时单位
            xmlBuilder.Append("<PrivateDescSeg10>Hour</PrivateDescSeg10>");
            //空闲工时
            xmlBuilder.Append("<PrivateDescSeg11>2</PrivateDescSeg11>");
            //组织
            xmlBuilder.Append("<PrivateDescSeg12>10</PrivateDescSeg12>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg13>HHH</PrivateDescSeg13>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg14>2018.09.17</PrivateDescSeg14>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg15>sugar</PrivateDescSeg15>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg16>2018.09.17</PrivateDescSeg16>");
            //全局段1（MES传入单号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg1>20182018</DescFlexField_PrivateDescSeg1>");
            //全局段2（MES传入行号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg2>10</DescFlexField_PrivateDescSeg2>");
            //全局段3（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg3>true</DescFlexField_PrivateDescSeg3>");
            xmlBuilder.Append("</head>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
