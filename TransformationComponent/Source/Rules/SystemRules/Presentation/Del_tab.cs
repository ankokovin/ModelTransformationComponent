namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция уменьшения счётчика табов
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Del_tab : SystemRule, IChangeState
    {

        /// <summary>
        /// Литерал конструкции уменьшения счётчика табов
        /// </summary>
        public override string Literal=> "/del_tab";

        public void ChangeState(ref GeneratorState generatorState)
        {
            --generatorState.TabCount;
        }

        public void ChangeState(ref ParserState parserState)
        {
            --parserState.TabCount;
        }
    }
}