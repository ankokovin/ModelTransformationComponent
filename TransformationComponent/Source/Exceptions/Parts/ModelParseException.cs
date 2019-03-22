using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга модели в <see cref="TransformationComponent.Transform(string, AllRules, string, string)"/>
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    [Serializable]
    public class ModelParseException : TransformComponentException
    {
        /// <summary>
        /// Конструктор <see cref="ModelParseException"/>
        /// </summary>
        public ModelParseException() { }

        /// <summary>
        /// Конструктор <see cref="ModelParseException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public ModelParseException(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="ModelParseException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public ModelParseException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Конструктор <see cref="ModelParseException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ModelParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
