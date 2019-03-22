namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция создания конструкции, реализующий тип
    /// </summary>
    class TypeEx : SystemRule{
        /// <summary>
        /// Литерал конструкции создания конструкции, реализующий тип
        /// </summary>
        public override string GetLiteral => "/type=";
    }
}