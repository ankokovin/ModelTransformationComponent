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
            var wsSplit = text.Split().Where(z => z.Length > 0).ToArray(); ;
            var result = new BNFRule(wsSplit[0]);

            if (wsSplit.Length == 1){
                charcnt = text.Length;
                return result;
            }

            if (wsSplit[1] != new Presentation().Literal)
                throw new SyntaxError(new Presentation().Literal, wsSplit[1]);

            
            //charcnt = wsSplit[0].Length + wsSplit[1].Length + 2;
            //return result;

            var declString = wsSplit[2];
            for (int i = 3; i < wsSplit.Length; ++i)
                declString +=" "+wsSplit[i];



            var basicBNFFactory = new BasicBNFFactory();
            var orStrings = declString.Split('|');
            foreach (var orStr in orStrings)
            {
                var basicBNFRule = (BasicBNFRule)basicBNFFactory.CreateRule(orStr, out int x);


                if (basicBNFRule.Contains(new BNFSystemRef(new Child())))
                        throw new SyntaxError("Синтаксическая ошибка. /child в описании не типа");
                
                result.Add(basicBNFRule);
            }

            charcnt = text.Length;
            return result;
        }
    }
}