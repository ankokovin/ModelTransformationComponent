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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generatorState"></param>
        public void ChangeState(ref GeneratorState generatorState)
        {
            //DO NOTHING
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parserState"></param>
        public void ChangeState(ref ParserState parserState)
        {
            //DO NOTHING
        }
    }
}