namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение: не был найден символ /start
    /// <para/>
    /// Наследует <see cref="SyntaxError"/>
    /// </summary>
    public class NoStartDetected : SyntaxError
    {

        /// <summary>
        /// Конструктор <see cref="NoStartDetected"/>
        /// </summary>
        public NoStartDetected() 
        : base(@"Не был найден символ /start") {}
    }
}
