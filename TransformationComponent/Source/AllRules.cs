using System.Collections.Generic;
namespace ModelTransformationComponent
{
    /// <summary>
    /// Все структуры
    /// </summary>
    public class AllRules{
        /// <summary>
        /// Структуры хранятся следующим образом:
        /// Имя языка : Словарь структур данного языка
        /// Название структуры : Структура
        /// </summary>
        private Dictionary<string, Dictionary<string, Rule>> rulesPerLanguage;

        /// <summary>
        /// Конструктор <see cref="AllRules"/>
        /// </summary>
        public AllRules()
        {
            rulesPerLanguage = new Dictionary<string, Dictionary<string, Rule>>();
        }

        /// <summary>
        /// Название под базовые правила
        /// </summary>
        public const string BaseName = "Base";

        /// <summary>
        /// Добавление структур нового языка
        /// </summary>
        /// <param name="LanguageName">Название языка</param>
        /// <param name="LanguageRules">Словарь структур</param>
        public void AddLanguageRules(string LanguageName, Dictionary<string, Rule> LanguageRules){
            rulesPerLanguage[LanguageName] = LanguageRules;
        }

        /// <summary>
        /// Добавление базовых структур
        /// </summary>
        /// <param name="Rules">Словарь базовых структур</param>
        public void AddBaseRules(Dictionary<string, Rule> Rules) => AddLanguageRules(BaseName, Rules);

        /// <summary>
        /// Проверка наличия описания языка
        /// </summary>
        /// <param name="name">Название языка</param>
        /// <returns>Есть ли описание данного языка</returns>
        public bool HasLanguage(string name) => rulesPerLanguage.ContainsKey(name);

        /// <summary>
        /// Получение структур для конкретного языка
        /// </summary>
        /// <param name="LanguageName">Название языка</param>
        /// <returns>Словарь структур для данного языка</returns>
        public Dictionary<string, Rule> GetRulesForLanguage(string LanguageName){
            if(rulesPerLanguage.ContainsKey(LanguageName))
                return rulesPerLanguage[LanguageName];
            else
                throw new System.ArgumentException("No rules for language "+LanguageName+" found.");
        }
        
        /// <summary>
        /// Получение базовых структур
        /// </summary>
        /// <returns>Словарь базовых структур</returns>
        public Dictionary<string, Rule> GetBaseRules => GetRulesForLanguage(BaseName);

        /// <summary>
        /// Получение списка введённых языков
        /// </summary>
        public List<string> GetLanguages
        {
            get
            {
                var result = new List<string>(rulesPerLanguage.Keys);
                result.Remove(BaseName);
                return result;
            }
        }
    }
}
