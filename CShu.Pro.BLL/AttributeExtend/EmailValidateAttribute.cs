using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CShu.Pro.BLL
{

    [AttributeUsage(AttributeTargets.Property)]
    public class EmailValidateAttribute : AbstractValidateAttribute
    {
        //private string sEmail = null;
        //public EmailValidateAttribute(string value)
        //{
        //    sEmail = value;
        //}

        public override bool Validate(object oValue)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?){1}";
            Regex regex = new Regex(strRegex, RegexOptions.IgnoreCase);
            return regex.Match(oValue.ToString()).Success;
           
        }

    }
}
