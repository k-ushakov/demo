using AirportDistanceCalculator.Core;
using AirportDistanceCalculator.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AirportDistanceCalculator.Tests
{
	[TestClass]
	public class Calculator_Tests
	{
		[TestMethod]
		public void Calculate_Distance_AMS_DME()
		{
			var first = new Location { Longitude = 4.763385, Latitude = 52.309069  }; // AMS
			var second = new Location { Longitude = 37.899494, Latitude = 55.414566 }; // DME

			// 1353.82359172486
			var miles = Calculator.DistanceBetween(first, second).MetresToMiles();

			Assert.AreEqual(1353, miles);
		}

		[TestMethod]
		public void Calculate_Distance_Between_Zero()
		{
			var first = new Location { Longitude = 0, Latitude = 0 };
			var second = new Location { Longitude = 0, Latitude = 0};

			var miles = Calculator.DistanceBetween(first, second).MetresToMiles();

			Assert.AreEqual(0, miles);
		}

		[TestMethod]
		public void Calculate_Distance_Between_1000()
		{
			var first = new Location { Longitude = 0, Latitude = 0 };
			var second = new Location { Longitude = 1000, Latitude = 1000 };

			var metres = Calculator.DistanceBetween(first, second);

			Assert.IsTrue(metres > 0);
			Assert.IsTrue(metres < (2 * Math.PI * Calculator.Radius));
		}

		[TestMethod]
		public void Calculate_Distance_Between_Minus1000()
		{
			var first = new Location { Longitude = 0, Latitude = 0 };
			var second = new Location { Longitude = -1000, Latitude = -1000 };

			var metres = Calculator.DistanceBetween(first, second);

			Assert.IsTrue(metres > 0);
			Assert.IsTrue(metres < (2 * Math.PI * Calculator.Radius));
		}
	}
}
