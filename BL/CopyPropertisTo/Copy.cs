﻿using BO;
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
    public static class Copy
    {
        public static void CopyPropTo<Source, Target>(this Source source, Target target)
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
        }
    }
}
