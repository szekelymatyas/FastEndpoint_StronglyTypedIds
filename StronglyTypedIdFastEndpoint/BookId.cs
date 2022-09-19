using NJsonSchema.Annotations;
using NJsonSchema;
using StronglyTypedIds;
using static System.Net.Mime.MediaTypeNames;

namespace StronglyTypedIdFastEndpoint;

/// <summary>
/// Book Id
/// </summary>
/// <example>something</example>
[JsonSchema(JsonObjectType.String, Format = "guid")]
[StronglyTypedId(StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.EfCoreValueConverter | StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.TypeConverter)]
public partial struct BookId
{
    public static BookId Parse(string s, IFormatProvider? provider)
    {
        return new(Guid.Parse(s));
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out BookId result)
    {
        if (Guid.TryParse(s, out Guid guid))
        {
            result = new(guid);
            return true;
        }
        result = default;
        return false;
    }

    public static BookId Parse(string s)
    {
        return new(Guid.Parse(s));
    }

    public static bool TryParse(string? s, out BookId result)
    {
        if (Guid.TryParse(s, out Guid guid))
        {
            result = new(guid);
            return true;
        }
        result = default;
        return false;
    }
}