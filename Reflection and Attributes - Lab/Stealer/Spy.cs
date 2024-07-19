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
    }
}
