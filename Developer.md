# Developer Guide - My Cybersecurity Chatbot

This document provides technical details for developers who want to understand, modify, or extend the My Cybersecurity Chatbot application.

---

## Architecture Overview

The application follows a modular architecture with clear separation of concerns:

- **UI Layer**: WPF XAML files (MainWindow.xaml, App.xaml)
- **Business Logic Layer**: Chatbot core classes
- **Data Layer**: JSON storage and file management
- **Feature Modules**: Quiz, Tasks, Logging, Audio

---

## Class Documentation

### Core Classes

#### ChatBot
The main orchestrator that processes all user input and routes requests to appropriate feature modules.

**Key Methods:**
- `ProcessInput(string input)` - Main entry point for all user messages
- `RefreshTaskList(ListBox listBox)` - Updates the task UI
- `ToggleTask(int id)` - Marks a task as complete/incomplete
- `GetChatHistory()` - Returns all chat records from storage

**Dependencies:**
- KeywordResponder
- SentimentDetector
- TaskManager
- QuizManager
- ActivityLog
- Database

---

#### KeywordResponder
Simulates NLP by matching user input against predefined keyword lists.

**Keyword Categories:**
- Password / Passphrase
- Phishing / Suspicious
- Task / Reminder
- Quiz / Test
- Log / Activity
- General Cybersecurity

**Response Storage:**
- Uses Dictionary with string[] keys and List<string> values
- Random selection from multiple responses per category

---

#### SentimentDetector
Analyzes user input to detect emotional tone.

**Supported Sentiments:**
- Neutral
- Happy
- Sad
- Worried
- Frustrated
- Curious
- Angry
- Confused
- Excited
- Fearful

**Scoring System:**
- Each sentiment has weighted keywords
- Scores are aggregated and highest score determines sentiment
- Returns appropriate empathetic response

---

#### QuizManager
Manages the cybersecurity quiz functionality.

**Features:**
- 12 pre-loaded cybersecurity questions
- Multiple choice with 4 options (A, B, C, D)
- Instant feedback with explanations
- Score tracking
- Sequential question flow

**Question Structure:**
```csharp
new QuizQuestion(
    "Question text",
    "Option A",
    "Option B", 
    "Option C",
    "Option D",
    correctAnswerIndex,  // 0=A, 1=B, 2=C, 3=D
    "Explanation text"
)
