using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга основных правил трансформаций в <see cref="TransformationComponent.GetBaseDescription(string)"/>
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    [Serializable]
    public class BaseRuleParseException : TransformComponentException
    {
        /// <summary>
        /// Конструктор <see cref="BaseRuleParseException"/>
        /// </summary>
        public BaseRuleParseException() { }

        /// <summary>
        /// Конструктор <see cref="BaseRuleParseException"/>
        /// </summary>
        /// <param name="message">Сообшение</param>
        public BaseRuleParseException(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="BaseRuleParseException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public BaseRuleParseException(string message, Exception inner) : base(message, inner) { }


        /// <summary>
        /// Конструктор <see cref="BaseRuleParseException"/>
        /// </summary>
        /// <param name="inner">Внутренняя ошибка</param>
        public BaseRuleParseException(Exception inner) : this("Ошибка парсинга основных правил трансформаций", inner) { }

        /// <summary>
        /// Конструктор <see cref="BaseRuleParseException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BaseRuleParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
