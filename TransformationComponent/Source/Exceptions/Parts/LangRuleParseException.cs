using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга правил трансформаций конкретного языка в <see cref="TransformationComponent.GetLangDescription(string, Dictionary{string, Rule}, out string)"/>
    /// </summary>
    [Serializable]
    public class LangRuleParseException : TransformComponentException
    {
        public LangRuleParseException() { }
        public LangRuleParseException(string message) : base(message) { }
        public LangRuleParseException(string langName, Exception inner) 
            : base("Ошибка парсинга правил трансформаций конкретного языка. Язык:" + langName, inner) { }
        
        public LangRuleParseException(Exception inner) : base("Ошибка парсинга правил трансформаций конкретного языка") { }

        protected LangRuleParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
