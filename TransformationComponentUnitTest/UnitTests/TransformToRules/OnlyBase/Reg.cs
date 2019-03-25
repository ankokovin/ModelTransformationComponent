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
                public class Reg
                {
                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesOneReg()
                    {
                        //arrange
                        var pattern = "[1-9][0-9]*|0";
                        var name = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/reg " + name + " ::= " + pattern + "\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(resultRules.Count, 1);

                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultRegRule = resultRules[name] as RegexRule;
                        TestUtil.AssertReg(resultRegRule, name, pattern);
                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTwoReg()
                    {
                        //arrange
                        var pattern = "[1-9][0-9]*|0";
                        var name = "a";
                        var pattern2 = @"\w+";
                        var name2 = "b";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/reg " + name + " ::= " + pattern + "\n" +
                        "/reg " + name2 + " ::= " + pattern2 + "\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(resultRules.Count, 2);

                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultRegRule = resultRules[name] as RegexRule;

                        TestUtil.AssertReg(resultRegRule, name, pattern);

                        resultRegRule = resultRules[name2] as RegexRule;
                        TestUtil.AssertReg(resultRegRule, name2, pattern2);
                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTwoRegSameName()
                    {
                        //arrange
                        var pattern = "[1-9][0-9]*|0";
                        var name = "a";
                        var pattern2 = @"\w+";
                        var name2 = name;
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/reg " + name + " ::= " + pattern + "\n" +
                        "/reg " + name2 + " ::= " + pattern2 + "\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        try
                        {
                            var actual = component.TransformToRules(rules);
                        }
                        catch (System.Exception e)
                        {
                            //assert
                            Assert.IsInstanceOfType(e, typeof(RuleParseException));
                            Assert.IsInstanceOfType(e.InnerException, typeof(BaseRuleParseException));
                            Assert.IsInstanceOfType(e.InnerException.InnerException, typeof(SyntaxErrorPlaced));
                            Assert.IsInstanceOfType(e.InnerException.InnerException.InnerException, typeof(ConstructAlreadyDefined));
                        }
                    }
                }
            }
        }
    }
}
