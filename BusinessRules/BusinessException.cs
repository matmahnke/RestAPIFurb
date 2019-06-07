using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRules
{
    [Serializable]
    public class BusinessException : Exception
    {
        private List<ErrorField> errors;

        public BusinessException(List<ErrorField> errors)
        {
            this.errors = errors;
        }

        public List<ErrorField> GetErrors()
        {
            return this.errors;
        }

        public string GetMessages()
        {
            StringBuilder builder = new StringBuilder();
            foreach (ErrorField item in errors)
            {
                builder.AppendLine(item.Message);
            }
            return builder.ToString();
        }

        public BusinessException() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception inner) : base(message, inner) { }
        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
