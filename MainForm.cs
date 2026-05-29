// ============================================================
//  MainForm.cs – GUI Interface (Requirement 1)
// ============================================================
//
//  Implements a WinForms chat interface that:
//    • Translates ALL Part 1 console features into a GUI (Req 1)
//    • Displays ASCII art logo and voice-style greeting in the GUI
//    • Collects the user's name before chat begins
//    • Uses a dark cybersecurity colour scheme for visual polish
//    • Wires together ResponseEngine, ChatbotMemory, SentimentDetector
// ============================================================

using System.Drawing.Text;
using System.Media;

namespace CybersecurityAwarenessBot
{
    /// <summary>
    /// Main WinForms window for the Cybersecurity Awareness Chatbot.
    /// Implements Requirement 1 (GUI) and coordinates all other requirements.
    /// </summary>
    public class MainForm : Form
    {
        // =================================================================
        //  Core Logic Objects
        // =================================================================
        private readonly ChatbotMemory  _memory         = new();
        private readonly ResponseEngine _responseEngine;

        // =================================================================
        //  UI Controls
        // =================================================================
        private Panel       _headerPanel    = null!;
        private Label       _logoLabel      = null!;
        private Label       _subtitleLabel  = null!;
        private RichTextBox _chatDisplay    = null!;
        private TextBox     _inputBox       = null!;
        private Button      _sendButton     = null!;
        private Button      _clearButton    = null!;
        private Button      _helpButton     = null!;
        private Panel       _inputPanel     = null!;
        private Panel       _sidePanel      = null!;
        private Label       _memoryLabel    = null!;
        private Label       _sentimentLabel = null!;
        private Panel       _statusBar      = null!;
        private Label       _statusLabel    = null!;

        // =================================================================
        //  Colours (dark cybersecurity theme)
        // =================================================================
        private static readonly Color BgDark       = Color.FromArgb(18,  20,  30);
        private static readonly Color BgMid        = Color.FromArgb(26,  30,  44);
        private static readonly Color BgPanel      = Color.FromArgb(32,  36,  52);
        private static readonly Color AccentCyan   = Color.FromArgb(0,   210, 200);
        private static readonly Color AccentGreen  = Color.FromArgb(57,  255, 130);
        private static readonly Color AccentAmber  = Color.FromArgb(255, 195, 0);
        private static readonly Color TextPrimary  = Color.FromArgb(220, 225, 240);
        private static readonly Color TextSecond   = Color.FromArgb(130, 140, 160);
        private static readonly Color BotMsgColour = Color.FromArgb(90,  200, 255);
        private static readonly Color UserMsgColor = AccentGreen;
        private static readonly Color BorderColour = Color.FromArgb(45,  55,  75);

        // State flag: waiting for user name input
        private bool _awaitingName = true;

        // =================================================================
        //  Constructor
        // =================================================================
        public MainForm()
        {
            _responseEngine = new ResponseEngine(_memory);
            InitializeComponent();
            ShowGreeting();

            // Play voice greeting in background (Req 1 – voice greeting in GUI)
            VoiceGreeting.PlayAsync();
        }

        // =================================================================
        //  UI Initialisation
        // =================================================================

        /// <summary>
        /// Builds all UI controls manually (no Designer.cs required).
        /// Implements the clean dark theme and proper spacing (Req 1).
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();

            // ── Form Properties ─────────────────────────────────────────
            Text            = "Cybersecurity Awareness Bot  |  Stay Safe Online";
            Size            = new Size(1000, 700);
            MinimumSize     = new Size(800, 560);
            BackColor       = BgDark;
            ForeColor       = TextPrimary;
            Font            = new Font("Segoe UI", 10f);
            StartPosition   = FormStartPosition.CenterScreen;

            // ── Header Panel ─────────────────────────────────────────────
            _headerPanel = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 110,
                BackColor = BgMid,
                Padding   = new Padding(16, 10, 16, 10)
            };

            _logoLabel = new Label
            {
                Text      = "🛡  CYBERSECURITY AWARENESS BOT",
                Font      = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = AccentCyan,
                AutoSize  = true,
                Location  = new Point(16, 14)
            };

            // ASCII art logo translated to GUI label (Req 1 – ASCII art in GUI)
            _subtitleLabel = new Label
            {
                Text = "  ██████╗██╗   ██╗██████╗ ███████╗██████╗  | " +
                       "Stay Safe · Stay Informed · Stay Secure",
                Font      = new Font("Courier New", 8f),
                ForeColor = Color.FromArgb(0, 140, 135),
                AutoSize  = true,
                Location  = new Point(16, 50)
            };

