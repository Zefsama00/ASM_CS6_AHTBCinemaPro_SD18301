using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ASM_CS6_AHTBCinemaPro_SD18301.Client;
using System.Text.Json;
using System.Globalization;
using System.Text.Json.Serialization;


namespace ASM_CS6_AHTBCinemaPro_SD18301.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });
            builder.Services.AddSingleton(new JsonSerializerOptions
            {
                Converters = { new CustomDateConverter("yyyy-MM-dd") }
            });

            await builder.Build().RunAsync();
        }
        public class CustomDateConverter : JsonConverter<DateTime>
        {
            private readonly string _dateFormat;

            public CustomDateConverter(string dateFormat)
            {
                _dateFormat = dateFormat;
            }

            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var dateString = reader.GetString();
                return DateTime.ParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture);
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(_dateFormat));
            }
        }
    }
}
