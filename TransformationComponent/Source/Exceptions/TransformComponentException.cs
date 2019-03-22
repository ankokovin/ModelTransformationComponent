using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{

    /// <summary>
    /// Класс исключений, генерируемых компонентом трансформации моделей <see cref="TransformationComponent"/>
    /// <para/>
    /// Наследует <see cref="Exception"/>
    /// </summary>
    [Serializable]
    public class TransformComponentException : Exception
    {
        /// <summary>
        /// Конструктор <see cref="TransformComponentException"/>
        /// </summary>
        public TransformComponentException() { }

        /// <summary>
        /// Конструктор <see cref="TransformComponentException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public TransformComponentException(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="TransformComponentException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public TransformComponentException(string message, Exception inner) : base(message, inner) { }
        
        /// <summary>
        /// Конструктор <see cref="TransformComponentException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TransformComponentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
