namespace ModelTransformationComponent
{
    /// <summary>
    /// Интерфейс для правил, которые могут изменять текущее положение генератора и парсера
    /// </summary>
    public interface IChangeState
    {
        /// <summary>
        /// Изменить положение генератора
        /// </summary>
        /// <param name="generatorState"></param>
        void ChangeState(ref GeneratorState generatorState);
        
        /// <summary>
        /// Изменить положение парсера
        /// </summary>
        /// <param name="parserState"></param>
        void ChangeState(ref ParserState parserState);
    }
}
