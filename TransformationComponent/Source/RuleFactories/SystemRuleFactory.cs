using System.Reflection;
using System.Linq;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Конкретная фабрика системных конструкций
    /// <para/>
    /// Наследует <see cref="AbstractRuleFactory"/>
    /// </summary>
    class SystemRuleFactory : AbstractRuleFactory
    {
        /// <summary>
        /// Создание системной конструкции
        /// </summary>
        /// <param name="text">Текстовое описание конструкции</param>
        /// <param name="charcnt">Количество символов, использованных для создания</param>
        /// <returns>Системная конструкция</returns>
        public override Rule CreateRule(string text, out int charcnt)
        {
            SystemTrieSingleton systemTrieSingleton = SystemTrieSingleton.getInstance();

            var result = systemTrieSingleton.Search(text, out charcnt, out string Suggestion);
            if (result != null)
                return result;
            else
            {
                if (Suggestion.Length > 0)
                    throw new SyntaxError(Suggestion, text);
                throw new SyntaxError("system rule", text);
            }
        }
    }
}