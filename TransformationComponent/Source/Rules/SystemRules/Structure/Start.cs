namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция начала описания трансформаций
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Start : SystemRule
    {
        /// <summary>
        /// Литерал конструкции начала описания трансформаций
        /// </summary>
        public override string Literal=> "/start";
    }
}