using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class JsonTimeSpanConverter : Newtonsoft.Json.JsonConverter
    {
        private const string TimeSpanFormatString = @"hh\:mm\:ss";

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var timeSpan = (TimeSpan)value;
            writer.WriteValue(timeSpan.ToString(TimeSpanFormatString));
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == Newtonsoft.Json.JsonToken.String)
            {
                var timeSpanString = (string)reader.Value;
                if (TimeSpan.TryParseExact(timeSpanString, TimeSpanFormatString, null, out var parsedTimeSpan))
                {
                    return parsedTimeSpan;
                }
            }
            throw new Newtonsoft.Json.JsonSerializationException("Invalid TimeSpan format");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }
    }
}
