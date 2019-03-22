using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка парсинга модели в <see cref="TransformationComponent.Transform(string, AllRules, string, string)"/>
    /// </summary>
    [Serializable]
    public class ModelParseException : TransformComponentException
    {
        public ModelParseException() { }
        public ModelParseException(string message) : base(message) { }
        public ModelParseException(string message, Exception inner) : base(message, inner) { }
        protected ModelParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
