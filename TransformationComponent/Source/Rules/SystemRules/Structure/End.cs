namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная консрукция окончания описания трансляции
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class End : SystemRule
    {
        /// <summary>
        /// Литерал конструкции окончания описания трансляции
        /// </summary>
        public override string Literal=> "/end";
    }
}