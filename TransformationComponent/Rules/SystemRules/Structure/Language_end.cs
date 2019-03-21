namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция окончания описания языка
    /// </summary>
    class Language_end : SystemRule
    {
        /// <summary>
        /// Литерал конструкции окончания описания языка
        /// </summary>
        public override string GetLiteral=> "/language_end";
    }
}