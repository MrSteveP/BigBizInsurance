using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace CrossCutting.Core.Globalization
{
    public class EnumHelper
    {
        public static string GetDescription(Enum enumValue,Type resourceFile)
        {
            string resourceKey = enumValue.GetType()?.GetMember(enumValue.ToString())?.First()?.GetCustomAttribute<DisplayAttribute>()?.Description;
            var resourceManager = new ResourceManager(resourceFile);
            string value = resourceManager.GetString(resourceKey);
            return value;
        }
    }
}
