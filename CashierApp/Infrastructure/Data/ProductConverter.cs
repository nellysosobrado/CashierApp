using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using CashierApp.Core.Entities;
using CashierApp.Core.Interfaces.StoreProducts;

namespace CashierApp.Infrastructure.Data
{
    /// <summary>
    /// A custom JSON converter for handling serialization and deserialization
    /// of objects implementing the IProducts interface.
    /// </summary>
    public class ProductConverter : JsonConverter<IProducts>
    {
        public override IProducts Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserializes the JSON into a Product object and returns it as an IProducts instance
            return JsonSerializer.Deserialize<Product>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IProducts value, JsonSerializerOptions options)
        {
            // Serializes the object to JSON if it's a Product type.
            if (value is Product product)
            {
                JsonSerializer.Serialize(writer, product, options);
            }
            else
            {
                throw new JsonException("Unsupported type for serialization.");
            }
        }
    }
}
