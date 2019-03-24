using System.Reflection;
using System.Linq;

namespace ModelTransformationComponent
{
    
    public class SystemTrieSingleton
    {
        private static SystemTrieSingleton instance;
        private static Trie<SystemRule> trie;
        private SystemTrieSingleton()
        {
            trie = new Trie<SystemRule>();

            System.Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            System.Type[] SystemRuleTypes = (from System.Type type in types
                                             where type.IsSubclassOf(typeof(SystemRule))
                                             select type)
                                             .ToArray();
            SystemRule rule;
            foreach (System.Type t in SystemRuleTypes)
            {
                rule = (SystemRule)System.Activator.CreateInstance(t);
                trie.Insert(rule,rule.Literal);
            }
        }

        public SystemRule Search(string text, out int charcnt, out string Suggestion)
        {
            return trie.Search(text, out charcnt, out Suggestion);
        }

        public static SystemTrieSingleton getInstance()
        {
            if (instance == null)
                instance = new SystemTrieSingleton();
            return instance;
        }
    }
}