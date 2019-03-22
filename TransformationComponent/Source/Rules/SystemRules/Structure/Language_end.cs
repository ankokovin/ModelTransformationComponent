namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция окончания описания языка
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Language_end : SystemRule
    {
        /// <summary>
        /// Литерал конструкции окончания описания языка
        /// </summary>
        public override string GetLiteral=> "/language_end";
    }
}