            var tagLine = new Label
            {
                Text      = "Your personal guide to staying safe in South Africa's digital landscape.",
                Font      = new Font("Segoe UI", 9f, FontStyle.Italic),
                ForeColor = TextSecond,
                AutoSize  = true,
                Location  = new Point(18, 82)
            };

            _headerPanel.Controls.AddRange(new Control[] { _logoLabel, _subtitleLabel, tagLine });

            // ── Side Panel (Memory + Sentiment) ──────────────────────────
            _sidePanel = new Panel
            {
                Dock      = DockStyle.Right,
                Width     = 220,
                BackColor = BgPanel,
                Padding   = new Padding(12)
            };

            var sideTitle = new Label
            {
                Text      = "BOT MEMORY",
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = AccentAmber,
                Dock      = DockStyle.Top,
                Height    = 28,
                TextAlign = ContentAlignment.MiddleLeft
            };

            _memoryLabel = new Label
            {
                Text      = "Nothing stored yet.\nTell me your name\nto get started.",
                Font      = new Font("Segoe UI", 8.5f),
                ForeColor = TextSecond,
                Dock      = DockStyle.Top,
                Height    = 80,
                Padding   = new Padding(0, 4, 0, 0)
            };

            var divider = new Label
            {
                Text      = "─────────────────",
                ForeColor = BorderColour,
                Font      = new Font("Segoe UI", 8f),
                Dock      = DockStyle.Top,
                Height    = 20
            };

            var sentimentTitle = new Label
            {
                Text      = "MOOD DETECTED",
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = AccentAmber,
                Dock      = DockStyle.Top,
                Height    = 28,
                TextAlign = ContentAlignment.MiddleLeft
            };

            _sentimentLabel = new Label
            {
                Text      = "😐  Neutral",
                Font      = new Font("Segoe UI", 9f),
                ForeColor = TextSecond,
                Dock      = DockStyle.Top,
                Height    = 30
            };

            var divider2 = new Label
            {
                Text      = "─────────────────",
                ForeColor = BorderColour,
                Font      = new Font("Segoe UI", 8f),
                Dock      = DockStyle.Top,
                Height    = 20
            };

