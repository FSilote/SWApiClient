using Kneat.SW.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kneat.SW.Domain.Entity
{
    public class Starship : BaseEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string StarshipClass { get; set; }
        public string Manufacturer { get; set; }
        public string CostInCredits { get; set; }
        public string Length { get; set; }
        public string Crew { get; set; }
        public string Passengers { get; set; }
        public string MaxAtmospheringSpeed { get; set; }
        public string HyperdriveRating { get; set; }
        public string Mglt { get; set; }
        public string CargoCapacity { get; set; }
        public string Consumables { get; set; }
        public ICollection<string> Films { get; set; } = new HashSet<string>();
        public ICollection<string> Pilots { get; set; } = new HashSet<string>();

        public override string ToString()
        {
            return $"Name: {Name}{Environment.NewLine}" +
                $"Model: {Model}{Environment.NewLine}" +
                $"Starship Class: {StarshipClass}{Environment.NewLine}" +
                $"Manufacturer: {Manufacturer}{Environment.NewLine}" +
                $"Cost in Credits: {CostInCredits}{Environment.NewLine}" +
                $"Length: {Length}{Environment.NewLine}" +
                $"Crew: {Crew}{Environment.NewLine}" +
                $"Passengers: {Passengers}{Environment.NewLine}" +
                $"Max Atmosphering Speed: {MaxAtmospheringSpeed}{Environment.NewLine}" +
                $"Hyperdrive Rating: {HyperdriveRating}{Environment.NewLine}" +
                $"MGLT: {Mglt}{Environment.NewLine}" +
                $"Cargo Capacity: {CargoCapacity}{Environment.NewLine}" +
                $"Consumables: {Consumables}{Environment.NewLine}" +
                $"Films: {string.Join(", ", Films ?? new List<string>())}{Environment.NewLine}" +
                $"Pilots: {string.Join(", ", Pilots ?? new List<string>())}{Environment.NewLine}" +
                $"Url: {Url}{Environment.NewLine}" +
                $"Created at: {Created.ToString("yyyy-MM-dd HH:mm")}{Environment.NewLine}" +
                $"Edited at: {Edited.ToString("yyyy-MM-dd HH:mm")}{Environment.NewLine}";
        }

        /// <summary>
        /// Gets the time of autonomy in days based in consumables.
        /// Returns 0 when description of time range is unknown or the value is not present.
        /// </summary>
        /// <returns>Int: Autonomy in days until next resupply. Considering consumables are full.</returns>
        public long GetAutonomyInDaysFromConsumables()
        {
            var rangeDays = 0L;
            var consumables = 0L;
            var numberRegex = new Regex(@"\d+");

            if (Regex.IsMatch(this.Consumables, @"(?i)(\w*day\w*)"))
            {
                rangeDays = 1L;
            }
            else if (Regex.IsMatch(this.Consumables, @"(?i)(\w*week\w*)"))
            {
                rangeDays = 7L;
            }
            else if (Regex.IsMatch(this.Consumables, @"(?i)(\w*month\w*)"))
            {
                rangeDays = 30L;
            }
            else if (Regex.IsMatch(this.Consumables, @"(?i)(\w*year\w*)"))
            {
                rangeDays = 365L;
            }

            long.TryParse(numberRegex.Match(this.Consumables).Value, out consumables);

            return rangeDays * consumables;
        }

        /// <summary>
        /// Gets the time of autonomy in hours based in consumables.
        /// Returns 0 when description of time range is unknown or the value is not present.
        /// </summary>
        /// <returns>Long: Autonomy in hours until next resupply. Considering consumables are full.</returns>
        public long GetAutonomyInHoursFromConsumables()
        {
            return this.GetAutonomyInDaysFromConsumables() * 24L;
        }

        /// <summary>
        /// Gets the integer value of MGLT without descriptions.
        /// Returns 0 when value is not present.
        /// </summary>
        /// <returns>Int: MGLT value</returns>
        public int GetMgltCleanValue()
        {
            var mglt = 0;
            var numberRegex = new Regex(@"\d+");
            int.TryParse(numberRegex.Match(this.Mglt).Value, out mglt);
            return mglt;
        }

        /// <summary>
        /// Gets the minimum number of stops need to resupply to cover the distance informed.
        /// Returns -1 when the number of stops can't be calculated or distance is Negative.
        /// </summary>
        /// <param name="distance">The travel distance in MGLT</param>
        /// <returns>Long: Minimum number of stops needed</returns>
        public long GetStopsNeededToResupply(long distance)
        {
            if (distance == 0)
                return 0;

            var stopsNeeded = -1L;
            var mglt = (long)this.GetMgltCleanValue();
            var autonomyInHours = this.GetAutonomyInHoursFromConsumables();

            if (mglt > 0 && autonomyInHours > 0 && distance > 0)
            {
                var flightTimeInHours = distance / this.GetMgltCleanValue();
                stopsNeeded = flightTimeInHours / autonomyInHours;
            }
            
            return stopsNeeded;
        }
    }
}
