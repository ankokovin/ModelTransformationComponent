using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;

namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        public partial class TransformToRules
        {
            public partial class OnlyBase
            {
                [TestClass]
                public class TypeEx
                {
                    [TestMethod]
                    public void TransformRulesSimpleTypeEx()
                    {
                        //arrange
                        var typename = "a";
                        var typep = "a";
                        var typebnf = typep + "/child";
                        var exname = "b";
                        var exbnf = "b";
                        var rules_txt = "/start\n" +
                            "/type " + typename + " ::= " + typebnf + "\n" +
                            "/type= " + typename + " " + exname + " ::= " + exbnf + '\n'+
                            "/end";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules_txt);

                        //assert
                        Assert.AreEqual(0, actual.Languages.Count);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(2, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, typename);
                        CollectionAssert.Contains(resultRules.Keys, exname);

                        var bnfrule = resultRules[exname] as BNFRule;

                        Assert.IsNotNull(bnfrule);

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.elements.Add(new BNFString() { Value = typep + exbnf });

                        TestUtil.AssertBNF(bnfrule, exname, basicBNFRule );

                        var expected = component.TransformToRules("/start\n" +
                            "b ::= ab\n" +
                            "/end").GetBaseRules["b"] as BNFRule;

                        Assert.IsTrue(expected.Equals(resultRules["b"] as BNFRule));
                    }
                    
                }
            }
        }
    }
}
