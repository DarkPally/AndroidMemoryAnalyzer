using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Linq.Expressions;

namespace AndroidMemoryAnalyzer.HeapQuerier
{
    public class PropertyHelper
    {       
        static public string GetPropertyName<T>(Expression<Func<T, object>> propertyFunc)
        {

            string propertyName = "";

            if (propertyFunc.Body is UnaryExpression)
            {
                propertyName = ((MemberExpression)((UnaryExpression)propertyFunc.Body).Operand).Member.Name;
            }
            else if (propertyFunc.Body is MemberExpression)
            {
                propertyName = ((MemberExpression)propertyFunc.Body).Member.Name;
            }
            else if (propertyFunc.Body is ParameterExpression)
            {
                propertyName = ((ParameterExpression)propertyFunc.Body).Type.Name;
            }

            var props = typeof(T).GetProperties();

            return propertyName;
        }
    
    }
}
