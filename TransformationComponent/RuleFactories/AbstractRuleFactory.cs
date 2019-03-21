namespace ModelTransformationComponent{

    /// <summary>
    /// Абстрактная фабрика конструкций
    /// </summary>
    abstract class AbstractRuleFactory{

        /// <summary>
        /// Создание конструкции
        /// </summary>
        /// <param name="text">Текстовое описание конструкции</param>
        /// <returns>Конструкция</returns>
        public abstract Rule CreateRule(string text);
    }
}