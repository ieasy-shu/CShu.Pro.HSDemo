using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UFSoft.UBF.Service;

namespace CShu.Pro.HSDemo
{
    public class ExceptionHelper
    {
        public static void TryCatch(Action act)
        {
            Type tt=null;
            try
            {
                string method=act.Method.ReflectedType.Name;
                tt = act.GetType();
                act.Invoke();
            }
            catch (Exception ex)
            {
                Type t = act.GetType();
                string name = tt.Name;
                string exMsg = GetExceptionMessage(ex);
                throw new Exception(exMsg + name);
            }
        }

        /// <summary>
        /// 提取异常信息
        /// </summary>
        /// <param name="ex"></param>
        private static string GetExceptionMessage(Exception ex)
        {
            string faultMessage = "未知错误，请查看ERP日志！";
            System.TimeoutException timeoutEx = ex as System.TimeoutException;
            if (timeoutEx != null)
            {
                faultMessage = "因第一次访问ERP服务，访问超时，如避免此错误，请先启动ERP系统！";
            }
            else
            {
                FaultException<ServiceException> faultEx = ex as FaultException<ServiceException>;
                if (faultEx == null)
                {
                    faultMessage = ex.Message;
                }
                else
                {
                    ServiceException serviceEx = faultEx.Detail;
                    if (serviceEx != null && !string.IsNullOrEmpty(serviceEx.Message) && !serviceEx.Message.Equals("fault", StringComparison.OrdinalIgnoreCase))
                    {
                        // 错误信息在faultEx.Message中，请提取，
                        // 格式为"Fault:料品不能为空，请录入\n 在....."
                        int startIndex = serviceEx.Message.IndexOf(":");
                        int endIndex = serviceEx.Message.IndexOf("\n");
                        if (endIndex == -1)
                            endIndex = serviceEx.Message.Length;
                        if (endIndex > 0 && endIndex > startIndex + 1)
                        {
                            faultMessage = serviceEx.Message.Substring(startIndex + 1, endIndex - startIndex - 1);
                        }
                        else
                        {
                            faultMessage = serviceEx.Message;
                        }
                    }
                }
            }
            return faultMessage;
        }
    }
}