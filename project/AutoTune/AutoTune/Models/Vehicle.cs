﻿using System.Collections.Generic;
using System.Linq;

namespace AutoTune.Models
{
	public class Vehicle : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"VehicleID", "VehicleTradeID"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public string Identifier
		{
			get { return (string)this["Identifier"]; }
			set { this["Identifier"] = value; }
		}

		public int Year
		{
			get { return (int)this["Year"]; }
			set { this["Year"] = value; }
		}

		public string Make
		{
			get { return (string)this["make"]; }
			set { this["Make"] = value; }
		}

		public string Model
		{
			get { return (string)this["Model"]; }
			set { this["Model"] = value; }
		}

		public string Trim
		{
			get { return (string)this["Trim"]; }
			set { this["Trim"] = value; }
		}

		public double BookValue
		{
			get { return (double)this["BookValue"]; }
			set { this["BookValue"] = value; }
		}

		public double BaseValue
		{
			get { return (double)this["BaseValue"]; }
			set { this["BaseValue"] = value; }
		}

		public double DiscountValue
		{
			get { return (double)this["DiscountValue"]; }
			set { this["DiscountValue"] = value; }
		}

		public string ImageUri
		{
			get { return (string)this["ImageUri"]; }
			set { this["ImageUri"] = value; }
		}

		public override string TableName()
		{ return "Vehicles"; }

		public static IEnumerable<Vehicle> FindAll()
		{
			return Find("Vehicles", () => new Vehicle(), null).Cast<Vehicle>();
		}
	}
}
