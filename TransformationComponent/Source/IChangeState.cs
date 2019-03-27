namespace ModelTransformationComponent
{
    public interface IChangeState
    {
        void ChangeState(ref GeneratorState generatorState);
        void ChangeState(ref ParserState parserState);
    }
}
