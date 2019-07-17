﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShu.Pro.BLL
{

    [AttributeUsage(AttributeTargets.Property)]
    public class IntValidateAttribute : AbstractValidateAttribute
    {
        private int _Min = 0;
        private int _Max = 0;

        public IntValidateAttribute(int min, int max)
        {
            this._Min = min;
            this._Max = max;
        }

        public override bool Validate(object oValue)
        {
            int num=0;
            return oValue != null && int.TryParse(oValue.ToString(), out num) && num >= this._Min && num <= this._Max;
        }
    }
}
