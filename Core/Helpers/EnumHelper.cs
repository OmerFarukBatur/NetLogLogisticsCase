using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName<T>(this T enumValue) where T : Enum
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? enumValue.ToString();
        }
        public static IEnumerable<(int Value, string Name)> GetSelectList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => (
                    Value: Convert.ToInt32(e),
                    Name: e.GetDisplayName()
                ));
        }
    }
}
