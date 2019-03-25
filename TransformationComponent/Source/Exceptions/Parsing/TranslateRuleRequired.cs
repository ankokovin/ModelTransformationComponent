using System;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Требуется переопределение конструкции для целевого языка
    /// </summary>
    [Serializable]
    public class TranslateRuleRequired: SyntaxError
    {

        /// <summary>
        /// 
        /// </summary>
        public TranslateRuleRequired() : base("Требуется переопределение конструкции для целевого языка")
        {
        }
    }
}