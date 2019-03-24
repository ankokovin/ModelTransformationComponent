namespace ModelTransformationComponent.SystemRules
{
    /// <summary>
    /// Системная конструкция однострочного комментария
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    public class Comment : SystemRule
    {
        
        
        /// <summary>
        /// Литерал конструкции однострочного комментария
        /// </summary>
        public override string Literal=> "//";
        
    }
}