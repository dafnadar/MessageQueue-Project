using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logic
{
    public class Manager
    {
        readonly DoubleLinkedList1051<Message> messagesList;
        readonly HashTable1051<string, Queue1051<DoubleLinkedList1051<Message>.Node>> messagesHashTable;

        public Manager()
        {
            messagesList = new DoubleLinkedList1051<Message>();
            messagesHashTable = new HashTable1051<string, Queue1051<DoubleLinkedList1051<Message>.Node>>();
        }

        public string[] GetGroupNames()
        {
            string[] names = new string[messagesHashTable.ItemsCount];
            int i = 0;
            foreach (KeyValuePair<string, Queue1051<DoubleLinkedList1051<Message>.Node>> item in messagesHashTable)
                names[i++] = item.Key;

            return names;
        }

        /// <summary>
        /// Add message to the end of group and main list 
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="message"></param>
        public void SendMessageToGroup(string groupName, string message)
        {
            Message newMessage = new Message(groupName, message);

            Queue1051<DoubleLinkedList1051<Message>.Node> groupQueue;
            if (!messagesHashTable.ContainsKey(groupName))
            {
                groupQueue = new Queue1051<DoubleLinkedList1051<Message>.Node>();
                messagesHashTable.Add(groupName, groupQueue);
            }
            else groupQueue = messagesHashTable[groupName];

            messagesList.AddLast(newMessage);
            groupQueue.EnQueue(messagesList.End);
        }

        /// <summary>
        /// Delete oldest message from group and main messages list, and show its' details to user.
        /// If Group does not exist or empty - returns failure.
        /// </summary>
        /// <returns> seccess or failer</returns>
        public bool CutOldestMessageFromGroup(string groupName, out UserMessage oldMessage)
        {
            Queue1051<DoubleLinkedList1051<Message>.Node> groupQueue = messagesHashTable[groupName];
            if (groupQueue==null || groupQueue.Count() == 0)
            {
                oldMessage = default;
                return false;
            }

            groupQueue.Peek(out DoubleLinkedList1051<Message>.Node node);
            oldMessage = new UserMessage(node.Value.GroupName, node.Value.MessageText, node.Value.MessageDate);
            groupQueue.DeQueue();
            return messagesList.RemoveNode(node);
        }

        /// <summary>
        /// Delete oldest message in system (from group and main list), and show its' details to user.
        /// If main list is empty - returns failure.
        /// </summary>
        /// <returns> seccess or failer</returns>
        public bool CutOldestMessage(out UserMessage oldMessage)
        {
            if (messagesList.Count == 0)
            {
                oldMessage = default;
                return false;
            }

            messagesList.GetAt(0, out Message msg);
            oldMessage = new UserMessage(msg.GroupName, msg.MessageText, msg.MessageDate);

            messagesHashTable[msg.GroupName].DeQueue();
            return messagesList.RemoveFirst();
        }

        /// <summary>
        /// Returns the oldest messages at the user's request.
        /// If main list is empty - returns failure.
        /// If the requested number is smaller than the total number of messages - 
        ///     it sends a partial list and returns a failure.
        /// </summary>
        /// <param name="k">Number of old messages to show (at the user's request)</param>
        /// <param name="oldMessages"></param>
        /// <returns></returns>
        public bool GetKOldestMessages(int k, out UserMessage[] oldMessages)
        {
            if (messagesList.Count == 0)
            {
                oldMessages = default;
                return false;
            }

            oldMessages = new UserMessage[k];

            int i = 0;
            foreach (Message msg in messagesList)
            {
                oldMessages[i++] = new UserMessage(msg.GroupName, msg.MessageText, msg.MessageDate);
                if (i == k) break;
            }

            return i >= k;
        }

        /// <summary>
        /// Returns the newest messages at the user's request.
        /// If main list is empty - returns failure.
        /// If the requested number is smaller than the total number of messages - 
        ///     it sends a partial list and returns a failure.
        /// </summary>
        /// <param name="k">Number of new messages to show (at the user's request)</param>
        /// <param name="newMessages"></param>
        /// <returns></returns>
        public bool GetKNewestMessages(int k, out UserMessage[] newMessages)
        {
            if (messagesList.Count == 0)
            {
                newMessages = default;
                return false;
            }

            newMessages = new UserMessage[k];
            IEnumerator<Message> reversedList = messagesList.GetEnumeratorReverse();

            int i = 0;
            while (reversedList.MoveNext() && i < k)
                newMessages[i++] = new UserMessage(reversedList.Current.GroupName,
                    reversedList.Current.MessageText, reversedList.Current.MessageDate);

            return i >= k;
        }

        /// <summary>
        /// Returns messages from a date at the user's request.
        /// If main list is empty or no results - returns failure.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="messagesFromDate"></param>
        /// <returns></returns>
        public bool GetMessagesFromDate(DateTime date, out LinkedList<UserMessage> messagesFromDate)
        {
            if (messagesList.Count == 0)
            {
                messagesFromDate = default;
                return false;
            }

            messagesFromDate = new LinkedList<UserMessage>();
            IEnumerator<Message> reversedList = messagesList.GetEnumeratorReverse();

            while (reversedList.MoveNext() && reversedList.Current.MessageDate >= date)
                messagesFromDate.AddFirst(new UserMessage(reversedList.Current.GroupName,
                    reversedList.Current.MessageText, reversedList.Current.MessageDate));

            return (messagesFromDate.Count != 0);
        }

        /// <summary>
        /// Returns messages up to date at user's request.
        /// If main list is empty or no results - returns failure.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="messagesByDate"></param>
        /// <returns></returns>
        public bool GetMessagesByDate(DateTime date, out LinkedList<UserMessage> messagesByDate)
        {
            if (messagesList.Count == 0)
            {
                messagesByDate = default;
                return false;
            }

            messagesByDate = new LinkedList<UserMessage>();

            foreach (Message msg in messagesList)
            {
                if (msg.MessageDate > date) break;
                messagesByDate.AddLast(new UserMessage(msg.GroupName, msg.MessageText, msg.MessageDate));
            }

            return (messagesByDate.Count != 0);
        }

        /// <summary>
        /// Returns messages from a group that include a specific word at the user's request.
        /// Returns failure in one of three cases:  1. Group does not exist
        ///                                         2. Group is empty
        ///                                         3. No Results.      
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="word"></param>
        /// <param name="msgWithWord"></param>
        /// <returns></returns>
        public bool GetMessagesWithWord(string groupName, string word, out LinkedList<UserMessage> msgWithWord)
        {             
            Queue1051<DoubleLinkedList1051<Message>.Node> groupQueue = messagesHashTable[groupName];
            if (groupQueue == null || groupQueue.Count() == 0)
            {
                msgWithWord = default;
                return false;
            }

            msgWithWord = new LinkedList<UserMessage>();

            foreach (var item in groupQueue)
            {
                string[] wordsArr = Regex.Split(item.Value.MessageText, @"\W+");
                foreach (string cell in wordsArr)
                {
                    if (cell.ToLower() == word.ToLower())
                    {
                        msgWithWord.AddLast(new UserMessage(item.Value.GroupName, item.Value.MessageText, item.Value.MessageDate));
                        break;
                    }
                }
            }
            return (msgWithWord.Count != 0);
        }
    }
}
