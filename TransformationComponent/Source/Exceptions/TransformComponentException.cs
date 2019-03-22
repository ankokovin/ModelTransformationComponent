using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{

    /// <summary>
    /// Класс исключений, генерируемых компонентом трансформации моделей <see cref="TransformationComponent"/>
    /// </summary>
    [Serializable]
    public class TransformComponentException : Exception
    {
        public TransformComponentException() { }
        public TransformComponentException(string message) : base(message) { }
        public TransformComponentException(string message, Exception inner) : base(message, inner) { }
        protected TransformComponentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
