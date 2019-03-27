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
        public TranslateRuleRequired() : base()
        {
        }

        public TranslateRuleRequired(string name) : base("Требуется переопределение конструкции "+name+" для целевого языка")
        {

        }
    }
}