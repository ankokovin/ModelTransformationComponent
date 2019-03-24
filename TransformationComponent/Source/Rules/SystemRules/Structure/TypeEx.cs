namespace ModelTransformationComponent.SystemRules{
    /// <summary>
    /// Системная конструкция создания конструкции, реализующий тип
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    
    [System.Serializable]
    public class TypeEx : SystemRule{
        /// <summary>
        /// Литерал конструкции создания конструкции, реализующий тип
        /// </summary>
        public override string Literal => "/type=";
    }
}