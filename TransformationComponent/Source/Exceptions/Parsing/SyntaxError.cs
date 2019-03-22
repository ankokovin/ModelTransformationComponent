using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{

    /// <summary>
    /// Синтаксическая ошибка
    /// </summary>
    [Serializable]
    public class SyntaxError : TransformComponentException
    {
        protected SyntaxError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SyntaxError(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SyntaxError()
        {
        }

        public SyntaxError(string message) : base(message)
        {
        }

        public SyntaxError(string expected, string got) : base("Синтаксическая ошибка: ожидалось \""+expected+"\", получили \""+got+"\"")
        {
            Got = got;
            Expected = expected;
        }


        /// <summary>
        /// Полученная информация
        /// </summary>
        public string Got;

        /// <summary>
        /// Ожидаемая информация
        /// </summary>
        public string Expected;
    }
}