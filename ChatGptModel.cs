using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGpt_Chat_Bot
{
    public class ChatGptModel
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
        public int max_tokens { get; set; }
    }
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
