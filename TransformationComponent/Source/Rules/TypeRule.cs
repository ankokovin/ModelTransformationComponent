using System.Linq;
using ModelTransformationComponent.SystemRules;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Правило типа
    /// </summary>
    [System.Serializable]
    public class TypeRule : BNFRule
    {
        /// <summary>
        /// Список ссылок на экземпляры
        /// </summary>
        public System.Collections.Generic.List<BNFReference> RefList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Название правила</param>
        public TypeRule(string name) : base(name)
        {
            RefList = new System.Collections.Generic.List<BNFReference>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Type " + base.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.WriteLine("Equals in TypeRule");
            System.Diagnostics.Debug.WriteLine("First:" + ToString());
            System.Diagnostics.Debug.WriteLine("Second:" + obj.ToString());
            var r = obj as TypeRule;
            return r != null && this.SequenceEqual(r);
        }
    }
}