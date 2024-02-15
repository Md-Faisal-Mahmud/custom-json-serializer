 using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Serializer__Custom_;

public static class JsonFormatter
{
    public static string Convert(object obj)
    {
        if (obj == null) return "null";

        StringBuilder json = new StringBuilder();
        Type type = obj.GetType();
        json.Append("{");

        var fieldInfo = type.GetFields();
        var propertyInfo = type.GetProperties();

        foreach (var field in fieldInfo)                   //==============================================================================================================================
        {
            if (field.FieldType == typeof(object))
            {
                var value = field.GetValue(obj);
                //json.Append($"\"{property.Name}\":");
                if (value is null)
                {
                    json.Append($"\"{field.Name}\":null");
                }
                else if (value is string || value is char)
                {
                    json.Append($"\"{field.Name}\":\"{field.GetValue(obj)}\"");
                }
                else if (value is int || value is sbyte || value is byte || value is short || value is ushort || value is uint || value is long || value is ulong)
                {
                    json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
                }
                else if (value is double)
                {
                    double withPoint = (double)field.GetValue(obj);                                     ///    double
                    double withoutPoint = Math.Truncate(withPoint);
                    if (withPoint == withoutPoint)
                    {
                        json.Append($"\"{field.Name}\":{string.Format("{0:0.0}", field.GetValue(obj))}");
                    }
                    else
                    {
                        json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
                    }
                }
                else if (value is decimal)
                {
                    decimal withPoint = (decimal)field.GetValue(obj);
                    decimal withoutPoint = Math.Truncate(withPoint);
                    if (withPoint == withoutPoint)
                    {
                        json.Append($"\"{field.Name}\":{string.Format("{0:0.0}", field.GetValue(obj))}");
                    }
                    else
                    {
                        json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
                    }
                }
                else if (value is float)
                {
                    float withPoint = (float)field.GetValue(obj);
                    float withoutPoint = (float)Math.Truncate(withPoint);
                    if (withPoint == withoutPoint)
                    {
                        json.Append($"\"{field.Name}\":{string.Format("{0:0.0}", field.GetValue(obj))}");
                    }
                    else
                    {
                        json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
                    }
                }
                else
                {
                    json.Append($"\"{field.Name}\": null");
                }
            }
            else if (field.GetValue(obj) == null)                                                    /// null
            {
                json.Append($"\"{field.Name}\": null");
            }
            else if (field.FieldType == typeof(char))                                             ///  char
            {
                if ((char)field.GetValue(obj) == '\0')
                {
                    json.Append($"\"{field.Name}\":\"\\u0000\"");
                }
                else
                    json.Append($"\"{field.Name}\":\"{field.GetValue(obj)}\"");
            }
            else if (field.FieldType == typeof(string) || field.FieldType == typeof(char))         /// string
            {
                if (field.FieldType == typeof(char) && (char)field.GetValue(obj) == '\0')
                {
                    json.Append($"\"{field.Name}\":\"\\u0000\"");
                }
                else json.Append($"\"{field.Name}\":\"{field.GetValue(obj)}\"");
            }
            else if (field.FieldType.IsArray && !field.FieldType.IsPrimitive)                 ///  primitive array
            {
                var array = field.GetValue(obj) as Array;
                json.Append($"\"{field.Name}\":[");
                for (int j = 0; j < array.Length; j++)
                {
                    json.Append(array.GetValue(j));
                    if (j < array.Length - 1)
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
            }
            else if (field.FieldType.IsArray)                                            ///   Non Primitive array
            {
                var array = field.GetValue(obj) as Array;
                json.Append($"\"{field.Name}\":[");
                for (int j = 0; j < array.Length; j++)
                {
                    json.Append(Convert(array.GetValue(j)));
                    if (j < array.Length - 1)
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
            }
            else if (field.FieldType.IsGenericType)                                           ///    generic List<>
            {
                json.Append($"\"{field.Name}\":[");
                var list = (field.GetValue(obj) as IEnumerable).OfType<object>().ToList();
                //var listType = property.PropertyType.GetGenericArguments()[0];
                //var list = (property.GetValue(obj) as IEnumerable<dynamic>).ToList();
                foreach (var item in list)
                {
                    json.Append(Convert(item));
                    if (item != list.Last())
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
            }
            else if (field.FieldType.IsClass)                                               ///    class 
            {
                var value = field.GetValue(obj);
                json.Append($"\"{field.Name}\":");
                json.Append($"{Convert(value)}");
            }
            else if (field.FieldType == typeof(DateTime))                                      ///  DateTime
            {
                json.Append($"\"{field.Name}\" : \"{((DateTime)field.GetValue(obj)).ToString("yyyy-MM-ddTHH:mm:ss")}\"");
            }
            else if (field.FieldType == typeof(bool))                                         /// boolen
            {
                string str;
                if ((bool)field.GetValue(obj) == true)
                {
                    str = "true";
                }
                else
                {
                    str = "false";
                }
                json.Append($"\"{field.Name}\":{str}");
            }
            else if (field.FieldType.IsEnum)                                                  /// Enum
            {
                int val = (int)field.GetValue(obj);
                json.Append($"\"{field.Name}\":{val}");
            }
            ///   int, sbyte, byte,short, ushort, uint, long, ulong
            else if (field.FieldType == typeof(int) || field.FieldType == typeof(sbyte) || field.FieldType == typeof(byte) || field.FieldType == typeof(short) || field.FieldType == typeof(ushort) || field.FieldType == typeof(uint) || field.FieldType == typeof(long) || field.FieldType == typeof(ulong))
            {
                json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
            }
            else if (field.FieldType == typeof(double))                                        /// double
            {
                double withPoint = (double)field.GetValue(obj);
                double withoutPoint = Math.Truncate(withPoint);
                if (withPoint == withoutPoint)
                {
                    json.Append($"\"{field.Name}\":{string.Format("{0:0.0}", field.GetValue(obj))}");
                }
                else
                {
                    json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
                }
            }
            else if (field.FieldType == typeof(decimal))                                   /// decimal
            {
                decimal withPoint = (decimal)field.GetValue(obj);
                decimal withoutPoint = Math.Truncate(withPoint);
                if (withPoint == withoutPoint)
                {
                    json.Append($"\"{field.Name}\":{string.Format("{0:0.0}", field.GetValue(obj))}");
                }
                else
                {
                    json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
                }
            }
            else if (field.FieldType == typeof(float))                                     /// float
            {
                float withPoint = (float)field.GetValue(obj);
                float withoutPoint = (float)Math.Truncate(withPoint);
                if (withPoint == withoutPoint)
                {
                    json.Append($"\"{field.Name}\":{string.Format("{0:0.0}", field.GetValue(obj))}");
                }
                else
                {
                    json.Append($"\"{field.Name}\":{field.GetValue(obj)}");
                }
            }
            else if (field.FieldType.IsValueType && !field.FieldType.IsPrimitive)          /// custom struct
            {
                var value = field.GetValue(obj);
                json.Append($"\"{field.Name}\":");
                json.Append($"{Convert(value)}");
            }
            else
            {
            }
            //if (i < fieldInfo.Length - 1)
            //{
            //    json.Append(",");
            //i++;
            //}

            //if (propertyInfo.Length < propertyInfo.Length - 1)
            //{
            //    json.Append(",");
            //}
            if (propertyInfo.Length == 0)
            {
                if (field != fieldInfo.Last())     // field na thakle error debe
                    json.Append(",");
            }
            else
            {
                if (field != propertyInfo.Last())
                    json.Append(",");
            }
        }

        foreach (var property in propertyInfo)          //=============================================================================================================
        {
            if (property.PropertyType == typeof(object))
            {
                var value = property.GetValue(obj);

                //json.Append($"\"{property.Name}\":");

                if (value is null)
                {
                    json.Append($"\"{property.Name}\":null");
                }
                else if (value is string || value is char)
                {
                    json.Append($"\"{property.Name}\":\"{property.GetValue(obj)}\"");
                }
                else if (value is int || value is sbyte || value is byte || value is short || value is ushort || value is uint || value is long || value is ulong)
                {
                    json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
                }
                else if (value is double)
                {
                    double withPoint = (double)property.GetValue(obj);                                     ///    double
                    double withoutPoint = Math.Truncate(withPoint);
                    if (withPoint == withoutPoint)
                    {
                        json.Append($"\"{property.Name}\":{string.Format("{0:0.0}", property.GetValue(obj))}");
                    }
                    else
                    {
                        json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
                    }
                }
                else if (value is decimal)
                {
                    decimal withPoint = (decimal)property.GetValue(obj);
                    decimal withoutPoint = Math.Truncate(withPoint);
                    if (withPoint == withoutPoint)
                    {
                        json.Append($"\"{property.Name}\":{string.Format("{0:0.0}", property.GetValue(obj))}");
                    }
                    else
                    {
                        json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
                    }
                }
                else if (value is float)
                {
                    float withPoint = (float)property.GetValue(obj);
                    float withoutPoint = (float)Math.Truncate(withPoint);
                    if (withPoint == withoutPoint)
                    {
                        json.Append($"\"{property.Name}\":{string.Format("{0:0.0}", property.GetValue(obj))}");
                    }
                    else
                    {
                        json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
                    }
                }
                else
                {
                    json.Append($"\"{property.Name}\": null");
                }
            }
            else if (property.GetValue(obj) == null)                                                 /// null
            {
                json.Append($"\"{property.Name}\": null");
            }
            else if (property.PropertyType == typeof(char))                                     ///  char 
            {
                if ((char)property.GetValue(obj) == '\0')
                {
                    json.Append($"\"{property.Name}\":\"\\u0000\"");
                }
                else json.Append($"\"{property.Name}\":\"{property.GetValue(obj)}\"");
            }
            else if (property.PropertyType == typeof(string))                                    ///  string
            {
                json.Append($"\"{property.Name}\":\"{property.GetValue(obj)}\"");
            }
            else if (property.PropertyType.IsArray)                                               ///   Non Primitive array
            {
                var array = property.GetValue(obj) as Array;
                json.Append($"\"{property.Name}\":[");
                for (int j = 0; j < array.Length; j++)
                {
                    json.Append(Convert(array.GetValue(j)));
                    if (j < array.Length - 1)
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
            }
            else if (property.PropertyType.IsGenericType)                                          ///    generic List<>
            {
                json.Append($"\"{property.Name}\":[");
                //var listType = property.PropertyType.GetGenericArguments()[0];
                //var list = (property.GetValue(obj) as IEnumerable<dynamic>).ToList();
                var list = (property.GetValue(obj) as IEnumerable).OfType<object>().ToList();

                foreach (var item in list)
                {
                    json.Append(Convert(item));
                    if (item != list.Last())
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
            }
            else if (property.PropertyType.IsClass)                                                 ///   class 
            {
                var value = property.GetValue(obj);
                json.Append($"\"{property.Name}\":");
                json.Append($"{Convert(value)}");
            }
            else if (property.PropertyType == typeof(DateTime))                                       ///  DateTime
            {
                //DateTime date = (DateTime)property.GetValue(obj);
                json.Append($"\"{property.Name}\" : \"{((DateTime)property.GetValue(obj)).ToString("yyyy-MM-ddTHH:mm:ss")}\"");
            }
            else if (property.PropertyType.IsEnum)                                                 ///    Enum
            {
                int val = (int)property.GetValue(obj);
                json.Append($"\"{property.Name}\":{val}");
            }
            else if (property.PropertyType == typeof(bool))                                        ///    boolen
            {
                string str;
                if ((bool)property.GetValue(obj) == true)
                {
                    str = "true";
                }
                else
                {
                    str = "false";
                }
                json.Append($"\"{property.Name}\":{str}");
            }
            ///     int, sbyte, byte,short, ushort, uint, long, ulong
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(sbyte) || property.PropertyType == typeof(byte) || property.PropertyType == typeof(short) || property.PropertyType == typeof(ushort) || property.PropertyType == typeof(uint) || property.PropertyType == typeof(long) || property.PropertyType == typeof(ulong))
            {
                json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
            }
            else if (property.PropertyType == typeof(double))
            {
                double withPoint = (double)property.GetValue(obj);                                     ///    double
                double withoutPoint = Math.Truncate(withPoint);
                if (withPoint == withoutPoint)
                {
                    json.Append($"\"{property.Name}\":{string.Format("{0:0.0}", property.GetValue(obj))}");
                }
                else
                {
                    json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
                }
            }


            else if (property.PropertyType == typeof(decimal))                                       ///   decimal
            {
                decimal withPoint = (decimal)property.GetValue(obj);
                decimal withoutPoint = Math.Truncate(withPoint);

                if (withPoint == withoutPoint)
                {
                    json.Append($"\"{property.Name}\":{string.Format("{0:0.0}", property.GetValue(obj))}");
                }
                else
                {
                    json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
                }
            }
            else if (property.PropertyType == typeof(float))                                        ///   float
            {
                float withPoint = (float)property.GetValue(obj);
                float withoutPoint = (float)Math.Truncate(withPoint);
                if (withPoint == withoutPoint)
                {
                    json.Append($"\"{property.Name}\":{string.Format("{0:0.0}", property.GetValue(obj))}");
                }
                else
                {
                    json.Append($"\"{property.Name}\":{property.GetValue(obj)}");
                }
            }
            else if (property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive)          /// custom struct
            {
                var value = property.GetValue(obj);
                json.Append($"\"{property.Name}\":");
                json.Append($"{Convert(value)}");
            }
            else
            {
                Console.WriteLine();
            }
            if (property != propertyInfo.Last())
            {
                json.Append(",");
            }
            //if (propertyInfo.Length < propertyInfo.Length - 1)
            //{
            //    json.Append(",");
            //}
        }
        json.Append("}");
        return json.ToString();
    }
}