# Frequently Asked Questions

## General Questions

### What is My Cybersecurity Chatbot?
A Windows desktop application that educates users about cybersecurity through chat interaction, quizzes, and task management.

### Is this application free?
Yes, this is open-source software released under the MIT License.

### Do I need an internet connection?
No, the application works completely offline.

## Technical Questions

### What technologies does it use?
- C# with .NET 8.0
- WPF for the user interface
- JSON for data storage
- Newtonsoft.Json for serialization

### Where is my data stored?
All data is stored locally in your Documents folder as JSON files.

### How do I backup my data?
Copy the following files from your Documents folder:
- chat.json
- CyberTasks.json

## Feature Questions

### How many quiz questions are there?
The quiz has 12 questions covering various cybersecurity topics.

### Can I add my own quiz questions?
Yes, you can modify QuizManager.cs to add more questions.

### How do reminders work?
The application detects keywords like "tomorrow" or "next week" in task descriptions and sets reminders accordingly.

### What happened to my chat history?
Chat history is saved in chat.json in your Documents folder.

## Troubleshooting

### The quiz isn't displaying
- Ensure you typed "start quiz" correctly
- Check that the quiz loaded properly

### Tasks aren't saving
- Verify Documents folder permissions
- Check if CyberTasks.json exists

### No audio when starting
- Ensure greetings (1).wav is in the executable folder
- Check if your system audio is enabled
