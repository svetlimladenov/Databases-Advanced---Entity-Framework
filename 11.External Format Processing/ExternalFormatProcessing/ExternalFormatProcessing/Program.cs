using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using ExternalFormatProcessing.Data;
using ExternalFormatProcessing.Data.Models;
using Newtonsoft.Json;

namespace ExternalFormatProcessing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var product = new Product
            {
                Name = "Tyre",
                Description = "Makes the car go",
                Manufacturer = new Manufacturer
                {
                    Name = "Nokian"
                }
            };

            //var jsonString = SerializeObject(product);

            var jsonString = JsonConvert.SerializeObject(product,Formatting.Indented, new JsonSerializerSettings
            {
                //NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore 
            });

            //var jsonFromFile = File.ReadAllText("jsonArray.json");

            //var parsedProduct = JsonConvert.DeserializeObject<Product>(jsonFromFile);
            //Console.WriteLine(jsonString);




            var obj = new
            {
                Name = "Pesho",
                Age = 18,
                Grades = new[]
                {
                    5.50,
                    2.20,
                    4.20,
                }
            };

            var outputJson = JsonConvert.SerializeObject(obj, Formatting.Indented);

            var tempate = new
            {
                Name = default(string),
                Age = default(int),
                Grades = new decimal[]
                {

                }
            };

            var desirializedObj = JsonConvert.DeserializeAnonymousType(outputJson, tempate);

            Console.WriteLine(outputJson);
        }

        private static string SerializeObject(Product product)
        {
            var jsonSerializer = new DataContractJsonSerializer(product.GetType());

            using (var stream = new MemoryStream())
            {
                jsonSerializer.WriteObject(stream,product);
                var result = Encoding.UTF8.GetString(stream.ToArray());

                return result;
            }
        }
    }
}
