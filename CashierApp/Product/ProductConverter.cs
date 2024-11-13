using CashierApp.Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CashierApp.Product
{
    public class ProductConverter : JsonConverter<IProducts>
    {
        public override IProducts Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialisera till den konkreta klassen Product
            var product = JsonSerializer.Deserialize<Product>(ref reader, options);
            return product;
        }

        public override void Write(Utf8JsonWriter writer, IProducts value, JsonSerializerOptions options)
        {
            // Serialisera den konkreta klassen
            JsonSerializer.Serialize(writer, (Product)value, options);
        }
    }
}
