namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция окончания описания дополнительных правил трансформации
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    
    [System.Serializable]
    public class Translate_rules_end : SystemRule{
        /// <summary>
        /// Литерал конструкции окончания описания дополнительных правил трансформации
        /// </summary>
        public override string Literal => "/translate_rules_end";
    }
}