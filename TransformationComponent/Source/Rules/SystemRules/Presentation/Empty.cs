namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция пустой символ
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Empty : SystemRule, IChangeState
    {

        /// <summary>
        /// Литерал конструкции пустой символ
        /// </summary>
        public override string Literal => "/empty";

        public void ChangeState(ref GeneratorState generatorState)
        {
            //DO NOTHING
        }

        public void ChangeState(ref ParserState parserState)
        {
            //DO NOTHING
        }
    }
}