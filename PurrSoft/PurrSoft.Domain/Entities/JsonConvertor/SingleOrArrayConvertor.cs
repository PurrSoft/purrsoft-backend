using Newtonsoft.Json;

namespace PurrSoft.Domain.Entities.JsonConvertor;

public class SingleOrArrayJsonConverter<T> : JsonConverter<List<T>>
{
    public override void WriteJson(JsonWriter writer, List<T>? value, JsonSerializer serializer)
    {
        if (value != null && value.Count == 1)
        {
            serializer.Serialize(writer, value[0]);
        }
        else
        {
            serializer.Serialize(writer, value);
        }
    }

    public override List<T>? ReadJson(JsonReader reader, Type objectType, List<T>? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartArray)
        {
            return serializer.Deserialize<List<T>>(reader);
        }
        else if (reader.TokenType != JsonToken.Null)
        {
            T? singleItem = serializer.Deserialize<T>(reader);
            if (singleItem != null) return new List<T> { singleItem };
        }

        return existingValue ?? new List<T>();
    }
}