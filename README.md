# MessageQueue-Project
A system for managing queues of text messages

# The system includes the following functions:
• Show names (Keys) of all groups existing in the system - O(k)
• Insert a message into the queue by Key - O(1)
• Extract (remove) a message from the queue by key - removes the oldest message in this queue - - O(1)
• Extract (output) the oldest message in the system - O(1)
• Return (without deleting from the system) the K oldest or newest messages
• Return (without deleting from the system) all messages newer / older than the given date and return:
o Message itself
o Date on which it was received
o To which queue it was originally sent
• Find in a certain queue (according to the Key) all the messages that contain a certain word and return:
o Message itself
o Date on which it was received
o To which queue it was originally sent
