namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция потомка. Используется в <see cref="TypeDef"/>
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Child : SystemRule{
        
        /// <summary>
        /// Литерал конструкции потомка
        /// </summary>
        public override string Literal => "/child";
    }
}