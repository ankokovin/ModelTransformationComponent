namespace ModelTransformationComponent
{
    /// <summary>
    /// Интрефейс компонента трансформации моделей
    /// </summary>
    public interface ITransformationComponent
    {
        /// <summary>
        /// Функция трансформации моделей
        /// </summary>
        /// <param name="text">Текстовое представление исходной модели</param>
        /// <param name="rules">Текстовое представление правил трансформации</param>
        /// <param name="sourceLang">Название исходного языка</param>
        /// <param name="targetLang">Название целевого языка</param>
        /// <returns>Результат трансформации - новое текстовое представление</returns>
        string Transform(string text, string rules, string sourceLang, string targetLang);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        AllRules TransformToRules(string rules, bool Minimize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="rules"></param>
        /// <param name="sourceLang"></param>
        /// <param name="targetLang"></param>
        /// <returns></returns>
        string Transform(string text, AllRules rules, string sourceLang, string targetLang);
    }
}
