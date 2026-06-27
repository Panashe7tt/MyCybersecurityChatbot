using System;
using System.IO;
using System.Media;

namespace MyCybersecurityChatbot
{
    public class Audio
    {
        public static void PlayGreeting()
        {
            try
            {
                string fileName = "greetings (1).wav";

                // Always resolve from application folder (bin\Debug\...)
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string audioPath = Path.Combine(basePath, fileName);

                if (!File.Exists(audioPath))
                {
                    Console.WriteLine("Audio file not found: " + audioPath);
                    return;
                }

                using (SoundPlayer player = new SoundPlayer(audioPath))
                {
                    player.Load();
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audio playback error: " + ex.Message);
            }
        }
    }
}