namespace ModelTransformationComponent{
    /// <summary>
    /// Системная консрукция окончания описания трансляции
    /// </summary>
    class End : SystemRule
    {
        /// <summary>
        /// Литерал конструкции окончания описания трансляции
        /// </summary>
        public override string GetLiteral=> "/end";
    }
}