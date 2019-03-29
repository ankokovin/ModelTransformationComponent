namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкиция пробел
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Space : SystemRule, IChangeState{

        /// <summary>
        /// Литерал конструкции пробел
        /// </summary>
        public override string Literal => "/space";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generatorState"></param>
        public void ChangeState(ref GeneratorState generatorState)
        {
            generatorState.AppendText(" ");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parserState"></param>
        public void ChangeState(ref ParserState parserState)
        {
            throw new System.NotImplementedException();
        }
    }
}