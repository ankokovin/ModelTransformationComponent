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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generatorState"></param>
        public void ChangeState(ref GeneratorState generatorState)
        {
            ++generatorState.TabCount;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parserState"></param>
        public void ChangeState(ref ParserState parserState)
        {
            ++parserState.TabCount;
        }
    }
}