using System.Collections;
using System.Linq;

namespace AutoTune.Models
{
	public class Message : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"Sender", "Body", "Added"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "Messages"; }

		public Employee[] Recipients
		{
			get
			{
				MessageRecipient[] recipients = MessageRecipient.Find(new Hashtable {{"MessageID", ID}});
				Employee[] employees = new Employee[recipients.Length];
				for (int i = 0; i < recipients.Length; ++i)
					employees[i] = recipients[i].GetRecipient();

				return employees;
			}
		}

		public static Message Find(int id)
		{ return (Message)Find("Messages", () => new Message(), new Hashtable { { "ID", id }}).First(); }
	}
}
