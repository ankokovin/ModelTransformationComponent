namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция увеличения счётчика табов
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Add_tab : SystemRule
    {

        /// <summary>
        /// Литерал конструкции увеличения счётчика табов
        /// </summary>
        public override string Literal => "/add_tab";
    }
}