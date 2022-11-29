using System.Reflection;

namespace CopyPropertisTo
{
    static internal class Tools
    {
        /// <summary>
        ///  a function that copy frome the "source" to the "target" (two different objects) the same properties
        /// </summary>
        /// <typeparam name="Source"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        internal static Target CopyPropTo<Source, Target>(this Source source, Target target)
        {
            ///getting the target properties
            Dictionary<string, PropertyInfo> propertyInfoTarget = target!.GetType().GetProperties().ToDictionary(p => p.Name, p => p);
            ///getting the source properties
            IEnumerable<PropertyInfo> propertyInfoSource = source!.GetType().GetProperties();

            /// for every property tha is in the source
            foreach (var sourcePropertyInfo in propertyInfoSource)
            {
                ///checks if the target conteins the property info to reset the property
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
        /// <summary>
        /// a function that is sending the "target" as object when the "target" is a struct
        /// </summary>
        /// <typeparam name="Source"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        internal static Target CopyPropToStruct<Source, Target>(this Source source, Target target) where Target : struct
        {
            object obj = target;

            source.CopyPropTo(obj);

            return (Target)obj;
        }
        /// <summary>
        /// copy the list of "sources" to a new list of "targets" 
        /// </summary>
        /// <typeparam name="Source"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        internal static IEnumerable<Target> CopyPropToList<Source, Target>(this IEnumerable<Source> sources) where Target : new()
        {
            return from source in sources
                   select source.CopyPropTo(new Target());
        }
        /// <summary>
        /// an extention method for to string function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        internal static string ToStringProperty<T>(this T property)
        {
            string str = "";
            var items = property.GetType().GetProperties();

            /// for every property print the name and the value of the priperty
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
