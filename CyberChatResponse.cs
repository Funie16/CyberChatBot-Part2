using System;
using System.Collections.Generic;
using System.Linq;

public class CyberChatResponse
{
    private string userName;
    private bool hasIntroducedName = false;
    private string lastTopic = null;
    private string favoriteTopic = null;

    private readonly Dictionary<string, string> keywordResponses = new Dictionary<string, string>
    {
        { "password", "Make sure to use strong, unique passwords for each account. Avoid using personal details." },
        { "scam", "Scams often pretend to be trusted sources. Always verify before clicking or sharing info." },
        { "privacy", "Adjust your social media and app settings to limit the personal data you share." },
        { "firewall", "A firewall protects your network by filtering incoming and outgoing traffic." },
        { "encryption", "Encryption secures your data so only authorised parties can access it." }
    };

    private readonly Dictionary<string, List<string>> randomResponses = new Dictionary<string, List<string>>
    {
        { "phishing", new List<string>
            {
                "Be cautious of emails asking for personal info. Scammers disguise themselves as trusted sources.",
                "Hover over links to check if they match the actual website before clicking.",
                "Phishing often creates urgency. Think before you act on threatening messages."
            }
        },
        { "malware", new List<string>
            {
                "Keep your antivirus up to date to help detect and block malware.",
                "Avoid downloading attachments from unknown emails or sketchy websites.",
                "Install software only from trusted sources to reduce malware risks."
            }
        }
    };

    private readonly Dictionary<string[], string> sentimentResponses = new Dictionary<string[], string>
    {
        { new[] { "worried", "anxious", "scared", "nervous" },
            "It's completely normal to feel that way. Online threats can be scary, but I'm here to help you stay safe and informed." },
        { new[] { "curious", "interested" },
            "Curiosity is a great mindset for learning about cybersecurity! Let me know what you'd like to learn more about." },
        { new[] { "frustrated", "annoyed", "upset" },
            "I’m sorry you're feeling that way. Cybersecurity can be tricky, but I’ll do my best to guide you." },
        { new[] { "confused", "don’t get it", "not sure" },
            "No worries—cybersecurity can be complex at first. I'm here to break things down for you. Could you tell me what part is confusing?" }
    };

    private readonly Random random = new Random();

    // Utility method: Check if any keyword exists in the input
    private bool ContainsAny(string input, IEnumerable<string> keywords)
    {
        return keywords.Any(keyword => input.Contains(keyword));
    }

    // Main chatbot logic
    public string GetResponse(string input)
    {
        input = input.ToLowerInvariant();

        //  Name Introduction
        if (!hasIntroducedName)
        {
            if (input.Contains("my name is"))
            {
                userName = input.Substring(input.IndexOf("is") + 3).Trim();
                hasIntroducedName = true;
                return $"Nice to meet you, {userName}! How may I assist you?";
            }
            return "What's your name? Please introduce yourself.";
        }

        //  Store Favorite Topic
        if (input.Contains("i'm interested in"))
        {
            favoriteTopic = input.Substring(input.IndexOf("in") + 3).Trim();
            return $"Great! I'll remember that you're interested in {favoriteTopic}. It's an important topic in cybersecurity.";
        }

        //  Personalized Follow-up
        if (!string.IsNullOrEmpty(favoriteTopic) &&
            (input.Contains("what should i know") || input.Contains("tips")))
        {
            return $"As someone interested in {favoriteTopic}, you should definitely stay updated and follow best practices. Would you like some specific tips on {favoriteTopic}?";
        }

        //  Vague Follow-ups (based on last topic)
        if (input.Contains("tell me more") || input.Contains("more info") || input.Contains("i'm confused"))
        {
            if (!string.IsNullOrEmpty(lastTopic))
            {
                if (randomResponses.ContainsKey(lastTopic))
                {
                    var responses = randomResponses[lastTopic];
                    return responses[random.Next(responses.Count)];
                }
                else if (keywordResponses.ContainsKey(lastTopic))
                {
                    return $"Here's more about {lastTopic}: {keywordResponses[lastTopic]}";
                }
            }
            return "Could you clarify what you'd like more information about?";
        }

        //  Keyword with Random Responses (e.g. phishing, malware)
        foreach (var keyword in randomResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                lastTopic = keyword;
                var responses = randomResponses[keyword];
                return responses[random.Next(responses.Count)];
            }
        }

        //  Keyword with Fixed Response
        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                lastTopic = keyword;

                if (!string.IsNullOrEmpty(favoriteTopic) && keyword == favoriteTopic)
                {
                    return $"Since {favoriteTopic} is your area of interest, here's a tip: {keywordResponses[keyword]}";
                }

                return keywordResponses[keyword];
            }
        }

        //  Sentiment Detection
        foreach (var sentimentGroup in sentimentResponses)
        {
            if (ContainsAny(input, sentimentGroup.Key))
            {
                return sentimentGroup.Value;
            }
        }

        //  Greetings and Bot Responses
        if (input.Contains("hello"))
            return $"Hello {userName}! How can I assist you today?";
        if (input.Contains("how are you"))
            return "I'm just a bot, but I'm always ready to help!";
        if (input.Contains("your name"))
            return "I'm CyberChatBot, your friendly AI assistant!";
        if (input.Contains("bye"))
            return "Goodbye! Stay safe online!";

        // 9. Default fallback
        return "I'm not sure I understand. Could you rephrase that?";
    }
}

