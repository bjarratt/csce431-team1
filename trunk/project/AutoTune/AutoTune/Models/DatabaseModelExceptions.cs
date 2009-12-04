using System;

namespace AutoTune.Models
{
	public abstract partial class DatabaseModel
	{
		public class DatabaseException : Exception
		{
			public DatabaseException(string message) :
				base(message) { }
		}
		protected class InternalException : Exception
		{
			public InternalException(string message) :
				base(message) { }
		}
	}
}
