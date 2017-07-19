using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapQuerier
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;
        public string Description { get { return description; } }

        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }

    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes =
                (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }

        public static Enum GetEnum<T>(string description)
         {
            var temp= Enum.GetValues(typeof(T));

            foreach (Enum p in Enum.GetValues(typeof(T)))
             {
                 if (description.Equals(GetDescription(p)) == true)
                 return p;
             }
             throw new Exception("没有找到对应的Enum");
         }
    }
}
