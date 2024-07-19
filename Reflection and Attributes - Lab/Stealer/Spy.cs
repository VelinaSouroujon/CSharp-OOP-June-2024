using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string className, params string[] fieldNames)
        {
            StringBuilder result = new StringBuilder();

            Type? type = Type.GetType(className);
            if(type is null )
            {
                throw new InvalidOperationException("Type not found");
            }
            result.AppendLine($"Class under investigation: {type.FullName}");

            object? instance = Activator.CreateInstance(type);
            if(instance is null )
            {
                throw new InvalidOperationException("Instance cannot be created");
            }

            foreach (string currentFieldName in fieldNames)
            {
                FieldInfo? field = type.GetField(currentFieldName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                if(field is null)
                {
                    continue;
                }

                result.AppendLine($"{field.Name} = {field.GetValue(instance)}");
            }

            return result.ToString().TrimEnd();
        }
        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder result = new StringBuilder();

            Type? type = Type.GetType(className);
            if (type is null)
            {
                throw new InvalidOperationException("Type not found");
            }

            FieldInfo[] publicFields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach(FieldInfo field in publicFields)
            {
                result.AppendLine($"{field.Name} must be private!");
            }
            foreach(PropertyInfo property in properties)
            {
                MethodInfo? getter = property.GetGetMethod(true);
                if(getter is not null && !getter.IsPublic)
                {
                    result.AppendLine($"{getter.Name} have to be public!");
                }

                MethodInfo? setter = property.GetSetMethod();
                if(setter is not null)
                {
                    result.AppendLine($"{setter.Name} have to be private!");
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}
