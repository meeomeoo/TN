using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EWallet.Utilities.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// <para>Author: D</para>
        /// <para>Date: 18/05/2018</para>
        /// <para>Description: Lấy tên Enum hoặc Description của Enum</para>
        /// </summary>
        /// <returns></returns>
        public static string Text(this Enum eEnum)
        {
            var fi = eEnum.GetType().GetField(eEnum.ToString());

            if (fi == null)
                return eEnum.Value().ToString();

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Any() ? attributes[0].Description : eEnum.ToString();
        }

        /// <summary>
        /// <para>Author: D</para>
        /// <para>Date: 18/05/2018</para>
        /// <para>Description: Lấy value của Enum</para>
        /// </summary>
        /// <returns></returns>
        public static int Value(this Enum eEnum)
        {
            var changeType = Convert.ChangeType(eEnum, eEnum.GetTypeCode());
            if (changeType != null)
                return (int)changeType;
            return -9999;
        }

        /// <summary>
        /// <para>Author: D</para>
        /// <para>Date: 18/05/2018</para>
        /// <para>Description: Covert String to Enum object</para>
        /// </summary>
        /// <returns></returns>
        public static T ToEnum<T>(this object enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString.ToString());
        }

        /// <summary>
        /// <para>Author: D</para>
        /// <para>Date: 18/05/2018</para>
        /// <para>Description: Convert number value to Enum</para>
        /// </summary>
        /// <returns></returns>
        public static T NumberToEnum<T>(this int enumValue)
        {
            return (T)Enum.ToObject(typeof(T), enumValue);
        }

        public static List<EnumToList> ToList(this Type eNum)
        {
            var enumValues = Enum.GetValues(eNum).Cast<Enum>();

            var items = (from enumValue in enumValues
                         select new EnumToList
                         {
                             Key = enumValue.Value(),
                             Value = enumValue.Text()
                         }).ToList();

            return items;
        }
    }

    public class EnumToList
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
