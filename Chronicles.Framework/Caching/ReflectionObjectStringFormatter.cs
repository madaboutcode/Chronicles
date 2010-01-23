using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chronicles.Framework.Caching
{
    public class ReflectionObjectStringFormatter : IObjectStringFormatter
    {
        public string Format(object objGraph)
        {
            return FormatInternal(objGraph);
        }

        private string FormatInternal(object objGraph)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("{");
            bool isFirstItem = true;

            if(objGraph != null)
            {
                Type type = objGraph.GetType();

                if(!type.IsValueType)
                {
                    foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public))
                    {
                        object value = property.GetValue(objGraph, null);

                        if(value == null)
                        {
                            Output(ref builder, property.Name, "NULL", ref isFirstItem);
                        }
                        else
                        {
                            if(value.GetType().IsValueType)
                            {
                                Output(ref builder, property.Name, value.ToString(), ref isFirstItem);
                            }
                            else
                            {
                                throw new NotImplementedException("Complex properties are not supported on the model");
                            }
                        }
                        
                    }
                }
                else
                {
                    builder.Append(objGraph.ToString());
                }
            }
            else
            {
                builder.Append("NULL");
            }

            builder.Append("}");

            return builder.ToString();
        }

        private void Output(ref StringBuilder builder, string name, object value,ref bool isFirstItem)
        {
            builder.AppendFormat("{0}{1}:{2}", isFirstItem ? "" : ",", name, value);
            isFirstItem = false;
        }
    }
}
