using BO;
using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CopyPropertisTo
{
    static internal class Tools
    {
        internal static Target CopyPropTo<Source, Target>(this Source source, Target target)
        {
            Dictionary<string, PropertyInfo> propertyInfoTarget = target.GetType().GetProperties().ToDictionary(p => p.Name, p => p);
            IEnumerable<PropertyInfo> propertyInfoSource = source.GetType().GetProperties().
            Where(p => propertyInfoTarget.ContainsKey(p.Name));

            foreach (var item in propertyInfoSource)
            {
                if (item.PropertyType == typeof(string) || !item.PropertyType.IsClass)
                {
                    propertyInfoTarget[item.Name].SetValue(target, item.GetValue(source));
                }
            }
            return target;
        }

        internal static Target CopyPropToStruct<Source, Target>(this Source source, Target target) where Target : struct
        {
            object obj = target;

            source.CopyPropTo(obj);

            return (Target)obj;
        }

        internal static IEnumerable<Target> CopyPropToList<Source, Target>(this IEnumerable<Source> sources) where Target : new()
        {
            return from source in sources
                   select source.CopyPropTo(new Target());
        }

        internal static IEnumerable<Target> CopyPropToListOfStruct<Source, Target>(this IEnumerable<Source> sources) where Target : struct
        {
            return from source in sources
                   select source.CopyPropTo(new Target());
        }

        internal static string ToStringProperty<T>(this T property)
        {
            string str = "";
            foreach (PropertyInfo item in property.GetType().GetProperties())
            {
                if (item.PropertyType.IsArray) // לבדוק אם בסדר שמדפיס הפוך ברקורסיה
                {
                    ToStringProperty(item.GetValue(property, null));
                }
                str += "\n" + item.Name +
                          ": " + item.GetValue(property, null);
            }
            return str;
        }
    }
}
