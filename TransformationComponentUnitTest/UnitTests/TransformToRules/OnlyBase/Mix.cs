using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;
using ModelTransformationComponent.SystemRules;


namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        public partial class TransformToRules
        {
            public partial class OnlyBase
            {
                [TestClass]
                public class Mix
                {
                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFRefReg()
                    {
                        //arrange
                        var name = "a";
                        var name2 = "b";
                        var pattern = "[0-9]+";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/reg " + name + " ::= " + pattern + "\n" +
                        name2 + " ::= <" + name + ">\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(2, resultRules.Count);

                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultRegRule = resultRules[name] as RegexRule;

                        TestUtil.AssertReg(resultRegRule, name, pattern);
                        Assert.IsTrue(resultRules.ContainsKey(name2));

                        var resultBNFRule = resultRules[name2] as BNFRule;



                        var expBasicBNF = new BasicBNFRule
                        {
                            new BNFReference { Name = name }
                        };
                        TestUtil.AssertBNF(resultBNFRule, name2,
                            new BasicBNFRule[1] { expBasicBNF });

                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFTwoOrs()
                    {
                        //arrange
                        var name = "a";
                        var str1 = "a";
                        var str2 = "b";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= " + str1 + "|" + str2 + "\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(1, resultRules.Count);

                        Assert.IsTrue(resultRules.ContainsKey(name));


                        var resultBNFRule = resultRules[name] as BNFRule;



                        var expBasicBNF = new BasicBNFRule();
                        expBasicBNF.Add(new BNFString { Value = str1 });

                        var expBasicBNF2 = new BasicBNFRule();
                        expBasicBNF2.Add(new BNFString { Value = str2 });
                        TestUtil.AssertBNF(resultBNFRule, name,
                            new BasicBNFRule[2] { expBasicBNF, expBasicBNF2 });

                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFSelfRef()
                    {
                        //arrange
                        var name = "a";
                        var str = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= " + "/empty |" + str + "<" + name + ">\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(1, resultRules.Count);

                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultBNFRule = resultRules[name] as BNFRule;



                        var expBasicBNF = new BasicBNFRule
                        {
                            new BNFSystemRef { rule = new Empty() }
                        };

                        var expBasicBNF2 = new BasicBNFRule
                        {
                            new BNFString { Value = str },
                            new BNFReference { Name = name }
                        };
                        TestUtil.AssertBNF(resultBNFRule, name,
                            new BasicBNFRule[2] { expBasicBNF, expBasicBNF2 });

                    }
                }
            }
        }
    }
}
