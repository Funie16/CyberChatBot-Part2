using System;

class CyberChatBot
{
    // Declare properties for each component
    public CyberChatResponse responseGenerator;
    public ChatbotVoice voiceGreeting;
    public CyberChatLogo logoGenerator;

    public CyberChatBot()
    {
        // Initialize the components in the constructor
        responseGenerator = new CyberChatResponse();
        voiceGreeting = new ChatbotVoice();  // Initialize voice component
        logoGenerator = new CyberChatLogo();  // Initialize logo component
    }

    public void StartChat()
    {
        voiceGreeting.PlayVoiceGreeting(); // Play voice greeting
        logoGenerator.DisplayLogo(); // Display ASCII logo
        Console.ForegroundColor = ConsoleColor.Cyan; // Change text color
        Console.WriteLine("Hello! I am CyberChatBot. Type 'exit' to end the chat.");
        Console.ResetColor();

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine()?.ToLower(); // Get user input

            if (userInput == "exit")
            {
                Console.WriteLine("CyberChatBot: Goodbye!");
                break;
            }

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("CyberChatBot: Please say something!");
                continue;
            }

            string response = responseGenerator.GetResponse(userInput); // Get response
            Console.WriteLine("CyberChatBot: " + response);
        }
    }
}
