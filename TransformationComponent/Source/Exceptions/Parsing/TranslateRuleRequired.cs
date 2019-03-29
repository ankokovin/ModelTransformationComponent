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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public TranslateRuleRequired(string name) : base("Требуется переопределение конструкции "+name+" для целевого языка")
        {

        }
    }
}