namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция пустой символ
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Empty : SystemRule{

        /// <summary>
        /// Литерал конструкции пустой символ
        /// </summary>
        public override string Literal => "/empty";
    }
}