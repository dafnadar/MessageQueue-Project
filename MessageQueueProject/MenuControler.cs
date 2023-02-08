using Logic;
using System;
using System.Collections.Generic;

namespace MessageQueueProject
{
    internal class MenuControler
    {
        private ActionMenu choice;
        readonly Manager m = new Manager();

        private enum ActionMenu
        {
            GetGroupNames = 1,
            SendMessage,
            DeleteOldMessageInGroup,
            DeleteOldestMessage,
            ReadKOldestMessages,
            ReadKNewestMessages,
            ReadMessagesFromDate,
            ReadMessagesByDate,
            ReadMessagesWithWord,
            Exit
        }

        public void ShowMenu()
        {
            do
            {
                Console.WriteLine($"Menu:");
                Console.WriteLine("1 - Show all group names");
                Console.WriteLine("2 - Send message");
                Console.WriteLine("3 - Delete oldest message in group");
                Console.WriteLine("4 - Delete oldest message in system");
                Console.WriteLine("5 - Read K Oldest Messages");
                Console.WriteLine("6 - Read K Newest Messages");
                Console.WriteLine("7 - Read Messages From Date");
                Console.WriteLine("8 - Read Messages By Date");
                Console.WriteLine("9 - Read messages from group with a specific word");
                Console.WriteLine("10 - Exit\n");
                Console.Write("Your choice is: ");

                bool isValid;
                do
                {
                    isValid = Enum.TryParse(Console.ReadLine(), out choice);
                    if (isValid) ActChoice();
                    else Console.Write("Invalid key!\nYour choice is: ");
                }
                while (!isValid);

                Console.WriteLine("Press any key for menu..\n");
                Console.ReadKey();
            }
            while (choice != ActionMenu.Exit);
        }

