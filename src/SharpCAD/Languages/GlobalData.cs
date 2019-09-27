using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SharpCAD
{
    public class GlobalData
    {
        /// <summary>
        /// System Language (Chinese, English...)
        /// </summary>
        public static string SystemLanguage = ConfigurationManager.AppSettings["Language"];

        private static Language globalLanguage;
        public static Language GlobalLanguage
        {
            get
            {
                if (globalLanguage == null)
                {
                    globalLanguage = new Language();
                    return globalLanguage;
                }
                return globalLanguage;
            }
        }
    }
}
