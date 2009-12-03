using MySql.Data.MySqlClient;

namespace AutoTune.Models
{
	public abstract partial class DatabaseModel
	{
		protected static MySqlConnection Connection;
		private const string ConnectionString = "datasource=database2.cs.tamu.edu;username=ironize;password=autotune;database=ironize";
		protected static void OpenConnection()
		{
			Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
		}

		protected static void CloseConnection()
		{
			Connection.Close();
		}
	}
}
