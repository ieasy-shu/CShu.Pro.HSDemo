using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    /// <summary>
    /// 委外领料、委外退料
    /// </summary>
    public class CreatePMIssueXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "PMIssue";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //单号
            xmlBuilder.Append("<PrivateDescSeg3>10WF190102001</PrivateDescSeg3>");
            //日期
            xmlBuilder.Append("<PrivateDescSeg4>2018-12-02</PrivateDescSeg4>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg5>cshu</PrivateDescSeg5>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg6>2018-12-02</PrivateDescSeg6>");
            xmlBuilder.Append("</head>");
            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //单行 行号
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //单行 数量
            xmlBuilder.Append("<PrivateDescSeg4>2</PrivateDescSeg4>");
            //单行 存储地点编码
            xmlBuilder.Append("<PrivateDescSeg5>10C01</PrivateDescSeg5>");
            //单行 项目编码
            xmlBuilder.Append("<PrivateDescSeg6></PrivateDescSeg6>");
            //单行 修改人
            xmlBuilder.Append("<PrivateDescSeg7>cshu</PrivateDescSeg7>");
            //单行 修改时间
            xmlBuilder.Append("<PrivateDescSeg8>2018-12-02</PrivateDescSeg8>");
            //单行 全局段5（WMS传入单号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>2018-12-02</DescFlexField_PrivateDescSeg5>");
            //单行 全局段6（WMS传入行号）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>10</DescFlexField_PrivateDescSeg6>");
            //单行 全局段7（WMS传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>true</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/
            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
