using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGpt_Chat_Bot
{
    public class ChatBotClient
    {
        private readonly string _apiKey;
        private readonly string _botInstructions;
        List<Message> _botRole;//this will act as a system role
        public ChatBotClient(string apiKey)
        {
            _apiKey = apiKey;
            //_botInstructions = @"You are a professional AI customer support and sales assistant. 
            //                    Your role is to assist users with inquiries, provide product recommendations, answer FAQs, 
            //                    and enhance customer satisfaction. You should maintain a friendly, professional, and helpful
            //                    tone. When assisting with sales, suggest relevant products based on user needs, highlight key
            //                    features and benefits, and offer solutions to common concerns. If the user has a complaint, 
            //                    handle it politely and offer appropriate resolutions. Keep responses clear, engaging, 
            //                    and customer-focused.";

            _botInstructions = @"You are an AI-powered educational tutor. Your goal is to teach concepts clearly, adapt to the user’s
            learning style, and provide engaging explanations with real-world examples. Use step-by-step guidance, interactive questions, and
            simple analogies to make learning easier. If a user struggles with a concept, break it down further and offer alternative explanations.
            Encourage curiosity and provide additional learning resources when necessary. Keep your tone friendly, patient, and motivating to create
            a positive learning experience.";

            _botRole = new List<Message> { new Message {
            role="system",
            content=_botInstructions
            } };
        }

        public async Task<string> TalkToChatBotUsingChatGpt(string prompt)
        {
            var client = new RestClient("https://api.openai.com/v1/chat");
            var request = new RestRequest("completions", Method.Post);
            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            request.AddHeader("Content-Type", "application/json");

            //now adding user prompt which will be asked at runtime.
            var requestBody = new ChatGptModel
            {
                messages = new List<Message> {
                new Message {
                    role = "user",
                    content = prompt
                }
            },
                model = "gpt-4o",
                max_tokens = 150
            };
            requestBody.messages.AddRange(_botRole);//based on the system role instructions it will suggest/give the answers.

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                dynamic completion = JsonConvert.DeserializeObject(response.Content);
                var responseString = (string)completion.choices[0].message.content;
                return responseString;
            }
            else
            {
                throw new Exception("Unable to get response from open AI.");
            }
        }
    }
}
