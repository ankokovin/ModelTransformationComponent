namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция начала описания языка
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Language_start : SystemRule
    {
        /// <summary>
        /// Литерал конструкции начала описания языка
        /// </summary>
        public override string GetLiteral=> "/language_start";
    }
}