        private void ActChoice()
        {
            switch (choice)
            {
                case ActionMenu.GetGroupNames:
                    PrintGroupNames();
                    break;
                case ActionMenu.SendMessage:
                    SendMessage();
                    break;
                case ActionMenu.DeleteOldMessageInGroup:
                    DeleteAndPrintOldestInGroup();
                    break;
                case ActionMenu.DeleteOldestMessage:
                    DeleteAndPrintOldestInSystem();
                    break;
                case ActionMenu.ReadKOldestMessages:
                    PrintKOldestMessages();
                    break;
                case ActionMenu.ReadKNewestMessages:
                    PrintKNewestMessages();
                    break;
                case ActionMenu.ReadMessagesFromDate:
                    PrintMessagesFromDate();
                    break;
                case ActionMenu.ReadMessagesByDate:
                    PrintMessagesByDate();
                    break;
                case ActionMenu.ReadMessagesWithWord:
                    PrintMessagesWithWord();
                    break;
                case ActionMenu.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
            Console.WriteLine("");
        }

        private void PrintMessagesWithWord()
        {
            Console.Write("Search the group: ");
            string group = Console.ReadLine();
            Console.Write("Messages with the word: ");
            string word = Console.ReadLine();

            bool isSuccess = m.GetMessagesWithWord(group, word, out LinkedList<UserMessage> msgWithWord);

            if (msgWithWord == null)
            {
                Console.WriteLine("No messages in the system !");
                return;
            }

            if (!isSuccess) Console.WriteLine("\nNo Messages !");
            else
            {
                Console.WriteLine("\n------------Results:------------");
                foreach (UserMessage msg in msgWithWord)
                    Console.WriteLine($"\nMessage: {msg.MessageText}\n" +
                        $"From Date: {msg.MessageDate}\nFrom Group: {msg.GroupName}");
            }
        }

        private void PrintMessagesByDate()
        {
            bool isDateOk;
            DateTime userDate;
            do
            {
                Console.Write("Read messages by date (dd/mm/yy): ");
                isDateOk = DateTime.TryParse(Console.ReadLine(), out userDate);
            }
            while (!isDateOk);

            bool isSuccess = m.GetMessagesByDate(userDate, out LinkedList<UserMessage> msgByDate);

            if (msgByDate == null)
            {
                Console.WriteLine("No messages in the system !");
                return;
            }

            if (!isSuccess) Console.WriteLine("No messages by requested date !");
            else
            {
                Console.WriteLine("\n------------Results:------------");
                foreach (UserMessage msg in msgByDate)
                    Console.WriteLine($"\nMessage: {msg.MessageText}\nFrom Date: {msg.MessageDate}\nFrom Group: {msg.GroupName}");
            }
        }

        private void PrintMessagesFromDate()
        {
            bool isDateOk;
            DateTime userDate;
            do
            {
                Console.Write("Read messages from date (dd/mm/yy): ");
                isDateOk = DateTime.TryParse(Console.ReadLine(), out userDate);
            }
            while (!isDateOk);

            bool isSuccess = m.GetMessagesFromDate(userDate, out LinkedList<UserMessage> msgFromDate);

            if (msgFromDate == null)
            {
                Console.WriteLine("No messages in the system !");
                return;
            }

            if (!isSuccess) Console.WriteLine("No messages from requested date !");
            else
            {
                Console.WriteLine("\n------------Results:------------");
                foreach (UserMessage msg in msgFromDate)
                    Console.WriteLine($"\nMessage: {msg.MessageText}\n" +
                        $"From Date: {msg.MessageDate}\nFrom Group: {msg.GroupName}");
            }
        }

        private void PrintKNewestMessages()
        {
            bool isValid;
            int kMessages;
            do
            {
                Console.Write("Numer of newest messages to show: ");
                isValid = int.TryParse(Console.ReadLine(), out kMessages);
            }
            while (!isValid);

            bool isSuccess = m.GetKNewestMessages(kMessages, out UserMessage[] kNewMsg);

            if (kNewMsg == default)
            {
                Console.WriteLine("No messages in the system !");
                return;
            }

            Console.WriteLine((isSuccess) ? "\n------------Results:------------" : "\n------------Partial results:------------");
            foreach (UserMessage msg in kNewMsg)
                if (msg != null) Console.WriteLine($"\nMessage: {msg.MessageText}\n" +
                    $"From Date: {msg.MessageDate}\nFrom Group: {msg.GroupName}");
                else break;
        }

        private void PrintKOldestMessages()
        {
            bool isValid;
            int kMessages;
            do
            {
                Console.Write("Numer of oldest messages to show: ");
                isValid = int.TryParse(Console.ReadLine(), out kMessages);
            }
            while (!isValid);

            bool isSuccess = m.GetKOldestMessages(kMessages, out UserMessage[] kOldMsg);

            if (kOldMsg == default)
            {
                Console.WriteLine("No messages in the system !");
                return;
            }

            Console.WriteLine((isSuccess) ? "\n------------Results:------------" : "\n------------Partial results:------------");
            foreach (UserMessage msg in kOldMsg)
                if (msg != null) Console.WriteLine($"\nMessage: {msg.MessageText}\nDate: {msg.MessageDate}\nGroup: {msg.GroupName}");
                else break;
        }

        private void DeleteAndPrintOldestInSystem()
        {
            bool isSuccess = m.CutOldestMessage(out UserMessage msg);
            Console.WriteLine((isSuccess) ? $"\n------------Deleted message:------------ {msg.ToString()}" : "\nNo Message to delete !");
        }

        private void DeleteAndPrintOldestInGroup()
        {
            Console.Write("\nGroup name: ");
            string name = Console.ReadLine();
            bool isSuccess = m.CutOldestMessageFromGroup(name, out UserMessage msg);
            Console.WriteLine((isSuccess) ? $"\n------------Deleted message:------------ {msg.ToString()}" : "\nNo Message to delete !");
        }

        public void SendMessage()
        {
            Console.Write("\n--- MessageDetails ---\nGroup name: ");
            string name = Console.ReadLine();
            Console.Write("Message: ");
            string msg = Console.ReadLine();

            m.SendMessageToGroup(name, msg);
            Console.WriteLine("\nMessage Added !");
        }

        public void PrintGroupNames()
        {
            string[] names = m.GetGroupNames();
            Console.Write("\n------------Groups names:------------\n");
            foreach (string name in names)
                Console.WriteLine(name);
        }
    }
}
