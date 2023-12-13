using System;
using System.Messaging;

namespace Samples{
	public class MessageQueueHelper{
		static MessageQueue queue = null;
		public static Guid CreateQueue(string queuePath,
				bool isTransactional,
				string label)
		{
			if(MessageQueue.Exists(queuePath))
				queue = new MessageQueue(queuePath);
			else
			{
				queue = MessageQueue.Create(queuePath,isTransactional);
				queue.Label = label;
			}
			return queue.Id;

		}

		public static void SendMessage(string queuePath,
				string message,string subject)
		{
			message += "\nCreated on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
			MessageQueueTransaction trans = null;
			if(MessageQueue.Exists(queuePath))
			{
				queue = new MessageQueue(queuePath);
				queue.Formatter = new XmlMessageFormatter();
				if(queue.Transactional)
				{
				trans = new MessageQueueTransaction();
				trans.Begin();
				}
				queue.Send(message,subject);

				if(queue.Transactional)
					trans.Commit();
				Console.WriteLine("Message sent.");
			}else
				Console.WriteLine("The queue doesn't exist");
		}

		public static string GetMessage(string queuePath)
		{
			string message = null;
			Message queueMessage = null;
			if(MessageQueue.Exists(queuePath))
			{
				queue = new MessageQueue(queuePath);
				queue.Formatter = new XmlMessageFormatter(new Type[]{
						Type.GetType("System.String")
						});
				queueMessage = queue.Receive(new TimeSpan(0,0,2));
				message = queueMessage.Body.ToString();
			}
			else
				message = "That queue doesn't exist";
			return message;
		}

	}
}
