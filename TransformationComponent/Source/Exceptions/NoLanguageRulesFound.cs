using System;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Исключение: не было найдено описание трансформаций для языка
    /// <para/>
    /// Наследует <see cref="TransformComponentException"/>
    /// </summary>
    public class NoLanguageRulesFound : TransformComponentException
    {

        /// <summary>
        /// Конструктор <see cref="NoLanguageRulesFound"/>
        /// </summary>
        /// <param name="LanguageName">Название языка</param>
        public NoLanguageRulesFound(string LanguageName) 
        : base("Не было найдено описание трансформаций для языка "+LanguageName){}
    }
}
