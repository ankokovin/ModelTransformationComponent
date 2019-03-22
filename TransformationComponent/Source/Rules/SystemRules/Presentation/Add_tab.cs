namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция увеличения счётчика табов
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Add_tab : SystemRule
    {

        /// <summary>
        /// Литерал конструкции увеличения счётчика табов
        /// </summary>
        public override string GetLiteral => "add_tab";
    }
}