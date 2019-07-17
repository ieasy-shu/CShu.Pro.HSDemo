using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    public class CreateTransInFormXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "TransferIn";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //单据类型
            xmlBuilder.Append("<PrivateDescSeg3>DR02</PrivateDescSeg3>");
            //单号
            xmlBuilder.Append("<PrivateDescSeg4></PrivateDescSeg4>");
            //日期
            xmlBuilder.Append("<PrivateDescSeg5>2018-12-2</PrivateDescSeg5>");
            //调入类型
            xmlBuilder.Append("<PrivateDescSeg6>0</PrivateDescSeg6>");
            //调拨方向
            xmlBuilder.Append("<PrivateDescSeg7>0</PrivateDescSeg7>");
            //创建人				
            xmlBuilder.Append("<PrivateDescSeg8>cshu</PrivateDescSeg8>");
            //创建时间				
            xmlBuilder.Append("<PrivateDescSeg9>2018-12-2</PrivateDescSeg9>");
            //修改人				
            xmlBuilder.Append("<PrivateDescSeg10>cshu</PrivateDescSeg10>");
            //修改时间				
            xmlBuilder.Append("<PrivateDescSeg11>2018-10-12</PrivateDescSeg11>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //单行 行号				
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //单行 来源类别				
            xmlBuilder.Append("<PrivateDescSeg4>3</PrivateDescSeg4>");
            //单行 来源单据号				
            xmlBuilder.Append("<PrivateDescSeg5>3</PrivateDescSeg5>");
            //单行 来源单据行号				
            xmlBuilder.Append("<PrivateDescSeg6>3</PrivateDescSeg6>");
            //单行 料号（调入）				
            xmlBuilder.Append("<PrivateDescSeg7>test01</PrivateDescSeg7>");
            //单行 调入数量				
            xmlBuilder.Append("<PrivateDescSeg8>10</PrivateDescSeg8>");
            //单行 存储地点编码（调入）				
            xmlBuilder.Append("<PrivateDescSeg9>10B01</PrivateDescSeg9>");
            //单行 项目编码（调入）				
            xmlBuilder.Append("<PrivateDescSeg10></PrivateDescSeg10>");
            //单行 创建人				
            xmlBuilder.Append("<PrivateDescSeg11>cshu</PrivateDescSeg11>");
            //单行 创建时间				
            xmlBuilder.Append("<PrivateDescSeg12>2018-12-2</PrivateDescSeg12>");
            //单行 修改人				
            xmlBuilder.Append("<PrivateDescSeg13>cshu</PrivateDescSeg13>");
            //单行 修改时间				
            xmlBuilder.Append("<PrivateDescSeg14>2018-12-2</PrivateDescSeg14>");
            //子行 行号				
            xmlBuilder.Append("<PrivateDescSeg15>3</PrivateDescSeg15>");
            //子行 来源类别				
            xmlBuilder.Append("<PrivateDescSeg16>3</PrivateDescSeg16>");
            //子行 来源单据号				
            xmlBuilder.Append("<PrivateDescSeg17>3</PrivateDescSeg17>");
            //子行 来源单据行号				
            xmlBuilder.Append("<PrivateDescSeg18>3</PrivateDescSeg18>");
            //子行 料号（调出）				
            xmlBuilder.Append("<PrivateDescSeg19>11060020026</PrivateDescSeg19>");
            //子行 调出数量				
            xmlBuilder.Append("<PrivateDescSeg20>10</PrivateDescSeg20>");
            //子行 存储地点编码（调出）				
            xmlBuilder.Append("<PrivateDescSeg21>10A01</PrivateDescSeg21>");
            //子行 项目编码（调出）				
            xmlBuilder.Append("<PrivateDescSeg22></PrivateDescSeg22>");
            //子行 创建人				
            xmlBuilder.Append("<PrivateDescSeg23>cshu</PrivateDescSeg23>");
            //子行 创建时间				
            xmlBuilder.Append("<PrivateDescSeg24>2018-12-2</PrivateDescSeg24>");
            //子行 修改人				
            xmlBuilder.Append("<PrivateDescSeg25>cshu</PrivateDescSeg25>");
            //子行 修改时间				
            xmlBuilder.Append("<PrivateDescSeg26>2018-12-2</PrivateDescSeg26>");
            //单行 全局段1（WMS传入单号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg1>tra2018101201</DescFlexField_PrivateDescSeg1>");
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
