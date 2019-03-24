namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция создания конструкции типа
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
     
    [System.Serializable]
    public class TypeDef : SystemRule{
        /// <summary>
        /// Литерал конструкции создания конструкции типа
        /// </summary>
        public override string Literal => "/type";
    }
}