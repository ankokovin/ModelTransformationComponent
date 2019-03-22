using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга правил трансформаций в <see cref="TransformationComponent.TransformToRules(string)"/>
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    [Serializable]
    public class RuleParseException : TransformComponentException
    {
        /// <summary>
        /// Конструктор <see cref="RuleParseException"/>
        /// </summary>
        public RuleParseException() { }

        /// <summary>
        /// Конструктор <see cref="RuleParseException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public RuleParseException(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="RuleParseException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public RuleParseException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Конструктор <see cref="RuleParseException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected RuleParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
