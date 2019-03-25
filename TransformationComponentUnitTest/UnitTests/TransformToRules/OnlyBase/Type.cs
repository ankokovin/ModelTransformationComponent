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
                public class Type
                {
                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesEmptyType()
                    {
                        //arrange
                        var name = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/type " + name + "\n" +
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

                        var resultTypeRule = resultRules[name] as TypeRule;

                        TestUtil.AssertBNF(resultTypeRule, name);

                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeStr()
                    {
                        //arrange
                        var name = "a";
                        var bnf = "a/child";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/type " + name + " ::= " + bnf + "\n" +
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

                        var resultTypeRule = resultRules[name] as TypeRule;

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFString() { Value = "a" },
                            new BNFSystemRef() { rule = new Child() }
                        };

                        TestUtil.AssertBNF(resultTypeRule, name,basicBNFRule);
                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeRef()
                    {
                        //arrange
                        var name = "a";
                        var bnf = "<b>/child";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/type " + name + " ::= " + bnf + "\n" +
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

                        var resultTypeRule = resultRules[name] as TypeRule;

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFReference() { Name = "b" },
                            new BNFSystemRef() { rule = new Child() }
                        };


                        TestUtil.AssertBNF(resultTypeRule, name,basicBNFRule);
                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeStrAndREf()
                    {
                        //arrange
                        var name = "a";
                        var bnf = "<b>a/child";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/type " + name + " ::= " + bnf + "\n" +
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

                        var resultTypeRule = resultRules[name] as TypeRule;

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFReference() { Name = "b" },
                            new BNFString() { Value = "a" },
                            new BNFSystemRef() { rule = new Child() }
                        };


                        TestUtil.AssertBNF(resultTypeRule, name, basicBNFRule );
                    }
                    
                }
            }
        }
    }
}
