namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция окончания описания языка
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Language_end : SystemRule
    {
        /// <summary>
        /// Литерал конструкции окончания описания языка
        /// </summary>
        public override string Literal=> "/language_end";
    }
}