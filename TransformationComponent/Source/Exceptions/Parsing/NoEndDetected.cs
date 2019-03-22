using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение: не был найден символ /end
    /// </summary>
    public class NoEndDetected : EOSException
    {

        /// <summary>
        /// Исключение: не было найдено описание трансформаций для языка
        /// </summary>
        public NoEndDetected()
        : base(@"Не был найден символ /end") { }

        public NoEndDetected(string message) : base(message)
        {
        }

        public NoEndDetected(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoEndDetected(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
