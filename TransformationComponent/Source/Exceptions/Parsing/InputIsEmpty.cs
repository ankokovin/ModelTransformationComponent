using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение, возникающее когда входная строка пуста
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    [Serializable]
    public class InputIsEmpty : TransformComponentException
    {
        /// <summary>
        /// Конструктор <see cref="InputIsEmpty"/>
        /// </summary>
        public InputIsEmpty(): base("Входная строка пуста") { }

        /// <summary>
        /// Конструктор <see cref="InputIsEmpty"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public InputIsEmpty(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="InputIsEmpty"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public InputIsEmpty(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected InputIsEmpty(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
