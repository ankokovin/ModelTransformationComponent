namespace ModelTransformationComponent
{
    /// <summary>
    /// Интерфейс правил, которые могут быть запрещены для использования в выходных правил
    /// </summary>
    public interface IBannableRule
    {
        /// <summary>
        /// Запрещено ли использование данного правила
        /// </summary>
        bool Banned { get; }
    }
}