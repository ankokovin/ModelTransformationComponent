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
            
            var wsSplit = text.Split();

            if (wsSplit[1] != new Presentation().Literal)
                throw new SyntaxError(wsSplit[1], new Presentation().Literal);

            
            //charcnt = wsSplit[0].Length + wsSplit[1].Length + 2;
            var result = new BNFRule(wsSplit[0]);
            //return result;

            var declString = wsSplit[2];
            for (int i = 3; i < wsSplit.Length; ++i)
                declString +=" "+wsSplit[i];

            if (declString.Contains("|"))
                throw new SyntaxError("Синтаксическая ошибка. Оператор \"|\" в описании типа");

            var basicBNFFactory = new BasicBNFFactory();
            var basicBNFRule = (BasicBNFRule)basicBNFFactory.CreateRule(declString, out int x);
            
            bool hasChild = false;
            foreach(var item in basicBNFRule.elements){
                if (item is BNFSystemRef sr && sr.rule is Child){
                    hasChild = true;   
                }
            }

            if (!hasChild){
                throw new SyntaxError("Синтаксическая ошибка. Описание типа без символа /child");
            }

            result.OrSplits.Add(basicBNFRule);
            

            charcnt = text.Length;
            return result;
        }
    }
}