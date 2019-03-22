namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция создания конструкции, реализующий тип
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    
    [System.Serializable]
    class TypeEx : SystemRule{
        /// <summary>
        /// Литерал конструкции создания конструкции, реализующий тип
        /// </summary>
        public override string GetLiteral => "/type=";
    }
}