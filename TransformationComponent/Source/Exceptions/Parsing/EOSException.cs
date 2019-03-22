using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка: неожиданный конец входной строки
    /// </summary>
    [Serializable]
    public class EOSException : SyntaxError
    {
        public EOSException():base("Входная строка неожиданно закончилась") { }
        public EOSException(string message) : base("Входная строка неожиданно закончилась: " + message) { }
        public EOSException(string message, Exception inner) : base(message, inner) { }
        protected EOSException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}