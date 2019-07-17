using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    /// <summary>
    /// 销售退回收货
    /// </summary>
    public class CreateRMAXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "RMA";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //日期				
            xmlBuilder.Append("<PrivateDescSeg4>2018-10-24</PrivateDescSeg4>");
            //入库确认日期				
            xmlBuilder.Append("<PrivateDescSeg5>2018-10-24</PrivateDescSeg5>");
            //退货（单据类型中收货单类型为销售退回收货单）
            xmlBuilder.Append("<PrivateDescSeg6>2</PrivateDescSeg6>");
            //创建人 				
            xmlBuilder.Append("<PrivateDescSeg7>cshu</PrivateDescSeg7>");
            //创建时间				
            xmlBuilder.Append("<PrivateDescSeg8>2018-10-24</PrivateDescSeg8>");
            //修改人				
            xmlBuilder.Append("<PrivateDescSeg9>cshu</PrivateDescSeg9>");
            //修改时间				
            xmlBuilder.Append("<PrivateDescSeg10>2018-10-24</PrivateDescSeg10>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");

            //来源单据类别				PrivateDescSeg4
            //来源单据号				PrivateDescSeg5
            //来源单据行号				PrivateDescSeg6
            //料号				PrivateDescSeg7
            //库存数量				PrivateDescSeg8
            //存储地点编码				PrivateDescSeg9
            //项目编码				PrivateDescSeg10
            //创建人				PrivateDescSeg11
            //创建时间				PrivateDescSeg12
            //修改人				PrivateDescSeg13
            //修改时间				PrivateDescSeg14
            
            //来源单据类别 销售退回处理单-8-RMA
            xmlBuilder.Append("<PrivateDescSeg4>8</PrivateDescSeg4>");
            //单行 来源单据号				
            xmlBuilder.Append("<PrivateDescSeg5>10RE181024001</PrivateDescSeg5>");
            //单行 来源单据行号				
            xmlBuilder.Append("<PrivateDescSeg6>10</PrivateDescSeg6>");
            //单行 料号				
            xmlBuilder.Append("<PrivateDescSeg7>11010040053</PrivateDescSeg7>");
            //单行 数量				
            xmlBuilder.Append("<PrivateDescSeg8>1</PrivateDescSeg8>");
            //单行 存储地点编码				
            xmlBuilder.Append("<PrivateDescSeg9>10B01</PrivateDescSeg9>");
            //单行 项目编码				
            xmlBuilder.Append("<PrivateDescSeg10></PrivateDescSeg10>");
            //单行 创建人				
            xmlBuilder.Append("<PrivateDescSeg11>cshu</PrivateDescSeg11>");
            //单行 创建时间				
            xmlBuilder.Append("<PrivateDescSeg12>2018-10-24</PrivateDescSeg12>");
            //单行 修改人				
            xmlBuilder.Append("<PrivateDescSeg13>cshu</PrivateDescSeg13>");
            //单行 修改时间				
            xmlBuilder.Append("<PrivateDescSeg14>2018-10-24</PrivateDescSeg14>");
            //单行 全局段3（WMS传入单号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>RCV2018102401</DescFlexField_PrivateDescSeg6>");
            //单行 全局段4（WMS传入行号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>10</DescFlexField_PrivateDescSeg7>");
            //单行 全局段5（WMS传入）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>true</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/
            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
