using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга основных правил трансформаций в <see cref="TransformationComponent.GetBaseDescription(string)"/>
    /// </summary>
    [Serializable]
    public class BaseRuleParseException : TransformComponentException
    {
        public BaseRuleParseException() { }
        public BaseRuleParseException(string message) : base(message) { }
        public BaseRuleParseException(string message, Exception inner) : base(message, inner) { }

        public BaseRuleParseException(Exception inner) : this("Ошибка парсинга основных правил трансформаций", inner) { }
        protected BaseRuleParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