            var topicsTitle = new Label
            {
                Text      = "QUICK TOPICS",
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = AccentAmber,
                Dock      = DockStyle.Top,
                Height    = 28,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Quick-topic buttons
            string[] quickTopics = { "Passwords", "Phishing", "Scams", "Privacy", "Malware", "VPN", "2FA" };
            var topicFlow = new FlowLayoutPanel
            {
                Dock      = DockStyle.Top,
                Height    = 170,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents  = true,
                BackColor = BgPanel
            };

            foreach (string topic in quickTopics)
            {
                string t = topic; // closure capture
                var btn = new Button
                {
                    Text      = t,
                    Font      = new Font("Segoe UI", 8f),
                    ForeColor = AccentCyan,
                    BackColor = Color.FromArgb(40, 50, 70),
                    FlatStyle = FlatStyle.Flat,
                    Margin    = new Padding(2),
                    Size      = new Size(90, 26),
                    Cursor    = Cursors.Hand
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(60, 80, 110);
                btn.Click += (_, _) => SendMessage($"Tell me about {t}");
                topicFlow.Controls.Add(btn);
            }

            // Stack controls BOTTOM to TOP in side panel (Dock=Top reverses)
            _sidePanel.Controls.Add(topicFlow);
            _sidePanel.Controls.Add(topicsTitle);
            _sidePanel.Controls.Add(divider2);
            _sidePanel.Controls.Add(_sentimentLabel);
            _sidePanel.Controls.Add(sentimentTitle);
            _sidePanel.Controls.Add(divider);
            _sidePanel.Controls.Add(_memoryLabel);
            _sidePanel.Controls.Add(sideTitle);

            // ── Chat Display ──────────────────────────────────────────────
            _chatDisplay = new RichTextBox
            {
                Dock        = DockStyle.Fill,
                ReadOnly    = true,
                BackColor   = BgDark,
                ForeColor   = TextPrimary,
                Font        = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.None,
                ScrollBars  = RichTextBoxScrollBars.Vertical,
                Padding     = new Padding(8),
                WordWrap    = true
            };

            // ── Input Panel ───────────────────────────────────────────────
            _inputPanel = new Panel
            {
                Dock      = DockStyle.Bottom,
                Height    = 56,
                BackColor = BgMid,
                Padding   = new Padding(10, 8, 10, 8)
            };

            _inputBox = new TextBox
            {
                Font        = new Font("Segoe UI", 10.5f),
                BackColor   = Color.FromArgb(30, 35, 50),
                ForeColor   = TextPrimary,
                BorderStyle = BorderStyle.FixedSingle,
                Dock        = DockStyle.Fill
            };
            _inputBox.KeyDown += InputBox_KeyDown;

            _sendButton = new Button
            {
                Text      = "Send  ▶",
                Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                BackColor = AccentCyan,
                ForeColor = BgDark,
                FlatStyle = FlatStyle.Flat,
                Dock      = DockStyle.Right,
                Width     = 90,
                Cursor    = Cursors.Hand
            };
            _sendButton.FlatAppearance.BorderSize = 0;
            _sendButton.Click += SendButton_Click;

            _clearButton = new Button
            {
                Text      = "Clear",
                Font      = new Font("Segoe UI", 9f),
                BackColor = Color.FromArgb(50, 55, 75),
                ForeColor = TextSecond,
                FlatStyle = FlatStyle.Flat,
                Dock      = DockStyle.Right,
                Width     = 60,
                Cursor    = Cursors.Hand
            };
            _clearButton.FlatAppearance.BorderColor = BorderColour;
            _clearButton.Click += (_, _) => { _chatDisplay.Clear(); ShowGreeting(); };

            _helpButton = new Button
            {
                Text      = "Help",
                Font      = new Font("Segoe UI", 9f),
                BackColor = Color.FromArgb(50, 55, 75),
                ForeColor = AccentAmber,
                FlatStyle = FlatStyle.Flat,
                Dock      = DockStyle.Right,
                Width     = 55,
                Cursor    = Cursors.Hand
            };
            _helpButton.FlatAppearance.BorderColor = BorderColour;
            _helpButton.Click += (_, _) => SendMessage("help");

            _inputPanel.Controls.Add(_inputBox);
            _inputPanel.Controls.Add(_sendButton);
            _inputPanel.Controls.Add(_clearButton);
            _inputPanel.Controls.Add(_helpButton);

            // ── Status Bar ────────────────────────────────────────────────
            _statusBar = new Panel
            {
                Dock      = DockStyle.Bottom,
                Height    = 22,
                BackColor = Color.FromArgb(15, 17, 26)
            };
            _statusLabel = new Label
            {
                Text      = "  🟢 Connected  |  Type your name to begin",
                Font      = new Font("Segoe UI", 8f),
                ForeColor = TextSecond,
                Dock      = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            _statusBar.Controls.Add(_statusLabel);

            // ── Assemble form ─────────────────────────────────────────────
            Controls.Add(_chatDisplay);
            Controls.Add(_sidePanel);
            Controls.Add(_inputPanel);
            Controls.Add(_statusBar);
            Controls.Add(_headerPanel);

            ResumeLayout(true);
            _inputBox.Focus();
        }

        // =================================================================
        //  Greeting & Startup (Req 1 – translate Part 1 greeting to GUI)
        // =================================================================

        private void ShowGreeting()
        {
            _awaitingName = true;
            UpdateStatus("Waiting for your name...");

            AppendLine("");
            AppendLine("  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ", AccentCyan, bold: true);
            AppendLine(" ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗", AccentCyan, bold: true);
            AppendLine(" ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝", AccentCyan, bold: true);
            AppendLine(" ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗", AccentCyan, bold: true);
            AppendLine(" ╚██████╗   ██║   ██████╔╝███████╗██║  ██║", AccentCyan, bold: true);
            AppendLine("  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝", AccentCyan, bold: true);
            AppendLine("");
            AppendLine("  Cybersecurity Awareness Bot  –  Keeping South Africa Safe Online",
                       AccentAmber, bold: true);
            AppendLine("  ─────────────────────────────────────────────────────────────",
                       BorderColour);
            AppendLine("");
            AppendBotMessage("Hello! Welcome to the Cybersecurity Awareness Bot.\n" +
                             "I am here to help you stay safe in the digital world.\n\n" +
                             "Please enter your name to get started:");
        }

        // =================================================================
        //  Event Handlers
        // =================================================================

        private void InputBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent 'ding' sound
                SendButton_Click(sender, e);
            }
        }

        private void SendButton_Click(object? sender, EventArgs e)
        {
            string input = _inputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            _inputBox.Clear();

            // ── Awaiting name ────────────────────────────────────────────
            if (_awaitingName)
            {
                CollectName(input);
                return;
            }

            SendMessage(input);
        }

        // =================================================================
        //  Name Collection (Part 1 feature translated to GUI – Req 1)
        // =================================================================

