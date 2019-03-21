using System;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение: не было найдено описание трансформаций для языка
    /// </summary>
    public class NoLanguageRulesFound : Exception{

        /// <summary>
        /// Исключение: не было найдено описание трансформаций для языка
        /// </summary>
        /// <param name="LanguageName">Название языка</param>
        public NoLanguageRulesFound(string LanguageName) 
        : base("Не было найдено описание трансформаций для языка "+LanguageName){}
    }
}
