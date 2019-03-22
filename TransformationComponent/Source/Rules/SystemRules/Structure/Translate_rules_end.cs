namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция окончания описания дополнительных правил трансформации
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Translate_rules_end : SystemRule{
        /// <summary>
        /// Литерал конструкции окончания описания дополнительных правил трансформации
        /// </summary>
        public override string GetLiteral => "/translate_rules_end";
    }
}