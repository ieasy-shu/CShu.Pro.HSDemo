using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.BLL
{
    public class BaseDAL
    {
        public static void Save<T>(T t)
        {
            Type type = t.GetType();
            bool isSafe = true;

            foreach (var property in type.GetProperties())
            {
                object[] oAttributeArray = property.GetCustomAttributes(typeof(AbstractValidateAttribute), true);//特性类的实例化就在反射发生的时候
                foreach (var oAttribute in oAttributeArray)
                {
                    AbstractValidateAttribute validateAttribute = oAttribute as AbstractValidateAttribute;
                    isSafe = validateAttribute.Validate(property.GetValue(t));
                    if (!isSafe)
                    {
                        break;
                    }
                }
            }
        }
    }
}







/*
 
            foreach (var property in type.GetProperties())
            {
                object[] oAttributeArray = property.GetCustomAttributes(typeof(AbstractValidateAttribute), true);//特性类的实例化就在反射发生的时候
                foreach (var oAttribute in oAttributeArray)
                {
                    AbstractValidateAttribute validateAttribute = oAttribute as AbstractValidateAttribute;
                    isSafe = validateAttribute.Validate(property.GetValue(t));
                    if (!isSafe)
                    {
                        break;
                    }
                }
            }
 */