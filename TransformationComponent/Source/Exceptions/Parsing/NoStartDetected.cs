namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение: не был найден символ /start
    /// </summary>
    public class NoStartDetected : SyntaxError
    {

        /// <summary>
        /// Исключение: не было найдено описание трансформаций для языка
        /// </summary>
        public NoStartDetected() 
        : base(@"Не был найден символ /start") {}
    }
}
