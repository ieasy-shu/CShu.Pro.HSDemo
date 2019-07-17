using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    public class CreateAntiStartMOXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "AntiStartMO";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //单号
            xmlBuilder.Append("<PrivateDescSeg2>10MO190220006</PrivateDescSeg2>");
            //反开工数量
            xmlBuilder.Append("<PrivateDescSeg3>2</PrivateDescSeg3>");
            //开工时间
            xmlBuilder.Append("<PrivateDescSeg4>2019.02.23 17:06:34</PrivateDescSeg4>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg5>cshu</PrivateDescSeg5>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg6>2019-03-02</PrivateDescSeg6>");
            //全局段1（MES传入单号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg1>20180914</DescFlexField_PrivateDescSeg1>");
            //全局段2（MES传入行号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg2>10</DescFlexField_PrivateDescSeg2>");
            //全局段2（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg3>true</DescFlexField_PrivateDescSeg3>");
            xmlBuilder.Append("</head>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");
            return xmlBuilder.ToString();
        }
    }
}
