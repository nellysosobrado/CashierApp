using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using CashierApp.Core.Interfaces;
using CashierApp.Core.Entities;

namespace CashierApp.Infrastructure.Data
{
    public class ProductConverter : JsonConverter<IProducts>
    {
        public override IProducts Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            var product = JsonSerializer.Deserialize<Product>(ref reader, options);
            return product;
        }

        public override void Write(Utf8JsonWriter writer, IProducts value, JsonSerializerOptions options)
        {

            JsonSerializer.Serialize(writer, (Product)value, options);
        }
    }
}
