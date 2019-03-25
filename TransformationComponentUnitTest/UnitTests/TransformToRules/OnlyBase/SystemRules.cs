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
                public class SystemRules
                {
                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesFakeSys()
                    {
                        var fakeName = "aofjpawf";
                        //arrange
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        fakeName + "\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        try
                        {
                            var actual = component.TransformToRules(rules);

                        }//assert
                        catch (TransformComponentException tr)
                        {
                            Assert.IsInstanceOfType(tr, typeof(RuleParseException));
                            Assert.IsInstanceOfType(tr.InnerException, typeof(BaseRuleParseException));
                            Assert.IsInstanceOfType(tr.InnerException.InnerException, typeof(SyntaxErrorPlaced));
                        }

                    }

                    [DataTestMethod]
                    [DataRow(typeof(Add_tab))]
                    [DataRow(typeof(Del_tab))]
                    [DataRow(typeof(Empty))]
                    [DataRow(typeof(Space))]

                    [TestCategory("TransformToRules")]
                    public void ToRulesSysPresentation(System.Type type)
                    {
                        //arrange
                        var name = "a";
                        var rule = (SystemRule)System.Activator.CreateInstance(type);
                        var str = rule.Literal;
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= " + str + "\n" +
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
                        expBasicBNF.elements.Add(new BNFSystemRef { rule = rule });
                        TestUtil.AssertBNF(resultBNFRule, name,
                            new BasicBNFRule[1] { expBasicBNF });

                    }
                    
                }
            }
        }
    }
}
