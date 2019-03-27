namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкиция новая строка
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class New_line : SystemRule, IChangeState   
    {

        /// <summary>
        /// Литерал конструкции новая строка
        /// </summary>
        public override string Literal => "/new_line";

        public void ChangeState(ref GeneratorState generatorState)
        {
            generatorState.AppendText("\r\n");
        }

        public void ChangeState(ref ParserState parserState)
        {
            throw new System.NotImplementedException();
        }
    }
}