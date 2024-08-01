using ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class CustomTimeSpanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var ticks = 0L;
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject)
                        break;

                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value.ToString();
                        reader.Read();
                        if (propertyName == "ticks")
                        {
                            ticks = (long)reader.Value;
                        }
                    }
                }
                return new TimeSpan(ticks);
            }

            throw new JsonSerializationException("Unexpected token type for TimeSpan");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            TimeSpan timeSpan = (TimeSpan)value;
            writer.WriteStartObject();
            writer.WritePropertyName("ticks");
            writer.WriteValue(timeSpan.Ticks);
            writer.WriteEndObject();
        }
    }
    public class GioChieu
    {
        [Key]
        public int IdGioChieu { get; set; }

        [Required(ErrorMessage = "Giờ bắt đầu là bắt buộc.")]
        [JsonConverter(typeof(CustomTimeSpanConverter))]
        public TimeSpan GioBatDau { get; set; }

        [Required(ErrorMessage = "Giờ kết thúc là bắt buộc.")]
        [JsonConverter(typeof(CustomTimeSpanConverter))]
        public TimeSpan GioKetThuc { get; set; }

        [ForeignKey("CaChieus")]
        public int Cachieu { get; set; }
        public NgayChieu CaChieus { get; set; }

        public string TrangThai { get; set; }

        public ICollection<Ve> Ves { get; set; }

        // Phương pháp tĩnh để thực hiện xác thực tùy chỉnh cho GioBatDau
        public static ValidationResult ValidateGioBatDau(TimeSpan gioBatDau, ValidationContext context)
        {
            var gioChieu = context.ObjectInstance as GioChieu;

            if (gioChieu != null && gioBatDau >= gioChieu.GioKetThuc)
            {
                return new ValidationResult("Giờ bắt đầu phải nhỏ hơn giờ kết thúc.");
            }

            return ValidationResult.Success;
        }
    }
}
