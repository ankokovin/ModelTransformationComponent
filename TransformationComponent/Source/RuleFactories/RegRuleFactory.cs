namespace ModelTransformationComponent{
    /// <summary>
    /// Конкретная фабрика структур с представлением в виде регулярных выражений
    /// </summary>
    class RegRuleFactory : AbstractRuleFactory
    {
        /// <summary>
        /// Метод создания структуры с регулярным выражением
        /// </summary>
        /// <param name="text">Текстовое представление структуры</param>
        /// <returns>Структура с представлением в виде регулярного выражения</returns>
        /// <exception cref="System.ArgumentException">Неожиданная строка</exception>
        /// <exception cref="ModelTransformationComponent.SyntaxError">Синтаксическая ошибка<exception>
        public override Rule CreateRule(string text)
        {
            var sp = text.Split();
            if (sp.Length<3)
                throw new System.ArgumentException("Unexpected string input");
            string pattern = sp[2];
            if (sp[1] != new Presentation().GetLiteral)
                throw new SyntaxError(sp[1], new Presentation().GetLiteral);
            for(int i=3;i<sp.Length; ++i){
                pattern += " " + sp[i];
            }
            return new RegexRule(pattern, sp[0]);
        }
    }
}