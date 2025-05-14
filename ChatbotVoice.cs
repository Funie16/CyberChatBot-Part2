using System;
using System.Media;

public class ChatbotVoice
{
    // Method to play a voice greeting
    public void PlayVoiceGreeting()
    {
        try
        {
            SoundPlayer player = new SoundPlayer(@"C:\Users\RC_Student_lab\source\repos\CyberChatbot\CyberChatbot\welcome.wav");
            player.Play();
            Console.WriteLine("(Playing voice greeting...)");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error playing sound: " + ex.Message);
        }
    }
}
