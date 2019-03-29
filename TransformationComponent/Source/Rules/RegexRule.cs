namespace ModelTransformationComponent
{
    /// <summary>
    /// Конструкция с представлением в виде регулярного выражения
    /// <para/>
    /// Наследует <see cref="NamedRule"/>
    /// </summary>
    
    [System.Serializable]
    public class RegexRule : NamedRule, IBannableRule{
        /// <summary>
        /// Объект регулярного выражения
        /// </summary>
        public System.Text.RegularExpressions.Regex regex;
        
        /// <summary>
        /// Регулярное выражение
        /// </summary>
        public string Pattern;

        private static System.Collections.Generic.HashSet<char> bannedChars 
            = new System.Collections.Generic.HashSet<char>
                                {
                                    '?', '(', '{', '+', '*', '.', '|', '['
                                };

        /// <summary>
        /// Проверка, является ли данный паттерн запрещённым для правил вывода
        /// </summary>
        /// <value></value>
        public bool Banned {
            get
            {
                for (int i = 0; i < Pattern.Length; i++)
                {
                    if (bannedChars.Contains(Pattern[i]) && (i == 0 || Pattern[i] != '/'))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Конструкция с представлением в виде регулярного выражения
        /// </summary>
        /// <param name="pattern">Регулярное выражение</param>
        /// <param name="name">Название конструкции</param>
        public RegexRule(string pattern, string name) : base(name){
            regex = new System.Text.RegularExpressions.Regex("^" + pattern);
            this.Pattern = pattern;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RegRule "+Name+" = "+Pattern;
        }
    }
}