        private void CollectName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                AppendBotMessage("Name cannot be empty. Please enter your name to continue.");
                return;
            }

            // Capitalise first letter
            string name = char.ToUpper(input[0]) + input[1..].ToLower();
            _memory.UserName = name;
            _awaitingName    = false;

            AppendUserMessage(input);
            AppendLine("");
            AppendBotMessage($"Great to meet you, {name}! 🎉\n\n" +
                             $"I'll remember your name throughout our conversation. " +
                             $"Feel free to ask me anything about cybersecurity.\n\n" +
                             $"Type 'help' to see all available topics, or just ask away!");

            UpdateMemoryPanel();
            UpdateStatus($"Chatting as {name}");
        }

        // =================================================================
        //  Message Dispatch
        // =================================================================

        /// <summary>
        /// Sends a message through the chatbot pipeline.
        /// Detects sentiment, gets a response, and updates the UI.
        /// </summary>
        private void SendMessage(string userText)
        {
            // ── Exit detection ───────────────────────────────────────────
            if (_responseEngine.IsExitCommand(userText))
            {
                AppendUserMessage(userText);
                AppendBotMessage($"Goodbye, {_memory.UserName}! 👋\n\n" +
                                 "Stay safe online and remember:\n" +
                                 "  • Use strong, unique passwords\n" +
                                 "  • Enable 2FA on all accounts\n" +
                                 "  • Stay alert for phishing and scams\n\n" +
                                 "Thanks for using the Cybersecurity Awareness Bot!");
                UpdateStatus("Session ended. Press Clear to start again.");
                _sendButton.Enabled = false;
                _inputBox.Enabled   = false;
                return;
            }

            // ── Display user message ─────────────────────────────────────
            AppendUserMessage(userText);

            // ── Sentiment detection (Req 6) ──────────────────────────────
            Sentiment sentiment = SentimentDetector.Detect(userText);
            UpdateSentimentPanel(sentiment);

            // ── Get response (Req 2, 3, 4, 7) ───────────────────────────
            string response = _responseEngine.GetResponse(userText, sentiment);
            AppendBotMessage(response);

            // ── Update memory panel (Req 5) ──────────────────────────────
            UpdateMemoryPanel();
        }

        // =================================================================
        //  Chat Display Helpers
        // =================================================================

        private void AppendUserMessage(string text)
        {
            AppendLine("");
            AppendColoured($"  You ({_memory.UserName}):  ", UserMsgColor, bold: true);
            AppendColoured(text + "\n", TextPrimary);
        }

        private void AppendBotMessage(string text)
        {
            AppendLine("");
            AppendColoured("  🤖 CyberBot:  ", BotMsgColour, bold: true);
            AppendColoured(text + "\n", TextPrimary);
            AppendLine("  ─────────────────────────────────────────────────────────",
                       BorderColour);
            ScrollToEnd();
        }

        private void AppendLine(string text, Color? colour = null, bool bold = false)
        {
            AppendColoured(text + "\n", colour ?? TextPrimary, bold);
        }

        private void AppendColoured(string text, Color colour, bool bold = false)
        {
            _chatDisplay.SelectionStart  = _chatDisplay.TextLength;
            _chatDisplay.SelectionLength = 0;
            _chatDisplay.SelectionColor  = colour;
            _chatDisplay.SelectionFont   = bold
                ? new Font(_chatDisplay.Font, FontStyle.Bold)
                : _chatDisplay.Font;
            _chatDisplay.AppendText(text);
            _chatDisplay.SelectionColor  = TextPrimary;
        }

        private void ScrollToEnd()
        {
            _chatDisplay.SelectionStart = _chatDisplay.TextLength;
            _chatDisplay.ScrollToCaret();
        }

        // =================================================================
        //  Side Panel Updates
        // =================================================================

        /// <summary>Updates the memory panel with current ChatbotMemory state.</summary>
        private void UpdateMemoryPanel()
        {
            _memoryLabel.Text =
                $"Name:  {_memory.UserName}\n" +
                $"Topic: {_memory.FavouriteTopic ?? "Not set yet"}\n" +
                $"Msgs:  {_memory.MessageCount}";
        }

        /// <summary>Updates the sentiment indicator in the side panel.</summary>
        private void UpdateSentimentPanel(Sentiment sentiment)
        {
            var (emoji, label) = SentimentDetector.GetSentimentDisplay(sentiment);
            _sentimentLabel.Text      = $"{emoji}  {label}";
            _sentimentLabel.ForeColor = SentimentDetector.GetSentimentColour(sentiment);
        }

        /// <summary>Updates the status bar text.</summary>
        private void UpdateStatus(string message)
        {
            _statusLabel.Text = $"  🟢 Connected  |  {message}";
        }
    }
}
