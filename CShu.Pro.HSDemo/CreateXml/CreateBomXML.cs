using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.HSDemo
{
    public class CreateBomXML
    {
        public static string CreateRequestXml()
        {
            string businessType = "BomCreate";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");


           
            //单据头部信息
            xmlBuilder.Append("<head>");
            //组织
            xmlBuilder.Append("<PrivateDescSeg2>Add</PrivateDescSeg2>");
            //组织
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //母件料号
            xmlBuilder.Append("<PrivateDescSeg4>11010010026</PrivateDescSeg4>");
            //项目编码
            xmlBuilder.Append("<PrivateDescSeg5></PrivateDescSeg5>");
            //生产目的
            xmlBuilder.Append("<PrivateDescSeg6>0</PrivateDescSeg6>");
            //版本号
            xmlBuilder.Append("<PrivateDescSeg7>AA.02</PrivateDescSeg7>");
            //生效日期
            xmlBuilder.Append("<PrivateDescSeg8>2019.1.25</PrivateDescSeg8>");
            //失效日期
            xmlBuilder.Append("<PrivateDescSeg9>9999.12.31</PrivateDescSeg9>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg10>admin</PrivateDescSeg10>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg11>2019.4.4</PrivateDescSeg11>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg12>admin</PrivateDescSeg12>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg13>2019.4.4</PrivateDescSeg13>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");
            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //子件项次
            xmlBuilder.Append("<PrivateDescSeg3>10</PrivateDescSeg3>");
            //子件料号
            xmlBuilder.Append("<PrivateDescSeg4>11010010002</PrivateDescSeg4>");
            //变动数量
            xmlBuilder.Append("<PrivateDescSeg5>1</PrivateDescSeg5>");
            xmlBuilder.Append("<PrivateDescSeg6>2019.4.4</PrivateDescSeg6>");
            xmlBuilder.Append("<PrivateDescSeg7>9999.12.31</PrivateDescSeg7>");
            xmlBuilder.Append("<PrivateDescSeg8>5</PrivateDescSeg8>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg9>admin</PrivateDescSeg9>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg10>2019.4.4</PrivateDescSeg10>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg11>admin</PrivateDescSeg11>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg12>2019.4.4</PrivateDescSeg12>");
            //全局段5（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>true</DescFlexField_PrivateDescSeg5>");
            //全局段6（工位）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>G10001</DescFlexField_PrivateDescSeg6>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>20190404</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>10</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg10>AA.10</DescFlexField_PrivateDescSeg10>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/

            ///***第二行开始***/
            xmlBuilder.Append("<detail>");
            //子件项次
            xmlBuilder.Append("<PrivateDescSeg3>20</PrivateDescSeg3>");
            //子件料号
            xmlBuilder.Append("<PrivateDescSeg4>11010010003</PrivateDescSeg4>");
            //变动数量
            xmlBuilder.Append("<PrivateDescSeg5>1</PrivateDescSeg5>");
            xmlBuilder.Append("<PrivateDescSeg6>2019.2.4</PrivateDescSeg6>");
            xmlBuilder.Append("<PrivateDescSeg7>9999.12.31</PrivateDescSeg7>");
            xmlBuilder.Append("<PrivateDescSeg8>5</PrivateDescSeg8>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg9>admin</PrivateDescSeg9>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg10>2019.1.10</PrivateDescSeg10>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg11>admin</PrivateDescSeg11>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg12>2019.2.10</PrivateDescSeg12>");
            //全局段5（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>true</DescFlexField_PrivateDescSeg5>");
            //全局段6（工位）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>G10003</DescFlexField_PrivateDescSeg6>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>20190404</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>20</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg10>AA.10</DescFlexField_PrivateDescSeg10>");
            xmlBuilder.Append("</detail>");
            ///***第二行结束***/
            ///
            ///***第二行开始***/
            xmlBuilder.Append("<detail>");
            //子件项次
            xmlBuilder.Append("<PrivateDescSeg3>30</PrivateDescSeg3>");
            //子件料号
            xmlBuilder.Append("<PrivateDescSeg4>11010010004</PrivateDescSeg4>");
            //变动数量
            xmlBuilder.Append("<PrivateDescSeg5>3</PrivateDescSeg5>");
            xmlBuilder.Append("<PrivateDescSeg6>2019.1.10</PrivateDescSeg6>");
            xmlBuilder.Append("<PrivateDescSeg7>9999.12.31</PrivateDescSeg7>");
            xmlBuilder.Append("<PrivateDescSeg8>5</PrivateDescSeg8>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg9>admin</PrivateDescSeg9>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg10>2019.1.10</PrivateDescSeg10>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg11>admin</PrivateDescSeg11>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg12>2019.1.10</PrivateDescSeg12>");
            //全局段5（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>true</DescFlexField_PrivateDescSeg5>");
            //全局段6（工位）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>G19993</DescFlexField_PrivateDescSeg6>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>20190404</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>30</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg10>AA.10</DescFlexField_PrivateDescSeg10>");
            xmlBuilder.Append("</detail>");

            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            //子件项次
            xmlBuilder.Append("<PrivateDescSeg3>40</PrivateDescSeg3>");
            //子件料号
            xmlBuilder.Append("<PrivateDescSeg4>11010010002</PrivateDescSeg4>");
            //变动数量
            xmlBuilder.Append("<PrivateDescSeg5>1</PrivateDescSeg5>");
            xmlBuilder.Append("<PrivateDescSeg6>2019.4.4</PrivateDescSeg6>");
            xmlBuilder.Append("<PrivateDescSeg7>9999.12.31</PrivateDescSeg7>");
            xmlBuilder.Append("<PrivateDescSeg8>5</PrivateDescSeg8>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg9>admin</PrivateDescSeg9>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg10>2019.4.4</PrivateDescSeg10>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg11>admin</PrivateDescSeg11>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg12>2019.4.4</PrivateDescSeg12>");
            //全局段5（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>true</DescFlexField_PrivateDescSeg5>");
            //全局段6（工位）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>G10001</DescFlexField_PrivateDescSeg6>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>20190404</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>40</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg10>AA.10</DescFlexField_PrivateDescSeg10>");
            xmlBuilder.Append("</detail>");
            /***第一行结束***/

            ///***第二行开始***/
            xmlBuilder.Append("<detail>");
            //子件项次
            xmlBuilder.Append("<PrivateDescSeg3>50</PrivateDescSeg3>");
            //子件料号
            xmlBuilder.Append("<PrivateDescSeg4>11010010003</PrivateDescSeg4>");
            //变动数量
            xmlBuilder.Append("<PrivateDescSeg5>1</PrivateDescSeg5>");
            xmlBuilder.Append("<PrivateDescSeg6>2019.2.4</PrivateDescSeg6>");
            xmlBuilder.Append("<PrivateDescSeg7>9999.12.31</PrivateDescSeg7>");
            xmlBuilder.Append("<PrivateDescSeg8>5</PrivateDescSeg8>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg9>admin</PrivateDescSeg9>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg10>2019.1.10</PrivateDescSeg10>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg11>admin</PrivateDescSeg11>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg12>2019.2.10</PrivateDescSeg12>");
            //全局段5（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>true</DescFlexField_PrivateDescSeg5>");
            //全局段6（工位）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>G10003</DescFlexField_PrivateDescSeg6>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>20190404</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>50</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg10>AA.10</DescFlexField_PrivateDescSeg10>");
            xmlBuilder.Append("</detail>");
            ///***第二行结束***/
            ///
            ///***第二行开始***/
            xmlBuilder.Append("<detail>");
            //子件项次
            xmlBuilder.Append("<PrivateDescSeg3>60</PrivateDescSeg3>");
            //子件料号
            xmlBuilder.Append("<PrivateDescSeg4>11010010004</PrivateDescSeg4>");
            //变动数量
            xmlBuilder.Append("<PrivateDescSeg5>3</PrivateDescSeg5>");
            xmlBuilder.Append("<PrivateDescSeg6>2019.1.10</PrivateDescSeg6>");
            xmlBuilder.Append("<PrivateDescSeg7>9999.12.31</PrivateDescSeg7>");
            xmlBuilder.Append("<PrivateDescSeg8>5</PrivateDescSeg8>");
            //创建人
            xmlBuilder.Append("<PrivateDescSeg9>admin</PrivateDescSeg9>");
            //创建时间
            xmlBuilder.Append("<PrivateDescSeg10>2019.1.10</PrivateDescSeg10>");
            //修改人
            xmlBuilder.Append("<PrivateDescSeg11>admin</PrivateDescSeg11>");
            //修改时间
            xmlBuilder.Append("<PrivateDescSeg12>2019.1.10</PrivateDescSeg12>");
            //全局段5（MES传入）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg5>true</DescFlexField_PrivateDescSeg5>");
            //全局段6（工位）
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg6>G19993</DescFlexField_PrivateDescSeg6>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg7>20190404</DescFlexField_PrivateDescSeg7>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg8>60</DescFlexField_PrivateDescSeg8>");
            xmlBuilder.Append("<DescFlexField_PrivateDescSeg10>AA.10</DescFlexField_PrivateDescSeg10>");
            xmlBuilder.Append("</detail>");

            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }
    }
}
