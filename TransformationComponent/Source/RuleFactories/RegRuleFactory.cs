using System.Linq;

namespace ModelTransformationComponent{
    /// <summary>
    /// Конкретная фабрика структур с представлением в виде регулярных выражений
    /// <para/>
    /// Наследует <see cref="AbstractRuleFactory"/>
    /// </summary>
    class RegRuleFactory : AbstractRuleFactory
    {
        /// <summary>
        /// Метод создания структуры с регулярным выражением
        /// </summary>
        /// <param name="text">Текстовое представление структуры</param>      
        /// <param name="charcnt">Количество символов, использованных для создания</param>  
        /// <exception cref="System.ArgumentException">Неожиданная строка</exception>
        /// <exception cref="SyntaxError">Синтаксическая ошибка</exception>
        /// <returns>Структура с представлением в виде регулярного выражения</returns>
        public override Rule CreateRule(string text, out int charcnt)
        {
            var sp = text.Split().Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            
            if (sp.Length<3)
                throw new System.ArgumentException("Unexpected string input");
            string pattern = sp[2];
            if (sp[1] != new Presentation().GetLiteral)
                throw new SyntaxError(sp[1], new Presentation().GetLiteral);
            for(int i=3;i<sp.Length; ++i){
                pattern += " " + sp[i];
            }
            charcnt = text.Length;
            return new RegexRule(pattern, sp[0]);
        }
    }
}