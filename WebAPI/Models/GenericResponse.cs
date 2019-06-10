using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class GenericResponse
    {
        public GenericResponse(string text)
        {
            success = new Message(text);
        }
        public Message success { get; set; }
    }

    public class Message
    {
        public Message(string text)
        {
            this.text = text;
        }
        public string text { get; set; }
    }
}
