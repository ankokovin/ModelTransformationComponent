using System.Linq;
using ModelTransformationComponent.SystemRules;

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
            System.Diagnostics.Debug.WriteLine(text);
            var wsSplit = text.Split().Where(z=>z.Length > 0).ToArray();
            var result = new TypeRule(wsSplit[0]);

            
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

            if (declString.Contains("|"))
                throw new SyntaxError("Синтаксическая ошибка. Оператор \"|\" в описании типа");

            var basicBNFFactory = new BasicBNFFactory();
            var basicBNFRule = (BasicBNFRule)basicBNFFactory.CreateRule(declString, out int x);

            if (!basicBNFRule.Contains(new BNFSystemRef( new Child())))
                throw new SyntaxError("Синтаксическая ошибка. Описание типа без символа /child");
            

            result.Add(basicBNFRule);
            

            charcnt = text.Length;
            return result;
        }
    }
}