namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция начала описания трансформаций
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Start : SystemRule
    {
        /// <summary>
        /// Литерал конструкции начала описания трансформаций
        /// </summary>
        public override string GetLiteral=> "/start";
    }
}