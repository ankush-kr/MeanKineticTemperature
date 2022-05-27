using System;
using System.Collections.Generic;
using System.Linq;

namespace MeanKineticTemperature
{
    public class MKT
    {
        /// <summary>
        /// Calculates Mean kinetic temperature for the given list of temperature readings taken at the same interval.
        /// </summary>
        /// <param name="temperatureList">List of temperature reading with same interval</param>
        /// <returns>Mean Kinetic temperature</returns>
        public static double Calculate(List<double> temperatureList)
        {
            // Mean Kinetic Temperature
            // https://en.wikipedia.org/wiki/Mean_kinetic_temperature

            // n                =   is number of temperature points or samples            
            // ΔH               =   83.14472 kJ/mole
            // R (Gas Constant) =   0.008314472 kJ/mole/degree
            // ΔH/R             =   10000
            int n = temperatureList.Count;
            double deltaH = 83.14472;
            double R = 0.008314472;

            // Convert  °C (celcius) to °K (kelvin) by adding 273.2 to each reading
            // The MKT value is always calculated in Degree Kelvin. If your readings are in°C or °F, the same has to be first converted to Kelvin.
            // The resultant MKT value also will be in Kelvin, which then has to be converted back to your required units
            List<double> temperatureListKelvin = temperatureList.Select(x => x + 273.2).ToList();

            // Calculate the following for each reading:
            // e raised (- ΔH /(R x Temperature Reading))
            temperatureListKelvin = temperatureListKelvin.Select(x => Math.Exp(-deltaH / (R * x))).ToList();

            // Calculate Numerator: ΔH/R
            var numerator = deltaH / R;

            // Calculate denominatore: -ln ( SUM < e raised (- ΔH /(R x Temperature Reading)) > of all temperature ) 
            var denominator = -Math.Log(temperatureListKelvin.Sum() / n);

            // Calculate MKT in Kelvin
            var MKTkelvin = numerator / denominator;

            // Calculate MKT in Celcius
            var MKTCelcius = MKTkelvin - 273.2;

            return MKTCelcius;
        }
    }
}
