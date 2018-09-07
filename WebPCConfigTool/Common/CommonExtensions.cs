using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// Util class with a lot helpful extensions.
    /// </summary>
    public static class CommonExtensions
    {
        private readonly static Type[] SimpleTypes = new Type[] { typeof(Enum), typeof(string), typeof(decimal), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Guid) };

        /// <summary>
        /// Static constructor to initialize the static logger.
        /// </summary>
        static CommonExtensions()
        {
        }

        /// <summary>
        /// Determines whether an instance of the current <see cref="Type"/> can be assigned from an instance of the specified generic <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="genericDefinition">Generic type.</param>
        /// <returns>True if genericDefinition and the current Type represent the same type, or if the current Type is in the inheritance hierarchy of genericDefinition, or if the current Type is an interface that genericDefinition implements, or if genericDefinition is a generic type parameter and the current Type represents one of the constraints of genericDefinition. false if none of these conditions are true, or if genericDefinition is null.</returns>
        public static bool IsGenericTypeOf(this Type type, Type genericDefinition)
        {
            return IsGenericTypeOf(type, genericDefinition, out var parameters);
        }

        /// <summary>
        /// Determines whether an instance of the current <see cref="Type"/> can be assigned from an instance of the specified generic <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="genericDefinition">Generic type.</param>
        /// <param name="genericParameters">Generic parameters found.</param>
        /// <returns>True if genericDefinition and the current Type represent the same type, or if the current Type is in the inheritance hierarchy of genericDefinition, or if the current Type is an interface that genericDefinition implements, or if genericDefinition is a generic type parameter and the current Type represents one of the constraints of genericDefinition. false if none of these conditions are true, or if genericDefinition is null.</returns>
        public static bool IsGenericTypeOf(this Type type, Type genericDefinition, out Type[] genericParameters)
        {
            genericParameters = new Type[] { };

            if (!genericDefinition.IsGenericType)
            {
                return false;
            }

            var isMatch = type.IsGenericType && type.GetGenericTypeDefinition() == genericDefinition.GetGenericTypeDefinition();

            if (!isMatch && type.BaseType != null)
            {
                isMatch = IsGenericTypeOf(type.BaseType, genericDefinition, out genericParameters);
            }

            if (!isMatch && genericDefinition.IsInterface && type.GetInterfaces().Any())
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (i.IsGenericTypeOf(genericDefinition, out genericParameters))
                    {
                        isMatch = true;
                        break;
                    }
                }
            }

            if (isMatch && !genericParameters.Any())
            {
                genericParameters = type.GetGenericArguments();
            }
            return isMatch;
        }

        /// <summary>
        /// Replaces all characters with newChar.
        /// </summary>
        /// <param name="str">String for replacement.</param>
        /// <param name="characters">Characters to replace.</param>
        /// <param name="newChar">The new characters.</param>
        /// <returns></returns>
        public static string Replace(this string str, char[] characters, string newChar)
        {
            var temp = str.Split(characters, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(newChar, temp);
        }

        /// <summary>
        /// Gets the expression properties as string array.
        /// </summary>
        /// <typeparam name="T">The type parameter T.</typeparam>
        /// <typeparam name="P">The type parameter P.</typeparam>
        /// <param name="expr">The expression.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<string> GetPath<T, P>(this Expression<Func<T, P>> expr)
        {
            // Iteration starts with the right most property.
            var stack = new Stack<string>();

            MemberExpression me;
            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    me = (ue?.Operand) as MemberExpression;
                    break;

                default:
                    me = expr.Body as MemberExpression;
                    break;
            }

            while (me != null && me.Member.MemberType != System.Reflection.MemberTypes.Field)
            {
                stack.Push(me.Member.Name);
                me = me.Expression as MemberExpression;
            }

            return stack;
        }

        /// <summary>
        /// Walks through the inner exceptions and returns them as an array.
        /// </summary>
        /// <param name="ex">The exception to get stacktrace messages from.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> with inner exceptions.</returns>
        public static IEnumerable<Exception> GetExceptionChain(this Exception ex)
        {
            if (ex == null)
            {
                yield break;
            }
            do
            {
                yield return ex;
                ex = ex.InnerException;
            } while (ex != null);
        }

        /// <summary>
        /// Removes the last character from a string.
        /// </summary>
        /// <param name="str">The string to use.</param>
        /// <returns>The string without its last character.</returns>
        public static string TrimLastCharacter(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return str.TrimEnd(str[str.Length - 1]);
        }

        /// <summary>
        /// Gets an attribute of an enum value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute to search for.</typeparam>
        /// <param name="enumeration">The enum value.</param>
        /// <returns>The attribute value if present.</returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumeration)
            where TAttribute : Attribute
        {
            var mi = enumeration.GetType()
                .GetMember(enumeration.ToString())
                .FirstOrDefault();

            if (mi != null)
                return mi.GetCustomAttributes(typeof(TAttribute), false)
                    .Cast<TAttribute>()
                    .SingleOrDefault();

            return null;
        }

        /// <summary>
        /// Gets a property of an enum attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="enumeration">The enum value.</param>
        /// <param name="expression">Expression to get the property.</param>
        /// <returns>The value of the property.</returns>
        public static T GetAttributeValue<TAttribute, T>(this Enum enumeration, Func<TAttribute, T> expression)
            where TAttribute : Attribute
        {
            var attribute = enumeration.GetType().GetMember(enumeration.ToString())[0]
                .GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>()
                .SingleOrDefault();

            return attribute == null ? default(T) : expression(attribute);
        }

        /// <summary>
        /// Gets an int property of an enum attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="enumeration">The enum value.</param>
        /// <param name="expression">Expression to get the property.</param>
        /// <returns>The value of the property.</returns>
        public static int GetAttributeValue<TAttribute>(this Enum enumeration, Func<TAttribute, int> expression)
            where TAttribute : Attribute
        {
            return GetAttributeValue<TAttribute, int>(enumeration, expression);
        }

        /// <summary>
        /// Gets a decimal property of an enum attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="enumeration">The enum value.</param>
        /// <param name="expression">Expression to get the property.</param>
        /// <returns>The value of the property.</returns>
        public static decimal GetAttributeValue<TAttribute>(this Enum enumeration, Func<TAttribute, decimal> expression)
            where TAttribute : Attribute
        {
            return GetAttributeValue<TAttribute, decimal>(enumeration, expression);
        }

        /// <summary>
        /// Gets a string property of an enum attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="enumeration">The enum value.</param>
        /// <param name="expression">Expression to get the property.</param>
        /// <returns>The value of the property.</returns>
        public static string GetAttributeValue<TAttribute>(this Enum enumeration, Func<TAttribute, string> expression)
            where TAttribute : Attribute
        {
            return GetAttributeValue<TAttribute, string>(enumeration, expression);
        }

        /// <summary>
        /// Gets a boolean property of an enum attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="enumeration">The enum value.</param>
        /// <param name="expression">Expression to get the property.</param>
        /// <returns>The value of the property.</returns>
        public static bool GetAttributeValue<TAttribute>(this Enum enumeration, Func<TAttribute, bool> expression)
            where TAttribute : Attribute
        {
            return GetAttributeValue<TAttribute, bool>(enumeration, expression);
        }

        /// <summary>
        /// Converts IEnumerable to HashSet.
        /// </summary>
        /// <typeparam name="TSource">The type of the objects in the IEnumerable.</typeparam>
        /// <param name="source">The source IEnumerable.</param>
        /// <returns>A HashSet with the elements from the IEnumerable.</returns>
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new HashSet<TSource>(source);
        }

        /// <summary>
        /// Gets the value of the description attribute of an enum value.
        /// </summary>
        /// <param name="enumeration">The enum value.</param>
        /// <returns>The description.</returns>
        public static string GetDescription(this Enum enumeration)
        {
            return enumeration.GetAttributeValue<DescriptionAttribute>(d => d.Description) ?? enumeration.ToString();
        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given string.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="value">The string value to get enum for.</param>
        /// <returns>The enum.</returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given int.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="value">The int value to get enum for.</param>
        /// <returns>The enum.</returns>
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }

        /// <summary>
        /// Extension method to check does a list contain all values of another list.
        /// </summary>
        /// <typeparam name="T">The list value type.</typeparam>
        /// <param name="containingList">the larger list we're checking in</param>
        /// <param name="lookupList">the list to look for in the containing list</param>
        /// <returns>true if it has everything</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> containingList, IEnumerable<T> lookupList)
        {
            return !lookupList.Except(containingList).Any();
        }

        /// <summary>
        /// Indicates whether the specified array is null or has a length of zero.
        /// </summary>
        /// <param name="array">The array to test.</param>
        /// <returns>True if the array parameter is null or has a length of zero; otherwise, false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> array)
        {
            return (array == null || array.Count() == 0);
        }

        /// <summary>
        /// Indicates whether the specified array is not null or has at least one element.
        /// </summary>
        /// <param name="array">The array to test.</param>
        /// <returns>True if the array parameter is not null or has at least one element; otherwise, false.</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> array)
        {
            return !IsNullOrEmpty(array);
        }

        /// <summary>
        /// Deserialize string to xml.
        /// </summary>
        /// <typeparam name="T">The class to deserialize.</typeparam>
        /// <param name="value">The string to deserialize.</param>
        /// <returns>The object that is serialized.</returns>
        public static T DeserializeXml<T>(this string value)
        {
            return DeserializeXml<T>(value, new Type[] { });
        }

        /// <summary>
        /// Deserialize string to xml.
        /// </summary>
        /// <typeparam name="T">The class to deserialize.</typeparam>
        /// <param name="value">The string to deserialize.</param>
        /// <param name="extraTypes">Extra types to use for deserialization.</param>
        /// <returns>The object that is serialized.</returns>
        public static T DeserializeXml<T>(this string value, Type[] extraTypes)
        {
            if (value.IsNullOrEmpty())
            {
                throw new Exception("Cannot deserialize empty string to " + typeof(T).Name);
            }
            try
            {
                var stringReader = new StringReader(value);
                using (var reader = XmlReader.Create(stringReader))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T), extraTypes);
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Cannot deserialize " + typeof(T).Name, e);
            }
        }

        /// <summary>
        /// Serialize class to xml string.
        /// </summary>
        /// <typeparam name="T">The type of class to serialize.</typeparam>
        /// <param name="value">The class to serialize.</param>
        /// <returns>The xml string.</returns>
        public static string SerializeToXml<T>(this T value)
        {
            return SerializeToXml(value, new Type[] { });
        }

        /// <summary>
        /// Serialize class to xml string.
        /// </summary>
        /// <typeparam name="T">The type of class to serialize.</typeparam>
        /// <param name="value">The class to serialize.</param>
        /// <param name="extraTypes">Extra types to use for serialization.</param>
        /// <returns>The xml string.</returns>
        public static string SerializeToXml<T>(this T value, Type[] extraTypes)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T), extraTypes);
                    xmlSerializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Cannot serialize " + typeof(T).Name, e);
            }
        }

        /// <summary>
        /// Checks is passed <see cref="Type"/> is simple type.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to check.</param>
        /// <returns>True if type is simple else false.</returns>
        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive || SimpleTypes.Contains(type) || Convert.GetTypeCode(type) != TypeCode.Object || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]));
        }

        /// <summary>
        /// Date range check.
        /// </summary>
        /// <param name="dateToCheck">The DateTime to be checked.</param>
        /// <param name="startDate">The start DateTime.</param>
        /// <param name="endDate">The end DateTime.</param>
        /// <returns>True if is in range else false.</returns>
        public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }
}