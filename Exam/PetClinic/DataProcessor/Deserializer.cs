using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.DataProcessor.Dto.Import;
using PetClinic.Models;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Deserializer
    {

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedAnimalAids = JsonConvert.DeserializeObject<AnimalsAidsDto[]>(jsonString);

            var validAnimalsAids = new List<AnimalAid>();

            foreach (var animalsAidsDto in deserializedAnimalAids)
            {
                if (!IsValid(animalsAidsDto))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var animalAidsAlreadyExist = validAnimalsAids.Any(a => a.Name == animalsAidsDto.Name);
                if (animalAidsAlreadyExist)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var name = animalsAidsDto.Name;
                var price = animalsAidsDto.Price;

                var animalAid = new AnimalAid()
                {
                    Name = name,
                    Price = price
                };
                validAnimalsAids.Add(animalAid);
                sb.AppendLine($"Record {animalsAidsDto.Name} successfully imported.");
            }
            context.AnimalAids.AddRange(validAnimalsAids);
            context.SaveChanges();
            var result = sb.ToString();
            return result;
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedAnimals = JsonConvert.DeserializeObject<AnimalsDto[]>(jsonString, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var validAnimals = new List<Animal>();

            foreach (var animalsDto in deserializedAnimals)
            {
                if (!IsValid(animalsDto))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (!IsValid(animalsDto.Passport))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var passportAlreadyExists =
                    validAnimals.Any(a => a.Passport.SerialNumber == animalsDto.Passport.SerialNumber);
                if (passportAlreadyExists)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var passport = animalsDto.Passport;
                var registrationDate = DateTime.ParseExact(animalsDto.Passport.RegistrationDate,"dd-mm-yyyy" , CultureInfo.InvariantCulture);
                var animal = new Animal()
                {
                    Name = animalsDto.Name,
                    Type = animalsDto.Type,
                    Age = animalsDto.Age,
                    Passport = new Passport()
                    {
                        SerialNumber = animalsDto.Passport.SerialNumber,
                        OwnerName = animalsDto.Passport.OwnerName,
                        OwnerPhoneNumber = animalsDto.Passport.OwnerPhoneNumber,
                        RegistrationDate = registrationDate
                    }
                };

                validAnimals.Add(animal);
                sb.AppendLine(
                    $"Record {animalsDto.Name} Passport №: {animalsDto.Passport.SerialNumber} successfully imported.");
            }
            context.Animals.AddRange(validAnimals);
            context.SaveChanges();
            var result = sb.ToString();
            return result;
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserializedVets = (VetDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();

            var validVets = new List<Vet>();
            foreach (var vetDto in deserializedVets)
            {
                if (!IsValid(vetDto))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var vetPhoneNumAlreadyExist = validVets.Any(v => v.PhoneNumber == vetDto.PhoneNumber);
                if (vetPhoneNumAlreadyExist)
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var vet = new Vet()
                {
                    Name = vetDto.Name,
                    Profession = vetDto.Profession,
                    Age = vetDto.Age,
                    PhoneNumber = vetDto.PhoneNumber
                };
                validVets.Add(vet);
                sb.AppendLine($"Record {vetDto.Name} successfully imported.");
            }
            context.Vets.AddRange(validVets);
            context.SaveChanges();
            var result = sb.ToString();
            return result;
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserializedProcedure = (ProcedureDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();

            var validProcedures = new List<Procedure>();

            foreach (var procedureDto in deserializedProcedure)
            {
                var currentProcedureAids = new List<AnimalsAidsDto>();
                var alreadyInThisProcedure = false;
                //foreach (var procedureDtoAnimalAid in procedureDto)
                //{

                //    if (currentProcedureAids.Contains(procedureDtoAnimalAid))
                //    {
                //        sb.AppendLine($"Error: Invalid data.");
                //        alreadyInThisProcedure = true;
                //        continue;
                //    }
                //    currentProcedureAids.Add(procedureDtoAnimalAid);
                //}

                //if (alreadyInThisProcedure)
                //{
                //    sb.AppendLine($"Error: Invalid data.");
                //    continue;
                //}


                //if (!IsValid(procedureDto))
                //{
                //    sb.AppendLine($"Error: Invalid data.");
                //    continue;
                //}

                //var vet = context.Vets.SingleOrDefault(v => v.Name == procedureDto.Vet);
                //var animal = context.Animals.SingleOrDefault(a => a.PassportSerialNumber == procedureDto.Animal);
                //if (animal == null || vet == null)
                //{
                //    sb.AppendLine($"Error: Invalid data.");
                //    continue;
                //}

                //var animalAidExist =
                //    context.AnimalAids.Any(a => a.Name == procedureDto.Animal);
                //if (!animalAidExist)
                //{
                //    sb.AppendLine($"Error: Invalid data.");
                //    continue;
                //}

                
                //var animalAidsAreValid = procedureDto.AnimalAids.All(IsValid);
                //if (!animalAidsAreValid)
                //{
                //    sb.AppendLine($"Error: Invalid data.");
                //    continue;
                //}


                

                //var dateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-mm-yyyy", CultureInfo.InvariantCulture);
                //var procedure = new Procedure()
                //{
                //    Vet = vet,
                //    Animal = animal,
                //    DateTime = dateTime,
                //    ProcedureAnimalAids =
                //};
            }

            var validVets = new List<Procedure>();

            var result = sb.ToString();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);

            return isValid;
        }
    }
}
