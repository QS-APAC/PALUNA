using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PALUNA.Utilties
{
    public static class CommonHelper
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            try
            {
                var currentField = enumValue.GetType()?.GetField(enumValue.ToString());
                if (currentField == null) return string.Empty;
                var descriptionAttr = currentField.GetCustomAttribute(typeof(DescriptionAttribute));
                return descriptionAttr is DescriptionAttribute value ? value.Description : string.Empty;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static int GetTypeFromLink(string typeInput)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(typeInput)) return -1;
                foreach (ProductType type in Enum.GetValues(typeof(ProductType)))
                {
                    if (type.GetEnumDescription().Equals(typeInput, StringComparison.OrdinalIgnoreCase))
                    {
                        return type.GetHashCode();
                    }
                }

                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;

            }
        }
    }
}