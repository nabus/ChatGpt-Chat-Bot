using ChatGpt_Chat_Bot;

var aiService = new ChatBotClient("OPEN-AI-API-KEY");//Your api key goes here.

chat:
Console.WriteLine("AI-powered educational tutor:");
string prompt = Console.ReadLine();//your input

string response = await aiService.TalkToChatBotUsingChatGpt(prompt);// prompt will be give by a user.
Console.WriteLine("Chat bot says: ");//response from chat gpt
Console.WriteLine(response);

Console.WriteLine("\nAsk again?\n");
var answer = Console.ReadLine();
if (answer != null && answer.ToLower() == "y")
{
    goto chat;
}
Console.ReadLine();