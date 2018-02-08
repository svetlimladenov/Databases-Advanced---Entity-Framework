using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using Stations.Data;
using Stations.DataProcessor.Dto.Impot;
using Stations.Models;
using Stations.Models.Enums;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Stations.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportStations(StationsDbContext context, string jsonString)
		{
		    var sb = new StringBuilder();

		    var deserializedStations = JsonConvert.DeserializeObject<StationDto[]>(jsonString);

		    var validStations = new List<Station>();
		    foreach (var stationDto in deserializedStations)
		    {
		        if (!IsValid(stationDto))
		        {
		            sb.AppendLine(FailureMessage);
		            continue;
		        }


		        if (stationDto.Town == null)
		        {
		            stationDto.Town = stationDto.Name;
		        }

		        var stationAlreadtExist = validStations.Any(s => s.Name == stationDto.Name);
		        if (stationAlreadtExist)
		        {
		            sb.AppendLine(FailureMessage);
		            continue;
		        }
		        var station = Mapper.Map<Station>(stationDto);

		        validStations.Add(station);

		        sb.AppendLine(string.Format(SuccessMessage,station.Name));
		    }    
            
            context.Stations.AddRange(validStations);
		    context.SaveChanges();
		    var result = sb.ToString();
		    return result;
		}

	    

	    public static string ImportClasses(StationsDbContext context, string jsonString)
		{
		    var deserializedClasses = JsonConvert.DeserializeObject<SeatingClassDto[]>(jsonString);

            var sb = new StringBuilder();

            var validSeatingClasses = new List<SeatingClass>();

		    foreach (var seatingClassDto in deserializedClasses)
		    {
		        if (!IsValid(seatingClassDto))
		        {
		            sb.AppendLine(FailureMessage);
                    continue;
		        }

		        var seatingClassAlreadyExists = validSeatingClasses
                    .Any(sc => sc.Name == seatingClassDto.Name || sc.Abbreviation == seatingClassDto.Abbreviation);		                                       

                if (seatingClassAlreadyExists)
                {
                    sb.AppendLine(FailureMessage);
		            continue;
		        }
                //dobavqme v stationsprofile.cs 
                var seatingClass = Mapper.Map<SeatingClass>(seatingClassDto);

                validSeatingClasses.Add(seatingClass);

		        sb.AppendLine(string.Format(SuccessMessage, seatingClassDto.Name));
		    }
            context.SeatingClasses.AddRange(validSeatingClasses);
		    context.SaveChanges();
            var result = sb.ToString();
		    return result;
		}

		public static string ImportTrains(StationsDbContext context, string jsonString)
		{
			var sb = new StringBuilder();

		    var deserializedTrains = JsonConvert.DeserializeObject<TrainDto[]>(jsonString,new JsonSerializerSettings()
		    {
		        NullValueHandling = NullValueHandling.Ignore
		    });

            var validTrains = new List<Train>();
		   foreach (var trainDto in deserializedTrains)
		    {
		        if (!IsValid(trainDto))
		        {
		            sb.AppendLine(FailureMessage);
                    continue;
		        }

		        var trainAlreadyExist = validTrains.Any(t => t.TrainNumber == trainDto.TrainNumber);
		        if (trainAlreadyExist)
		        {
		            sb.AppendLine(FailureMessage);
		            continue;
                }

		        var seatesAreValid = trainDto.Seats.All(IsValid);

		        if (!seatesAreValid)
		        {
		            sb.AppendLine(FailureMessage);
		            continue;
                }

		        var seatingClassesAreValid = trainDto.Seats
                    .All(s =>context.SeatingClasses            
                    .Any(sc => sc.Name == s.Name && sc.Abbreviation == s.Abbreviation));
		        if (!seatingClassesAreValid)
		        {
		            sb.AppendLine(FailureMessage);
		            continue;
                }

		        var type = Enum.Parse<TrainType>(trainDto.Type);

		        

		        var trainSeats = trainDto.Seats.Select(s => new TrainSeat()
		        {
		            SeatingClass =
		                context.SeatingClasses.SingleOrDefault(sc => sc.Name == s.Name && sc.Abbreviation == s.Abbreviation),
                    Quantity = s.Quantity.Value,  
		        }).ToArray();

		        var train = new Train
		        {
		            TrainNumber = trainDto.TrainNumber,
                    Type = type,
                    TrainSeats = trainSeats
		        };

                validTrains.Add(train);
		        sb.AppendLine(string.Format(SuccessMessage, trainDto.TrainNumber));
		    }

            context.Trains.AddRange(validTrains);
		    context.SaveChanges();
            var result = sb.ToString();
		    return result;
		}

		public static string ImportTrips(StationsDbContext context, string jsonString)
		{
			var sb = new StringBuilder();

		    var deserializedTrips = JsonConvert.DeserializeObject<TripDto[]>(jsonString,new JsonSerializerSettings()
		    {
		        NullValueHandling = NullValueHandling.Ignore,
		    });

		    var validTrips = new List<Trip>();

		    foreach (var tripDto in deserializedTrips)
		    {
		        if (!IsValid(tripDto))
		        {
		            sb.AppendLine(FailureMessage);
                    continue;
		        }

		        var train = context.Trains.SingleOrDefault(t => t.TrainNumber == tripDto.Train);
		        var originStation = context.Stations.SingleOrDefault(s => s.Name == tripDto.OriginStation);
		        var destinationStation = context.Stations.SingleOrDefault(s => s.Name == tripDto.DestinationStation);

                if (train == null || originStation == null || destinationStation == null)
		        {
		            sb.AppendLine(FailureMessage);
		            continue;
                }

		        var departureTime = DateTime.ParseExact(tripDto.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
		        var arrivalTime = DateTime.ParseExact(tripDto.ArrivalTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

		        TimeSpan timeDifference;
		        if (tripDto.TimeDifference != null )
		        {
		            timeDifference = TimeSpan.ParseExact(tripDto.TimeDifference, "hh\\:mm", CultureInfo.InvariantCulture);
		        }


                if (departureTime > arrivalTime)
		        {
		            sb.AppendLine(FailureMessage);
		            continue;
                }

		        var status = Enum.Parse<TripStatus>(tripDto.Status);

                var trip = new Trip()
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    Status = status,
                    TimeDifference = timeDifference
                };

                sb.AppendLine($"Trip from {tripDto.OriginStation} to {tripDto.DestinationStation} imported.");
		    }

            var result = sb.ToString();
		    return result;
		}

		public static string ImportCards(StationsDbContext context, string xmlString)
		{
			throw new NotImplementedException();
		}

		public static string ImportTickets(StationsDbContext context, string xmlString)
		{
			throw new NotImplementedException();
		}




	    private static bool IsValid(object obj)
	    {
	        var validationContext = new ValidationContext(obj);
	        var validationResult = new List<ValidationResult>();

	        var isValid = Validator.TryValidateObject(obj, validationContext, validationResult,true);

	        return isValid;
	    }
    }
}