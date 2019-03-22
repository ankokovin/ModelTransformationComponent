using System.Linq;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Конкретная фабрика BNF конструкций
    /// <para/>
    /// Наследует <see cref="AbstractRuleFactory"/>
    /// </summary>
    class BNFRuleFactory : AbstractRuleFactory
    {
        /// <summary>
        /// Создание BNF конструкции
        /// </summary>
        /// <param name="text">Текстовое описание конструкции</param>
        /// <param name="charcnt">Количество символов, использованных для создания</param>
        /// <returns>BNF конструкция</returns>
        public override Rule CreateRule(string text, out int charcnt)
        {
            var wsSplit = text.Split();

            if (wsSplit[1] != new Presentation().GetLiteral)
                throw new SyntaxError(wsSplit[1], new Presentation().GetLiteral);


            charcnt = wsSplit[0].Length + wsSplit[1].Length + 2;
            var result = new BNFRule(wsSplit[0]);
            return result;



            throw new System.NotImplementedException();
            throw new System.Exception("Unexpected string input");
        }

        public Rule CreateRule(BNFRule prev, BasicBNFRule rule)
        {
            prev.OrSplits.Add(rule);

            return prev;
        }
    }
}