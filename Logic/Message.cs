using System;

namespace Logic
{
    internal class Message
    {
        internal string MessageText { get; set; }
        internal DateTime MessageDate { get; set; }
        internal string GroupName { get; set; }         

        public Message(string groupName, string message)
        {
            MessageText = message;
            MessageDate = DateTime.Now;
            GroupName = groupName;
        }
    }
}
