﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoTune.Models
{
	public class Message : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"Sender", "Body", "Deleted"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "Messages"; }

		public Message() : base()
		{
			this["Deleted"] = false;
		}

		public Employee Sender
		{
			get
			{
				return Employee.Find((int)this["Sender"]);
			}
			set
			{
				this["Sender"] = value.ID;
			}
		}

		public string Body
		{
			get { return (string)this["Body"]; }
			set { this["Body"] = value; }
		}

		public bool Deleted
		{ get { return (bool)this["Deleted"]; } }

		public void Delete()
		{
			this["Deleted"] = true;
			Commit();
		}

		public Employee[] GetRecipients()
		{
			return
				FindChildren("Employees", () => new Employee(), "MessageRecipients", "MessageID", "EmployeeID").Cast<Employee>().ToArray();
		}

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

		public void SendTo(Employee recipient)
		{
			AttachTo(recipient, () => new MessageRecipient(), "EmployeeID", "MessageID");
		}

		public void SendTo(IEnumerable<Employee> recipients)
		{
			foreach(Employee employee in recipients)
				SendTo(employee);
		}
	}
}
