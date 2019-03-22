using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение: не был найден символ /end
    /// <para/>
    /// Наследует <see cref="EOSException"/>
    /// </summary>
    public class NoEndDetected : EOSException
    {

        /// <summary>
        /// Конструктор <see cref="NoEndDetected"/>
        /// </summary>
        public NoEndDetected()
        : base(@"Не был найден символ /end") { }

        /// <summary>
        /// Конструктор <see cref="NoEndDetected"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public NoEndDetected(string message) : base(message)
        {
        }

        /// <summary>
        /// Конструктор <see cref="NoEndDetected"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public NoEndDetected(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Конструктор <see cref="NoEndDetected"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected NoEndDetected(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
