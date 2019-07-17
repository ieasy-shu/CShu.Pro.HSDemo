using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.BLL
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            _Remark = remark;
        }

        private string _Remark;

        public string Remark
        {
            get
            {
                return _Remark;
            }
        }

    }
    public static class RemarkExtend
    {
        public static string GetRemark(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo field = type.GetField(value.ToString());
            RemarkAttribute remarkAttribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
            return remarkAttribute.Remark;



            Type tt = value.GetType();
            FieldInfo ff = type.GetField(value.ToString());
            RemarkAttribute ra = (RemarkAttribute)ff.GetCustomAttribute(typeof(RemarkAttribute));

           string i= remarkAttribute.Remark;

        }  
    }
    public static class IntExtend
    {
        public static int GetInt(this int value)
        {
            Type type = value.GetType();
            FieldInfo field = type.GetField(value.ToString());
            RemarkAttribute remarkAttribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
            return 0;
        }
    }    
}
