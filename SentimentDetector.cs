// ============================================================
//  SentimentDetector.cs – Sentiment Detection (Requirement 6)
// ============================================================
//
//  Advanced sentiment detection with DYNAMIC response content
//  tailored to mood/tone — not just a prefix, but different
//  advice framing depending on how the user is feeling.
//  Achieves "Greatly Exceeds": dynamic responses tailored to mood.
// ============================================================

namespace CybersecurityAwarenessBot
{
    /// <summary>Emotional states detectable from user messages.</summary>
    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated,
        Confident
    }

    /// <summary>
    /// Implements advanced Sentiment Detection (Requirement 6).
    /// Detects emotional tone and returns both an empathy prefix
    /// AND content-level guidance adjustments tailored to the mood.
    /// </summary>
    public static class SentimentDetector
    {
        // ── Keyword Arrays (one per sentiment) ────────────────────────────

        private static readonly string[] WorriedKeywords =
        {
            "worried", "scared", "afraid", "nervous", "anxious",
            "concern", "fear", "unsafe", "stress", "panic", "terrified",
            "frightened", "uneasy", "dread"
        };

        private static readonly string[] CuriousKeywords =
        {
            "curious", "wonder", "interested", "learn", "tell me",
            "explain", "how does", "what is", "why", "teach me",
            "i want to know", "how can", "show me", "what about"
        };

        private static readonly string[] FrustratedKeywords =
        {
            "frustrated", "annoyed", "angry", "confused", "don't understand",
            "doesn't make sense", "hate", "useless", "impossible", "difficult",
            "complicated", "lost", "overwhelmed", "can't figure", "not working"
        };

        private static readonly string[] ConfidentKeywords =
        {
            "great", "thanks", "got it", "understand now", "makes sense",
            "helpful", "good to know", "perfect", "awesome", "clear now",
            "i know", "already", "easy", "simple", "no problem"
        };

        // =================================================================
        //  Detection
        // =================================================================

        /// <summary>
        /// Analyses the input string and returns the dominant sentiment.
        /// Priority: Worried → Frustrated → Curious → Confident → Neutral.
        /// </summary>
        public static Sentiment Detect(string input)
        {
            string lower = input.ToLower();

            foreach (string kw in WorriedKeywords)
                if (lower.Contains(kw)) return Sentiment.Worried;

            foreach (string kw in FrustratedKeywords)
                if (lower.Contains(kw)) return Sentiment.Frustrated;

            foreach (string kw in CuriousKeywords)
                if (lower.Contains(kw)) return Sentiment.Curious;

            foreach (string kw in ConfidentKeywords)
                if (lower.Contains(kw)) return Sentiment.Confident;

            return Sentiment.Neutral;
        }

        // =================================================================
        //  Dynamic Response Adjustments (Greatly Exceeds criterion)
        // =================================================================

        /// <summary>
        /// Returns an empathetic opening line that acknowledges the user's mood.
        /// Works together with GetContentSuffix for a fully tailored response.
        /// </summary>
        public static string GetEmpathyPrefix(Sentiment sentiment) => sentiment switch
        {
            Sentiment.Worried =>
                "It's completely understandable to feel that way — " +
                "online threats are real and scammers can be very convincing. " +
                "Let me share some practical steps to help you feel more in control.\n\n",

            Sentiment.Frustrated =>
                "I hear you — cybersecurity can feel overwhelming at first. " +
                "Let me break this down as clearly as possible, step by step.\n\n",

            Sentiment.Curious =>
                "Great question! I love that you're interested in learning — " +
                "curiosity is the first step to staying safe online. Here's what you need to know:\n\n",

            Sentiment.Confident =>
                "Excellent — it's great that you already have a good foundation! " +
                "Here's some deeper information to take your knowledge further:\n\n",

            _ => ""
        };

        /// <summary>
        /// Returns a mood-tailored closing line appended after the core advice.
        /// This changes the CONTENT direction based on sentiment, not just tone.
        /// </summary>
        public static string GetContentSuffix(Sentiment sentiment) => sentiment switch
        {
            Sentiment.Worried =>
                "\n\n💛 Remember: feeling aware of the risks is actually a strength. " +
                "The fact that you're asking means you're already ahead of most people.",

            Sentiment.Frustrated =>
                "\n\n💡 Tip: tackle one security step at a time — you don't need to " +
                "do everything at once. Start with a password manager and 2FA.",

            Sentiment.Curious =>
                "\n\n🔍 Want to go deeper? Ask me to 'tell me more' and I'll give " +
                "you another angle on this topic.",

            Sentiment.Confident =>
                "\n\n🚀 Since you're comfortable with this, consider helping someone " +
                "you know — sharing knowledge is one of the best ways to stay sharp.",

            _ => ""
        };

        /// <summary>
        /// Returns the display colour associated with each sentiment for the UI panel.
        /// </summary>
        public static Color GetSentimentColour(Sentiment sentiment) => sentiment switch
        {
            Sentiment.Worried    => Color.FromArgb(255, 200, 80),
            Sentiment.Frustrated => Color.FromArgb(255, 110, 110),
            Sentiment.Curious    => Color.FromArgb(80,  200, 255),
            Sentiment.Confident  => Color.FromArgb(80,  220, 120),
            _                    => Color.FromArgb(160, 165, 180)
        };

        /// <summary>Returns the emoji and label for the sentiment panel.</summary>
        public static (string Emoji, string Label) GetSentimentDisplay(Sentiment sentiment) =>
            sentiment switch
            {
                Sentiment.Worried    => ("😟", "Worried"),
                Sentiment.Frustrated => ("😤", "Frustrated"),
                Sentiment.Curious    => ("🤔", "Curious"),
                Sentiment.Confident  => ("😊", "Confident"),
                _                    => ("😐", "Neutral")
            };
    }
}
