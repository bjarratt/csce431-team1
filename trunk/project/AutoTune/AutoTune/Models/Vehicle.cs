using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoTune.Models
{
	public class Vehicle : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"DealershipID", "Identifier", "Year", "Make", "Model", "Trim", "BookValue", "BaseValue", "DiscountValue", "ImageUri", "VehicleSaleID"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public Dealership Location
		{
			get { return (Dealership) Dealership.Find((int) this["DealershipID"]); }
			set { this["DealershipID"] = value.ID; }
		}

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
			get { return (string)this["Make"]; }
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
			get {
				Type temp = this["BookValue"].GetType(); return (System.Single)this["BookValue"]; }
			set { this["BookValue"] = value; }
		}

		public double BaseValue
		{
			get { return (System.Single)this["BaseValue"]; }
			set { this["BaseValue"] = value; }
		}

		public double DiscountValue
		{
			get { return (System.Single)this["DiscountValue"]; }
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

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", Year, Make, Model);
		}
	}
}
