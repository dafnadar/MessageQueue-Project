# MessageQueue-Project
A system for managing queues of text messages.

Details of each message:<br/>
• Text message <br/>
• Date Received <br/>
• Queue to which it was originally sent<br/>

# The system includes the following functions:
- Show names (Keys) of all groups existing in the system - O(k) <br/>
- Insert a message into the queue by Key - O(1) <br/>
- Extract (remove) a message from the queue by key - removes the oldest message in this queue - O(1) <br/>
- Extract (output) the oldest message in the system - O(1) <br/>
- Return (without deleting from the system) the K oldest or newest messages  <br/>
- Return (without deleting from the system) all messages newer / older than the given date and return message details. <br/>
- Find in a certain queue (according to the Key) all the messages that contain a certain word and return message details. <br/>

