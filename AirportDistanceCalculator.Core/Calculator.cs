using AirportDistanceCalculator.Core.Model;
using System;

namespace AirportDistanceCalculator.Core
{
	public static class Calculator
	{
        // Radius of earth (defaults to mean radius in metres).
        public const int Radius = 6371000;

        public static double DistanceBetween(Location first, Location second)
		{
            /*
             * 
                Haversine formula:
                a = sin²(Δφ/2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ/2)
                c = 2 ⋅ atan2( √a, √(1−a) )
                d = R ⋅ c

                where	φ is latitude, λ is longitude, R is earth’s radius (mean radius = 6,371km);
                note that angles need to be in radians to pass to trig functions!
            */

            var φ1 = first.Latitude.Wrap90().ToRadians();
            var λ1 = first.Longitude.Wrap180().ToRadians();

            var φ2 = second.Latitude.Wrap90().ToRadians();
            var λ2 = second.Longitude.Wrap180().ToRadians();

            var Δφ = φ2 - φ1;
            var Δλ = λ2 - λ1;

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) + Math.Cos(φ1) * Math.Cos(φ2) * Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return Radius * c;
		}

        private static double Wrap180(this double degrees)
        {
            // Constrain degrees to range -180..+180 (e.g. for longitude); -181 => 179, 181 => -179.

            // avoid rounding due to arithmetic ops if within range
            if (-180 < degrees && degrees <= 180) return degrees;

            // sawtooth wave p:180, a:±180
            return (degrees + 540) % 360 - 180; 
        }

        private static double Wrap90(this double degrees)
        {
            // Constrain degrees to range -90..+90 (e.g. for latitude); -91 => -89, 91 => 89.

            // avoid rounding due to arithmetic ops if within range
            if (-90 <= degrees && degrees <= 90) return degrees;

            // triangle wave p:360 a:±90 TODO: fix e.g. -315°
            return Math.Abs((degrees % 360 + 270) % 360 - 180) - 90; 
        }

        private static double ToRadians(this double value) => value * Math.PI / 180.0;

        public static int MetresToMiles(this double value) => (int)(value / 1609.344);
    }
}
