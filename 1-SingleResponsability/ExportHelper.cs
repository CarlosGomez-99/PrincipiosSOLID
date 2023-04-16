using System.Collections;
using System.Reflection;
using System.Text;

namespace SingleResponsability
{
    public class ExportHelper<T> where T : class
    {
        public void ExportStudents(IEnumerable<T> items)
        {
            Type type = typeof(T);
            string classNme = type.Name;

            string csv = String.Join(",", items.Select(x => x.ToString()).ToArray());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //Obten las propiedades de la clase
            PropertyInfo[] properties = type.GetProperties();
            string appentLine = string.Empty;
            foreach (PropertyInfo property in properties)
            {
                appentLine += $"{property.Name};";
            }
            sb.AppendLine(appentLine);

            foreach (var item in items)
            {
                string valueOfProperty = string.Empty;
                foreach (PropertyInfo property in properties)
                {
                    Type propertyType = property.PropertyType;
                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        IList list = (IList)property.GetValue(item);
                        if (list != null)
                        {

                            foreach (object listItem in list)
                            {
                                valueOfProperty += $"{(listItem != null ? listItem.ToString() : "null")}|";
                            }
                            valueOfProperty += ";";
                        }
                        else
                        {
                            valueOfProperty += "null";
                        }
                    }
                    else
                    {
                        object value = property.GetValue(item);
                        valueOfProperty += $"{(value != null ? value.ToString() : "null")};";

                    }
                }
                sb.AppendLine(valueOfProperty);
                //sb.AppendLine($"{item.Id};{item.Fullname};{string.Join("|", item.Grades)}");
            }
            System.IO.File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{classNme}.csv"), sb.ToString(), Encoding.Unicode);
        }
    }
}

