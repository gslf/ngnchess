using System.ComponentModel;
using System.Reflection;

namespace ngnchess.Extensions;
/// <summary>
/// Provides extension methods for working with enumerations.
/// </summary>
public static class EnumExtensions {
    /// <summary>
    /// Retrieves the description of an enumeration value, as specified by the <see cref="DescriptionAttribute"/>.
    /// </summary>
    /// <param name="value">The enumeration value for which to retrieve the description.</param>
    /// <returns>
    /// The description of the enumeration value if the <see cref="DescriptionAttribute"/> is applied; 
    /// otherwise, the string representation of the enumeration value.
    /// </returns>
    public static string GetDescription(this Enum value) {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute == null ? value.ToString() : attribute.Description;
    }
}
