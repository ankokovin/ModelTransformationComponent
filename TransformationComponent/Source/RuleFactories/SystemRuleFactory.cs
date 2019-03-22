namespace ModelTransformationComponent{
    /// <summary>
    /// Конкретная фабрика системных конструкций
    /// <para/>
    /// Наследует <see cref="AbstractRuleFactory"/>
    /// </summary>
    class SystemRuleFactory : AbstractRuleFactory
    {
        /// <summary>
        /// Создание системной конструкции
        /// </summary>
        /// <param name="text">Текстовое описание конструкции</param>
        /// <returns>Системная конструкция</returns>
        public override Rule CreateRule(string text)
        {

            switch (text){
                case "/add_tab":
                    return new Add_tab();
                case "/del_tab":
                    return new Del_tab();
                case "/empty":
                    return new Empty();
                case "/space":
                    return new Space();
            }
            throw new System.Exception("Unexpected string input");
        }
    }
}