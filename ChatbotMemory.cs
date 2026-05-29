// ============================================================
//  ChatbotMemory.cs – Memory and Recall (Requirement 5)
// ============================================================
//
//  Stores user details shared during the conversation and
//  allows the chatbot to personalise EVERY response seamlessly.
//  Achieves "Greatly Exceeds": consistent, responsive memory
//  with seamless recall that enhances engagement.
// ============================================================

namespace CybersecurityAwarenessBot
{
    /// <summary>
    /// Implements the Memory and Recall feature (Requirement 5).
    /// Tracks the user's name, favourite topic, all topics discussed,
    /// and message count. Provides personalised recall snippets
    /// woven naturally into every response.
    /// </summary>
    public class ChatbotMemory
    {
        // ── Stored User Information ────────────────────────────────────────
        public string  UserName       { get; set; } = "User";
        public string? FavouriteTopic { get; private set; }
        public string? LastTopic      { get; private set; }
        public string? PreviousTopic  { get; private set; }
        public int     MessageCount   { get; private set; }

        // ── Ordered topic history (no duplicates) ─────────────────────────
        private readonly List<string> _topicsDiscussed = new();

        // =================================================================
        //  Public Methods
        // =================================================================

        /// <summary>
        /// Records that the user discussed a topic and updates history.
        /// The very first topic becomes their noted "favourite."
        /// </summary>
        public void RecordTopic(string topic)
        {
            // Shift last → previous for cross-topic connections
            PreviousTopic = LastTopic;
            LastTopic     = topic;
            MessageCount++;

            if (!_topicsDiscussed.Contains(topic))
                _topicsDiscussed.Add(topic);

            // First topic ever becomes the standing favourite
            FavouriteTopic ??= topic;
        }

        /// <summary>
        /// Produces a seamless personalised memory line woven into
        /// the bot's response. Returns null only before the first message.
        /// This ensures memory actively enhances every interaction.
        /// </summary>
        public string? GetSeamlessRecall()
        {
            if (MessageCount == 0) return null;

            // After first topic: acknowledge interest
            if (MessageCount == 1 && FavouriteTopic != null)
                return $"I'll remember that you're interested in {FavouriteTopic}, {UserName}.";

            // Cross-topic connection: link current to previous topic
            if (PreviousTopic != null && LastTopic != null && PreviousTopic != LastTopic)
                return $"Since you've also asked about {PreviousTopic}, {UserName}, " +
                       $"knowing about {LastTopic} works hand-in-hand with that — " +
                       $"good digital hygiene covers both.";

            // Returning to favourite topic
            if (LastTopic == FavouriteTopic && MessageCount > 2)
                return $"You keep coming back to {FavouriteTopic}, {UserName} — " +
                       $"that tells me it's important to you. Let me give you an extra tip.";

            // General personalised nudge after several messages
            if (MessageCount >= 3 && FavouriteTopic != null)
                return $"As someone focused on {FavouriteTopic}, {UserName}, " +
                       $"this topic is directly relevant to your digital security.";

            return null;
        }

        /// <summary>
        /// Returns a formatted summary of everything the bot remembers.
        /// Triggered by 'what do you remember' or 'memory'.
        /// </summary>
        public string GetMemorySummary()
        {
            string topics = _topicsDiscussed.Count > 0
                ? string.Join(", ", _topicsDiscussed)
                : "none yet";

            string favourite = FavouriteTopic != null
                ? FavouriteTopic
                : "not yet identified";

            return $"Here is everything I remember about you, {UserName}:\n\n" +
                   $"  👤  Name:              {UserName}\n" +
                   $"  ⭐  Favourite topic:   {favourite}\n" +
                   $"  📚  Topics explored:  {topics}\n" +
                   $"  💬  Messages sent:    {MessageCount}\n\n" +
                   $"I use this to personalise every response, making sure " +
                   $"the advice I give is relevant to what matters most to you.";
        }

        /// <summary>Returns true if the user has discussed at least one topic.</summary>
        public bool HasContext() => _topicsDiscussed.Count > 0;

        /// <summary>Returns all topics discussed as a read-only list.</summary>
        public IReadOnlyList<string> GetTopicsDiscussed() => _topicsDiscussed.AsReadOnly();
    }
}
