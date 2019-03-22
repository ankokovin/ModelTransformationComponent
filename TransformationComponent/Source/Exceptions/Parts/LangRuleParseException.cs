using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга правил трансформаций конкретного языка в <see cref="TransformationComponent.GetLangDescription(string, Dictionary{string, Rule}, out string)"/>
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    [Serializable]
    public class LangRuleParseException : TransformComponentException
    {
        /// <summary>
        /// Конструктор <see cref="LangRuleParseException"/>
        /// </summary>
        public LangRuleParseException() { }

        /// <summary>
        /// Конструктор <see cref="LangRuleParseException"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LangRuleParseException(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="LangRuleParseException"/>
        /// </summary>
        /// <param name="langName">Название языка</param>
        /// <param name="inner">Внутреняя ошибка</param>
        public LangRuleParseException(string langName, Exception inner) 
            : base("Ошибка парсинга правил трансформаций конкретного языка. Язык:" + langName, inner) { }


        /// <summary>
        /// Конструктор <see cref="LangRuleParseException"/>
        /// </summary>
        /// <param name="inner">Внутренняя ошибка</param>
        public LangRuleParseException(Exception inner) : base("Ошибка парсинга правил трансформаций конкретного языка") { }

        /// <summary>
        /// Конструктор <see cref="LangRuleParseException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected LangRuleParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
