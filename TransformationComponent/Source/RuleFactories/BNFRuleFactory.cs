using System.Linq;
using ModelTransformationComponent.SystemRules;

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

            if (wsSplit[1] != new Presentation().Literal)
                throw new SyntaxError(wsSplit[1], new Presentation().Literal);

            
            //charcnt = wsSplit[0].Length + wsSplit[1].Length + 2;
            var result = new BNFRule(wsSplit[0]);
            //return result;

            var declString = wsSplit[2];
            for (int i = 3; i < wsSplit.Length; ++i)
                declString +=" "+wsSplit[i];



            var basicBNFFactory = new BasicBNFFactory();
            var orStrings = declString.Split('|');
            foreach (var orStr in orStrings)
            {
                var basicBNFRule = (BasicBNFRule)basicBNFFactory.CreateRule(orStr, out int x);
                
                foreach(var item in basicBNFRule.elements){
                    if (item is BNFSystemRef sr && sr.rule is Child){
                        throw new SyntaxError("Синтаксическая ошибка. /child в описании не типа");
                    }
                }

                result.OrSplits.Add(basicBNFRule);
            }

            charcnt = text.Length;
            return result;
        }
    }
}