namespace ModelTransformationComponent{

    /// <summary>
    /// Абстрактная фабрика конструкций
    /// </summary>
    abstract class AbstractRuleFactory{

        /// <summary>
        /// Создание конструкции
        /// </summary>
        /// <param name="text">Текстовое описание конструкции</param>
        /// <param name="charcnt">Количество символов, использованных для создания</param>
        /// <returns>Конструкция</returns>
        public abstract Rule CreateRule(string text, out int charcnt);
    }
}