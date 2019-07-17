using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    /// <summary>
    /// 材料入库
    /// </summary>
    public class CreateIssueFormRedFlushXML
    {
        public static string CreateRequestXml()
        {
            //string businessType = "MaterialDeliveryDoc";
            string businessType = "MaterialDeliveryDocRedFlush";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            //材料入出库单头 单号
            //日期
            xmlBuilder.Append("<PrivateDescSeg4>2018-11-27</PrivateDescSeg4>");
            //确认日期
            xmlBuilder.Append("<PrivateDescSeg5>2018-11-27</PrivateDescSeg5>");
            //收发类型
            xmlBuilder.Append("<PrivateDescSeg6>0</PrivateDescSeg6>");
            //超额
            xmlBuilder.Append("<PrivateDescSeg7>true</PrivateDescSeg7>");
            //创建人				
            xmlBuilder.Append("<PrivateDescSeg8>cshu</PrivateDescSeg8>");
            //创建时间				
            xmlBuilder.Append("<PrivateDescSeg9>2018-11-27</PrivateDescSeg9>");
            //修改人				
            xmlBuilder.Append("<PrivateDescSeg10>cshu</PrivateDescSeg10>");
            //修改时间				
            xmlBuilder.Append("<PrivateDescSeg11>2018-11-27</PrivateDescSeg11>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //单行 行号				
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //单行 生产订单				
            xmlBuilder.Append("<PrivateDescSeg4>10MO181127002</PrivateDescSeg4>");
            //单行 生产订单备料行号				
            xmlBuilder.Append("<PrivateDescSeg5>20</PrivateDescSeg5>");
            //单行 料号				
            xmlBuilder.Append("<PrivateDescSeg6>11060020026</PrivateDescSeg6>");
            //单行 应发时间				
            xmlBuilder.Append("<PrivateDescSeg7>2018-11-27</PrivateDescSeg7>");
            //单行 VMI标识				
            xmlBuilder.Append("<PrivateDescSeg8>true</PrivateDescSeg8>");
            //单行 应发数量（发料单位）			
            xmlBuilder.Append("<PrivateDescSeg9>3</PrivateDescSeg9>");
            //单行 材料出库数量（发料单位）			
            xmlBuilder.Append("<PrivateDescSeg10>3</PrivateDescSeg10>");
            //单行 供应地点编码				
            xmlBuilder.Append("<PrivateDescSeg11>10B01</PrivateDescSeg11>");
            //单行 项目编码				
            xmlBuilder.Append("<PrivateDescSeg12>10KH180829001</PrivateDescSeg12>");
            //单行 退料理由（收发类型为材料入库时填写）				
            xmlBuilder.Append("<PrivateDescSeg13></PrivateDescSeg13>");
            //单行 创建人				
            xmlBuilder.Append("<PrivateDescSeg14></PrivateDescSeg14>");
            //单行 创建时间				
            xmlBuilder.Append("<PrivateDescSeg15></PrivateDescSeg15>");
            //单行 修改人				
            xmlBuilder.Append("<PrivateDescSeg16></PrivateDescSeg16>");
            //单行 修改时间				
            xmlBuilder.Append("<PrivateDescSeg17></PrivateDescSeg17>");
            //单行 来源出库单行号				
            xmlBuilder.Append("<PrivateDescSeg18>10</PrivateDescSeg18>");
            //单行 来源出库单号		
            xmlBuilder.Append("<PrivateDescSeg19>10CC181127005</PrivateDescSeg19>");
            //单行 全局段1（WMS传入单号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg1>MA2018101501</DescFlexField_PrivateDescSeg1>");
            //单行 全局段2（WMS传入行号）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg2>10</DescFlexField_PrivateDescSeg2>");
            //单行 全局段3（WMS传入）				
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg3>true</DescFlexField_PrivateDescSeg3>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/
            ///***第二行开始***/
            //xmlBuilder.Append("<detail>");
            ////单行 行号				
            //xmlBuilder.Append("<PrivateDescSeg3>20</PrivateDescSeg3>");
            ////单行 生产订单				
            //xmlBuilder.Append("<PrivateDescSeg4>10MO2018-0122</PrivateDescSeg4>");
            ////单行 生产订单备料行号				
            //xmlBuilder.Append("<PrivateDescSeg5>10</PrivateDescSeg5>");
            ////单行 料号				
            //xmlBuilder.Append("<PrivateDescSeg6>13010030994</PrivateDescSeg6>");
            ////单行 应发时间				
            //xmlBuilder.Append("<PrivateDescSeg7>2018-10-15</PrivateDescSeg7>");
            ////单行 VMI标识				
            //xmlBuilder.Append("<PrivateDescSeg8>10</PrivateDescSeg8>");
            ////单行 应发数量（发料单位）			
            //xmlBuilder.Append("<PrivateDescSeg9>0.01</PrivateDescSeg9>");
            ////单行 材料出库数量（发料单位）			
            //xmlBuilder.Append("<PrivateDescSeg10>0.01</PrivateDescSeg10>");
            ////单行 供应地点编码				
            //xmlBuilder.Append("<PrivateDescSeg11></PrivateDescSeg11>");
            ////单行 项目编码				
            //xmlBuilder.Append("<PrivateDescSeg12></PrivateDescSeg12>");
            ////单行 退料理由（收发类型为材料入库时填写）				
            //xmlBuilder.Append("<PrivateDescSeg13></PrivateDescSeg13>");
            ////单行 创建人				
            //xmlBuilder.Append("<PrivateDescSeg14></PrivateDescSeg14>");
            ////单行 创建时间				
            //xmlBuilder.Append("<PrivateDescSeg15></PrivateDescSeg15>");
            ////单行 修改人				
            //xmlBuilder.Append("<PrivateDescSeg16></PrivateDescSeg16>");
            ////单行 修改时间				
            //xmlBuilder.Append("<PrivateDescSeg17></PrivateDescSeg17>");
            ////单行 全局段1（WMS传入单号）				
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg1>MA2018101501</DescFlexField_PrivateDescSeg1>");
            ////单行 全局段2（WMS传入行号）				
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg2>10</DescFlexField_PrivateDescSeg2>");
            ////单行 全局段3（WMS传入）				
            //xmlBuilder.Append("<DescFlexField_PrivateDescSeg3>true</DescFlexField_PrivateDescSeg3>");
            //xmlBuilder.Append("</detail>");
            ///***第二行结束***/

            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
