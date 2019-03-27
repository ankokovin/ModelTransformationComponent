namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция увеличения счётчика табов
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Add_tab : SystemRule, IChangeState
    {

        /// <summary>
        /// Литерал конструкции увеличения счётчика табов
        /// </summary>
        public override string Literal => "/add_tab";

        public void ChangeState(ref GeneratorState generatorState)
        {
            ++generatorState.TabCount;
        }

        public void ChangeState(ref ParserState parserState)
        {
            ++parserState.TabCount;
        }
    }
}