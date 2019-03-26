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
                public class WithTransformRulesStart
                {
                    [TestMethod]
                    public void SimpleReg()
                    {
                        //arrange
                        var name = "r";
                        var pattern = "a+";
                        var rules = "/start\n" +
                            "/reg " + name + " ::= " + pattern + "\n" +
                            "/translate_rules_start\n" +
                            name + " ::= a\n" +
                            "/translate_rules_end\n" +
                            "/end";

                        var component = new TransformationComponent();

                        //act
                        var result = component.TransformToRules(rules);

                        //assert
                        result.GetBaseRules.ContainsKey(name);
                        result.GetBaseRules.ContainsKey("T+"+name);

                        TestUtil.AssertReg(result.GetBaseRules[name] as RegexRule,name, pattern);
                        TestUtil.AssertBNF(result.GetBaseRules["T+" + name] as BNFRule, "T+" + name, 
                            new BasicBNFRule { new BNFString("a") });
                        
                    }

                    [TestMethod]
                    public void SimpleRBnf()
                    {
                        //arrange
                        var name = "r";
                        var pattern = "a|<r>";
                        var rules = "/start\n" +
                            name + " ::= " + pattern + "\n" +
                            "/translate_rules_start\n" +
                            name + " ::= a\n" +
                            "/translate_rules_end\n" +
                            "/end";

                        var component = new TransformationComponent();

                        //act
                        var result = component.TransformToRules(rules);

                        //assert
                        result.GetBaseRules.ContainsKey(name);
                        result.GetBaseRules.ContainsKey("T+" + name);

                        TestUtil.AssertBNF(result.GetBaseRules[name] as BNFRule, name, 
                            new BasicBNFRule { new BNFString("a") },
                            new BasicBNFRule { new BNFReference ("r") });
                        TestUtil.AssertBNF(result.GetBaseRules["T+" + name] as BNFRule, "T+" + name, 
                            new BasicBNFRule { new BNFString ("a") });

                    }
                }
            }
        }
    }
}
