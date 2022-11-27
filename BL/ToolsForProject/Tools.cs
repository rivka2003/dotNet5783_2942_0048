using BO;
using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections;
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
            Dictionary<string, PropertyInfo> propertyInfoTarget = target!.GetType().GetProperties().ToDictionary(p => p.Name, p => p);
            IEnumerable<PropertyInfo> propertyInfoSource = source!.GetType().GetProperties();

            foreach (var sourcePropertyInfo in propertyInfoSource)
            {
                if (propertyInfoTarget.ContainsKey(sourcePropertyInfo.Name)
                    && (sourcePropertyInfo.PropertyType == propertyInfoTarget[sourcePropertyInfo.Name].PropertyType
                    || sourcePropertyInfo.PropertyType.IsEnum)
                    && (sourcePropertyInfo.PropertyType == typeof(string) || !sourcePropertyInfo.PropertyType.IsClass))
                {
                    propertyInfoTarget[sourcePropertyInfo.Name].SetValue(target, sourcePropertyInfo.GetValue(source));
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
            var items = property.GetType().GetProperties();

            foreach (PropertyInfo item in items)
            {
                //if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) 
                //{
                //    //List values = (List)sourcePropertyInfo.GetValue(property, null);
                //    //ToStringProperty(sourcePropertyInfo.GetValue(property, null));

                //   var objects = item.GetValue(property);

                //    if (objects is not null)
                //    {
                //        foreach (var @object in (IEnumerable)objects)
                //        {
                //            ToStringProperty(@object);
                //        }
                //    }
                //}
                str += "\n" + item.Name +
                          ": " + item.GetValue(property, null);
            }
            return str;
        }
    }
}
