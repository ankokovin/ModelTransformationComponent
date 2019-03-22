using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение, возникающее когда входная строка пуста
    /// </summary>
    [Serializable]
    public class InputIsEmpty : TransformComponentException
    {
        public InputIsEmpty(): base("Входная строка пуста") { }
        public InputIsEmpty(string message) : base(message) { }
        public InputIsEmpty(string message, Exception inner) : base(message, inner) { }
        protected InputIsEmpty(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
