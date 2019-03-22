namespace ModelTransformationComponent
{
    /// <summary>
    /// Конструкция с представлением в виде регулярного выражения
    /// <para/>
    /// Наследует <see cref="NamedRule"/>
    /// </summary>
    
    [System.Serializable]
    class RegexRule : NamedRule{
        /// <summary>
        /// Объект регулярного выражения
        /// </summary>
        private System.Text.RegularExpressions.Regex regex;
        
        /// <summary>
        /// Регулярное выражение
        /// </summary>
        public string Pattern;

        /// <summary>
        /// Конструкция с представлением в виде регулярного выражения
        /// </summary>
        /// <param name="pattern">Регулярное выражение</param>
        /// <param name="name">Название конструкции</param>
        public RegexRule(string pattern, string name) : base(name){
            regex = new System.Text.RegularExpressions.Regex(pattern);
            this.Pattern = pattern;
        }
    }
}