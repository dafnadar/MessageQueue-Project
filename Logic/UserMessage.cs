using System;

namespace Logic
{
    public class UserMessage
    {
        public string MessageText { get; private set; }
        public DateTime MessageDate { get; set; }
        public string GroupName { get; set; }

        public UserMessage(string groupName, string message, DateTime date)
        {
            MessageText = message;
            MessageDate = date;
            GroupName = groupName;
        }

        public override string ToString()
        {
            return $"\nFrom Group: {GroupName}\nMessage: {MessageText}\nFrom Date: {MessageDate}\n";
        }
    }
}
