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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generatorState"></param>
        public void ChangeState(ref GeneratorState generatorState)
        {
            generatorState.AppendText("\r\n");
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