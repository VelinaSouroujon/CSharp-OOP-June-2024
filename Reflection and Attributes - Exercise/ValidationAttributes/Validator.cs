using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ValidationAttributes.Attributes;

namespace ValidationAttributes
{
    public static class Validator
    {
        private static readonly Dictionary<Type, Dictionary<PropertyInfo, MyValidationAttribute[]>> cache =
            new Dictionary<Type, Dictionary<PropertyInfo, MyValidationAttribute[]>>();
        public static bool IsValid(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            Type type = obj.GetType();

            Dictionary<PropertyInfo, MyValidationAttribute[]> validationSetup = GetValidationSetup(type);

            foreach(var kvp in validationSetup)
            {
                object propertyValue = kvp.Key.GetValue(obj);

                foreach(var attribute in kvp.Value)
                {
                    if (!attribute.IsValid(propertyValue))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private static Dictionary<PropertyInfo, MyValidationAttribute[]> GetValidationSetup(Type type)
        {
            if(cache.ContainsKey(type))
            {
                return cache[type];
            }

            Dictionary<PropertyInfo, MyValidationAttribute[]> propertiesMap = new Dictionary<PropertyInfo, MyValidationAttribute[]>();

            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach (PropertyInfo property in properties)
            {
                MyValidationAttribute[] validationAttributes = property.GetCustomAttributes<MyValidationAttribute>()
                    .ToArray();

                if(validationAttributes.Length > 0)
                {
                    propertiesMap[property] = validationAttributes;
                }
            }

            cache[type] = propertiesMap;

            return propertiesMap;
        }
    }
}
