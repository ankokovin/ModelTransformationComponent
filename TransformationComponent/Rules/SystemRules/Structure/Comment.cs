namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция однострочного комментария
    /// </summary>
    class Comment : SystemRule
    {
        
        
        /// <summary>
        /// Литерал конструкции однострочного комментария
        /// </summary>
        public override string GetLiteral=> "//";
    }
}