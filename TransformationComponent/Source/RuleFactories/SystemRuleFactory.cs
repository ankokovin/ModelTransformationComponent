using System.Reflection;
using System.Linq;

namespace ModelTransformationComponent{
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
            SystemRule rule;
            System.Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            System.Type[] SystemRuleTypes = (from System.Type type in types
                                             where type.IsSubclassOf(typeof(SystemRule))
                                             select type)
                                             .ToArray();
            foreach(System.Type t in SystemRuleTypes)
            {
                rule = (SystemRule)System.Activator.CreateInstance(t);
                if (text.StartsWith(rule.GetLiteral))
                {
                    charcnt = rule.GetLiteral.Length;
                    return rule;
                }
            }
            throw new SyntaxError("system rule", text);
        }
    }
}