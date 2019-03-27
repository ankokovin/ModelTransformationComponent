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

        public void ChangeState(ref GeneratorState generatorState)
        {
            generatorState.AppendText(" ");
        }

        public void ChangeState(ref ParserState parserState)
        {
            throw new System.NotImplementedException();
        }
    }
}