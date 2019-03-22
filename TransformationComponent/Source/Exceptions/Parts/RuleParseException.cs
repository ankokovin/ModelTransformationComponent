using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга правил трансформаций в <see cref="TransformationComponent.TransformToRules(string)"/>
    /// </summary>
    [Serializable]
    public class RuleParseException : TransformComponentException
    {
        public RuleParseException() { }
        public RuleParseException(string message) : base(message) { }
        public RuleParseException(string message, Exception inner) : base(message, inner) { }
        protected RuleParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
