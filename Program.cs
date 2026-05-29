// ============================================================
//  Program.cs – Application Entry Point
// ============================================================

using System;
using System.Windows.Forms;

namespace CybersecurityAwarenessBot
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
