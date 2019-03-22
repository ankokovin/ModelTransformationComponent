namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция начала описания дополнительных правил трансформации
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    
    [System.Serializable]
    public class Translate_rules_start : SystemRule{
        /// <summary>
        /// Литерал конструкции начала описания дополнительных правил трансформации
        /// </summary>
        public override string GetLiteral => "/translate_rules_start";
    }
}