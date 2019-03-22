using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка: неожиданный конец входной строки <para/>
    /// Наследует <see cref="SyntaxError"/>
    /// </summary>
    [Serializable]
    public class EOSException : SyntaxError
    {
        /// <summary>
        /// Конструктор <see cref="EOSException"/>
        /// </summary>
        public EOSException():base("Входная строка неожиданно закончилась") { }
        /// <summary>
        /// Конструктор <see cref="EOSException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public EOSException(string message) : base("Входная строка неожиданно закончилась: " + message) { }
        /// <summary>
        /// Конструктор <see cref="EOSException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public EOSException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Конструктор <see cref="EOSException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected EOSException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}