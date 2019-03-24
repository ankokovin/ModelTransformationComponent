namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкиция пробел
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Space : SystemRule{

        /// <summary>
        /// Литерал конструкции пробел
        /// </summary>
        public override string Literal => "/space";
    }
}