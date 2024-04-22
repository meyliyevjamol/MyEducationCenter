using System.Text.Json;
using System.Text.Json.Serialization;

public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
{
    private readonly string _format;

    public NullableDateOnlyJsonConverter(string format = "dd.MM.yyyy")
    {
        _format = format;
    }

    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (string.IsNullOrEmpty(dateString))
        {
            return null; // Return null if the string is empty
        }
        return DateOnly.ParseExact(dateString, _format, System.Globalization.CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString(_format, System.Globalization.CultureInfo.InvariantCulture));
        }
        else
    {
            writer.WriteNullValue(); // Write null if the DateOnly? is null
        }
    }
}


public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private readonly string _format;

    public DateOnlyJsonConverter(string format = "dd.MM.yyyy")
    {
        _format = format;
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();

        return DateOnly.ParseExact(dateString, _format, System.Globalization.CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format, System.Globalization.CultureInfo.InvariantCulture));
    }
}
