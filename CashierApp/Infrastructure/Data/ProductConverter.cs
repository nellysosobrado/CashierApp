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
    public class ProductConverter : JsonConverter<IProducts>
    {
        public override IProducts Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialisera till Product
            return JsonSerializer.Deserialize<Product>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IProducts value, JsonSerializerOptions options)
        {
            // Kontrollera att värdet är av typen Product
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
