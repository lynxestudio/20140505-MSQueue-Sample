using System;

namespace Samples{
	public class TestMessageQueue{
		public static void Main(string[] args){
			bool isTransactional = false;
			Console.WriteLine("\nTest MSMQ\n");
			try
			{
				Console.Write("\nEnter path > ");
				string path = Console.ReadLine();
				Console.Write("\nIs transactional? (1=true/0=false) > ");
				string sTransactional = Console.ReadLine();
				if(sTransactional.Equals("1"))
					isTransactional = true;
				Console.Write("\nEnter a label > ");
				string label = Console.ReadLine();
				Guid guid = MessageQueueHelper.CreateQueue(path,isTransactional,label);
				Console.WriteLine("\nMessageQueue created with guid {0}\n",guid);
				Console.Write("\nMessage subject > ");
				string subject = Console.ReadLine();
				Console.Write("\nMessage to send > ");
				string message = Console.ReadLine();
				Console.WriteLine();
				MessageQueueHelper.SendMessage(path,message,subject);
			}
			catch(Exception ex){
				Console.WriteLine(ex.Message);
			}
		}

	}
}
