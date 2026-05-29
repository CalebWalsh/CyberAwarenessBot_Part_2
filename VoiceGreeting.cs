// ============================================================
//  VoiceGreeting.cs – Voice Greeting (Requirement 1)
// ============================================================
//
//  Plays the welcome voice greeting when the application starts.
//  Priority:
//    1. greeting.wav (same folder as the .exe) — Part 1 compatibility
//    2. Text-to-Speech via System.Speech.Synthesis — automatic fallback
//
//  Audio runs on a background thread so the UI stays responsive.
// ============================================================

using System.Media;
using System.Speech.Synthesis;

namespace CybersecurityAwarenessBot
{
    /// <summary>
    /// Handles the voice greeting played at application startup.
    /// Tries to play greeting.wav first; falls back to TTS automatically.
    /// </summary>
    public static class VoiceGreeting
    {
        // The text spoken when no WAV file is found
        private const string GreetingText =
            "Hello and welcome to the Cybersecurity Awareness Bot. " +
            "I am here to help keep you safe in South Africa's digital landscape. " +
            "Please enter your name to get started.";

        /// <summary>
        /// Plays the greeting asynchronously so the UI loads immediately.
        /// </summary>
        public static void PlayAsync()
        {
            // Fire-and-forget on a background thread
            Task.Run(() =>
            {
                try
                {
                    // ── Option 1: Play greeting.wav if it exists ──────────
                    string exeFolder = AppContext.BaseDirectory;
                    string wavPath   = Path.Combine(exeFolder, "greeting.wav");

                    if (File.Exists(wavPath))
                    {
                        using var player = new SoundPlayer(wavPath);
                        player.PlaySync(); // blocks background thread until done
                        return;
                    }

                    // ── Option 2: Text-to-Speech fallback ─────────────────
                    using var synth = new SpeechSynthesizer();

                    // Pick the best available voice
                    var voices = synth.GetInstalledVoices();
                    var preferred = voices.FirstOrDefault(v =>
                        v.VoiceInfo.Name.Contains("Zira") ||        // US female
                        v.VoiceInfo.Name.Contains("Hazel") ||       // UK female
                        v.VoiceInfo.Name.Contains("David"));        // US male
                    if (preferred != null)
                        synth.SelectVoice(preferred.VoiceInfo.Name);

                    synth.Rate   = -1;   // slightly slower for clarity
                    synth.Volume = 100;
                    synth.Speak(GreetingText);
                }
                catch
                {
                    // Audio failure is non-fatal — silently ignore
                }
            });
        }
    }
}
