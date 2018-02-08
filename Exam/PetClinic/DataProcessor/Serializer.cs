using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context
                .Animals
                .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(e => new
                {
                    OwnerName = e.Passport.OwnerName,
                    AnimalName = e.Name,
                    Age = e.Age,
                    SerialNumber = e.PassportSerialNumber,
                    RegisteredOn = e.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                })
                .OrderBy(e => e.Age)
                .ThenBy(e => e.SerialNumber);

            var jsonString = JsonConvert.SerializeObject(animals, Formatting.Indented);
            return jsonString;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context
                .Procedures
                .Select(p => new
                {
                    Passport = p.Animal.PassportSerialNumber,
                    OwnerNumber = p.
                });
        }
    }
}
