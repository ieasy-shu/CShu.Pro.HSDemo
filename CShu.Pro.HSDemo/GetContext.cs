using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFSoft.UBF.Util.Context;

namespace CShu.Pro.HSDemo
{
    public class GetContext
    {
        public static ThreadContext CreateContextObj()
        {
            //注：上下文的值一定动要和服务器对应上
            //string orgID = "1001410310010024";      //组织ID
            //string orgCode = "10";                  //组织Code
            //string enterpriseID = "0819";             //公司ID（ERP管理控制台【企业管理】里面的代码）
            //string userID = "1001410310010245";     //用户ID
            //string userCode = "admin";              //用户账号
            //string userName = "管理员";            //用户名称
            //string cultureName = "zh-CN";          //语言默认为中文

            //注：上下文的值一定动要和服务器对应上
            string orgID = "1001808140000058";      //组织ID 10【1001304150000212】  20【1001304150000875】
            string orgCode = "10";                  //组织Code
            string enterpriseID = "30";             //公司ID（ERP管理控制台【企业管理】里面的代码）
            string userID = "1001808033975177";     //用户ID
            string userCode = "admin";              //用户账号
            string userName = "MES";            //用户名称
            string cultureName = "zh-CN";          //语言默认为中文

            //string orgID = "1001410310010024";      //组织ID 10【1001304150000212】  20【1001304150000875】
            //string orgCode = "10";                  //组织Code
            //string enterpriseID = "0819";             //公司ID（ERP管理控制台【企业管理】里面的代码）
            //string userID = "1001410310010245";     //用户ID
            //string userCode = "admin";              //用户账号
            //string userName = "MES";            //用户名称
            //string cultureName = "zh-CN";          //语言默认为中文


            // 实例化应用上下文对象
            ThreadContext theContext = new ThreadContext();
            System.Collections.Generic.Dictionary<object, object> ns = new Dictionary<object, object>();

            ns.Add("OrgID", orgID);
            ns.Add("OrgCode", orgCode);
            ns.Add("EnterpriseID", enterpriseID);
            ns.Add("UserID", userID);
            ns.Add("UserCode", userCode);
            ns.Add("UserName", userName);
            ns.Add("CultureName", cultureName);
            ns.Add("DefaultCultureName", cultureName);
            ns.Add("Support_CultureNameList", cultureName);

            theContext.nameValueHas = ns;
            return theContext;
        }
    }
}
