# Cybersecurity Awareness Bot — Part 2
### GUI Interface · Dynamic Responses · Sentiment Detection · Memory & Recall

---

## Table of Contents
1. [Project Overview](#project-overview)
2. [Prerequisites & Setup](#prerequisites--setup)
3. [How to Run](#how-to-run)
4. [GUI Layout & Features](#gui-layout--features)
5. [Feature Demonstrations](#feature-demonstrations)
6. [Keyword Reference](#keyword-reference)
7. [Requirements Coverage](#requirements-coverage)
8. [Project Structure](#project-structure)
9. [Version History](#version-history)

---

## Project Overview

Part 2 expands the **Cybersecurity Awareness Chatbot** (Part 1 console app) into a
fully featured **WinForms GUI application** targeting **.NET 8 on Windows**.

The chatbot educates South African users about cybersecurity threats — phishing,
scams, malware, privacy, passwords, VPNs, and more — through natural, personalised
conversation enhanced by sentiment detection and persistent memory.

**Module:** PROG6221  
**Institution:** The Independent Institute of Education (IIE)  
**GitHub:** [Insert your GitHub repository link here]  
**Video Presentation:** [Insert your YouTube unlisted link here]

---

## Prerequisites & Setup

### Required Software

| Software | Version | Download |
|----------|---------|----------|
| .NET SDK | 8.0 or later | https://dotnet.microsoft.com/download/dotnet/8.0 |
| Windows OS | Windows 10 / 11 | — |
| Visual Studio (optional) | 2022 v17.8+ | https://visualstudio.microsoft.com |

> **Note:** WinForms is a Windows-only technology. The app will not build on macOS or Linux.

### Verify Installation
Open a terminal and run:
```bash
dotnet --version
# Should output: 8.x.x
```

---

## How to Run

### Option A — Visual Studio 2022 (Recommended)
1. Open **Visual Studio 2022**
2. Click **File → Open → Project/Solution**
3. Select `CybersecurityAwarenessBot.csproj`
4. Press **F5** to build and run (or Ctrl+F5 for without debugger)

### Option B — .NET CLI (Terminal)
```bash
# Navigate to the project folder
cd CybersecurityAwarenessBot

# Restore NuGet packages (first run only)
dotnet restore

# Build and run
dotnet run
```

### Option C — Run the Published Executable
If a pre-built release is available in the GitHub Releases section:
1. Download the `.zip` from the latest release
2. Extract to any folder
3. Double-click `CybersecurityAwarenessBot.exe`

---

## GUI Layout & Features

```
┌─────────────────────────────────────────────────────────┬──────────────┐
│  🛡 CYBERSECURITY AWARENESS BOT                          │  BOT MEMORY  │
│  ██████╗██╗   ██╗██████╗ ███████╗██████╗  | Stay Safe   │  Name: ...   │
│  Stay Safe · Stay Informed · Stay Secure                 │  Topic: ...  │
├─────────────────────────────────────────────────────────│  Msgs: ...   │
│                                                         │──────────────│
│   Chat Display Area (RichTextBox)                       │ MOOD DETECTED│
│                                                         │  😐 Neutral   │
│   🤖 CyberBot:  [responses appear here in cyan]         │──────────────│
│   You (Name):   [user messages appear in green]         │ QUICK TOPICS │
│                                                         │ [Passwords]  │
│                                                         │ [Phishing]   │
│                                                         │ [Scams]      │
│                                                         │ [Privacy]    │
├─────────────────────────────────────────────────────────│ [Malware]    │
│  [Help] [Clear]  [ Type your message here... ] [Send ▶] │ [VPN] [2FA]  │
├─────────────────────────────────────────────────────────┴──────────────┤
│  🟢 Connected  |  Chatting as [Name]                                   │
└────────────────────────────────────────────────────────────────────────┘
```

**Key UI elements:**
- **Header:** ASCII art logo + subtitle (translated from Part 1 console output)
- **Chat Display:** Colour-coded RichTextBox — cyan for bot, green for user
- **Side Panel:** Live memory readout + real-time mood indicator + quick-topic buttons
- **Input Area:** TextBox, Send button (or press Enter), Help and Clear shortcuts
- **Status Bar:** Shows connection status and current user name

---

## Feature Demonstrations

### 1. Voice Greeting
The bot speaks a welcome message aloud when the application starts.
- **Primary:** Plays `greeting.wav` if placed in the same folder as the `.exe`
- **Fallback:** Uses Windows Text-to-Speech (`System.Speech.Synthesis`) automatically

### 2. Name Collection & Memory
```
Bot:  Hello! Welcome to the Cybersecurity Awareness Bot.
      Please enter your name to get started:

You:  Caleb

Bot:  Great to meet you, Caleb! 🎉
      I'll remember your name throughout our conversation.
```
Type `memory` or `what do you remember` at any time:
```
You:  memory

Bot:  Here is everything I remember about you, Caleb:
       👤  Name:             Caleb
       ⭐  Favourite topic:  Passwords
       📚  Topics explored: Passwords, Phishing, VPN
       💬  Messages sent:   6
```

### 3. Keyword Recognition
```
You:  Tell me about password safety.

Bot:  Use strong, unique passwords for every account. A strong password
      has at least 12 characters mixing uppercase, lowercase, numbers,
      and symbols. Avoid personal details like your name or birthday.

      ✨ I'll remember that you're interested in Passwords, Caleb.
```

### 4. Random Responses
Ask the same question multiple times — you'll get a different answer each time:
```
You:  Give me a phishing tip.
Bot:  [Response A — randomly selected from 4 options]

You:  Give me a phishing tip.
Bot:  [Response B — a different tip]
```

### 5. Conversation Flow — Follow-ups
```
You:  Tell me about VPNs.
Bot:  A VPN encrypts all your internet traffic and hides your real IP...

You:  Tell me more.
Bot:  Always connect via a VPN on public Wi-Fi — airports, coffee shops...
      🔗 We're going deep on VPN — great focus!
```

**Logical topic progression:**
```
You:  Tell me about passwords.
Bot:  [Password tip]

You:  Now tell me about 2FA.
Bot:  [2FA tip]
      🔗 This connects perfectly with what we discussed about passwords
         — they work best together.
```

### 6. Sentiment Detection
```
You:  I'm really worried about online scams.

Bot:  It's completely understandable to feel that way — online threats
      are real and scammers can be very convincing. Let me share some
      practical steps to help you feel more in control.

      [Scam advice]

      💛 Remember: feeling aware of the risks is actually a strength.
         The fact that you're asking means you're already ahead of most people.
```

The side panel updates in real-time with the detected mood and a colour indicator.

### 7. Error Handling
```
You:  ajsdhaksjdhaksd

Bot:  I'm not sure I understand that — could you try rephrasing?

      Here are the cybersecurity topics I can help with:
        🔑 passwords  🎣 phishing  💰 scams  🔒 privacy  🦠 malware
        🎭 social engineering  🌐 VPN  📱 2FA  🖥️ safe browsing  💾 backups
```
No crashes, no exceptions — all edge cases are handled gracefully.

### 8. Exiting
```
You:  bye

Bot:  Goodbye, Caleb! 👋

      Stay safe online and remember:
        • Use strong, unique passwords
        • Enable 2FA on all accounts
        • Stay alert for phishing and scams
```

---

## Keyword Reference

| Topic | Recognised Keywords |
|-------|-------------------|
| Passwords | password, passwords, passphrase, credential |
| Phishing | phishing, phish, fake email, suspicious email, spam |
| Scams | scam, scams, fraud, con, swindle, trick |
| Privacy | privacy, private, personal data, data protection, popia |
| Malware | malware, virus, ransomware, spyware, trojan, worm, infected |
| Social Engineering | social engineering, manipulation, vishing, smishing, pretexting |
| VPN | vpn, virtual private network, tunnel |
| 2FA / MFA | 2fa, two-factor, mfa, multi-factor, authenticator, otp |
| Safe Browsing | browsing, browser, https, website, internet safety |
| Backups | backup, back up, data loss, restore, recovery |
| Exit | exit, quit, bye, goodbye, close, stop, end |

---

## Requirements Coverage

| # | Requirement | Implementation | Class |
|---|---|---|---|
| 1 | GUI Design | Dark-themed WinForms, ASCII art logo, voice greeting, side panel | `MainForm.cs` |
| 2 | Keyword Recognition | 10 keyword groups, 50+ synonyms, dictionary-style matching | `ResponseEngine.cs` |
| 3 | Random Responses | `List<string>` pools of 3–4 per topic, random index selection | `ResponseEngine.cs` |
| 4 | Conversation Flow | Follow-up detection, logical topic transition notes | `ResponseEngine.cs` |
| 5 | Memory and Recall | Seamless recall on every message, topic history, summary command | `ChatbotMemory.cs` |
| 6 | Sentiment Detection | 5 sentiments, empathy prefix + content suffix per mood | `SentimentDetector.cs` |
| 7 | Error Handling | Graceful fallback, no crashes, exit detection | `ResponseEngine.cs` |
| 8 | Code Optimisation | OOP classes, `List`, `HashSet`, XML docs, `static`/`readonly` | All files |

---

## Project Structure

```
CybersecurityAwarenessBot/
├── CybersecurityAwarenessBot.csproj   .NET 8 WinForms project file
├── Program.cs                          Application entry point [STAThread]
├── MainForm.cs                         GUI window — chat display, side panel, controls
├── VoiceGreeting.cs                    Voice greeting (WAV + TTS fallback)
├── ChatbotMemory.cs                    Memory & Recall — stores name, topics, history
├── SentimentDetector.cs                Sentiment Detection — 5 moods, dynamic responses
├── ResponseEngine.cs                   Core logic — keyword matching, random responses,
│                                       conversation flow, error handling
├── greeting.wav                        (Optional) Voice greeting audio from Part 1
└── README.md                           This file
```

---

## Version History

| Tag | Description |
|-----|-------------|
| v1.0 | Initial Part 1 console application |
| v1.1 | Added keyword recognition and response engine |
| v2.0 | Part 2: WinForms GUI with dark cyber theme |
| v2.1 | Added sentiment detection and memory panel |
| v2.2 | Added voice greeting (TTS + WAV), conversation flow |

---

