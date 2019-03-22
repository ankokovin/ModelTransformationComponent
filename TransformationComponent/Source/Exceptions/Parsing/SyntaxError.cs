using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{

    /// <summary>
    /// Синтаксическая ошибка
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    [Serializable]
    public class SyntaxError : TransformComponentException
    {
        /// <summary>
        /// Конструктор <see cref="SyntaxError"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SyntaxError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


        /// <summary>
        /// Конструктор <see cref="SyntaxError"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутренняя ошибка</param>
        public SyntaxError(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Конструктор <see cref="SyntaxError"/>
        /// </summary>
        public SyntaxError()
        {
        }


        /// <summary>
        /// Конструктор <see cref="SyntaxError"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public SyntaxError(string message) : base(message)
        {
        }

        /// <summary>
        /// Конструктор <see cref="SyntaxError"/>
        /// </summary>
        /// <param name="expected">Ожидаемая строка</param>
        /// <param name="got">Полученная строка</param>
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