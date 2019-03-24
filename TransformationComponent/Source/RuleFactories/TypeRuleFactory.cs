namespace ModelTransformationComponent{
    /// <summary>
    /// Конкретная фабрика type конструкций
    /// <para/>
    /// Наследует <see cref="AbstractRuleFactory"/>
    /// </summary>
    class TypeRuleFactory : AbstractRuleFactory
    {
        /// <summary>
        /// Создание type конструкции
        /// </summary>
        /// <param name="text">Текстовое описание конструкции</param>
        /// <param name="charcnt">Количество символов, использованных для создания</param>
        /// <returns>type конструкция</returns>
        public override Rule CreateRule(string text, out int charcnt)
        {
            throw new System.NotImplementedException();
            throw new System.Exception("Unexpected string input");
        }
    }
}