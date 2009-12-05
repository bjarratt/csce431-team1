using System.Collections;
using System.Linq;

namespace AutoTune.Models
{
	public class MessageRecipient : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"MessageID", "EmployeeID"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "MessageRecipients"; }

		public static MessageRecipient[] Find(Hashtable conditions)
		{
			return Find("MessageRecipients", () => new MessageRecipient(), conditions).Cast<MessageRecipient>().ToArray();
		}

		public int? MessageID
		{ get { return (int?)this["MessageID"]; } }
		public int? EmployeeID
		{ get { return (int?)this["EmployeeID"]; } }

		public Message GetMessage()
		{
			return Message.Find((int) MessageID);
		}

		public Employee GetRecipient()
    {
    	return Employee.Find((int) EmployeeID);
    }
	}
}
