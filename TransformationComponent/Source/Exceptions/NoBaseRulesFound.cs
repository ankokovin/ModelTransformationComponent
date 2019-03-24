namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение: не было найдено описание трансформаций для языка
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    public class NoBaseRulesFound : TransformComponentException
    {

        /// <summary>
        /// Конструктор <see cref="NoLanguageRulesFound"/>
        /// </summary>
        public NoBaseRulesFound()
        : base("Не было найдено базовое описание трансформаций") { }

        /// <summary>
        /// Конструктор <see cref="NoLanguageRulesFound"/>
        /// </summary>
        /// <param name="inner"></param>
        public NoBaseRulesFound(System.Exception inner) : base("Не было найдено базовое описание трансформаций", inner) { }
    }
}
