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
                public class BNF
                {
                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNF()
                    {
                        //arrange
                        var name = "a";
                        var str = "a";
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
                        expBasicBNF.elements.Add(new BNFString { Value = str });
                        TestUtil.AssertBNF(resultBNFRule, name,expBasicBNF);

                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFEmpty()
                    {
                        //arrange
                        var name = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + "\n" +
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

                        TestUtil.AssertBNF(resultBNFRule, name);

                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTwoBNF()
                    {
                        //arrange
                        var name = "a";
                        var name2 = "b";
                        var str = "a";
                        var str2 = "b";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= " + str + "\n" +
                        name2 + " ::= " + str2 + "\n" +
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

                        var resultBNFRule = resultRules[name] as BNFRule;


                        var expBasicBNF = new BasicBNFRule();
                        expBasicBNF.elements.Add(new BNFString { Value = str });
                        TestUtil.AssertBNF(resultBNFRule, name,
                            new BasicBNFRule[1] { expBasicBNF });

                        Assert.IsTrue(resultRules.ContainsKey(name2));

                        resultBNFRule = resultRules[name2] as BNFRule;


                        expBasicBNF = new BasicBNFRule();
                        expBasicBNF.elements.Add(new BNFString { Value = str2 });
                        TestUtil.AssertBNF(resultBNFRule, name2,expBasicBNF);
                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFFakeSys()
                    {
                        //arrange
                        var name = "a";
                        var fakeName = "adkjlxfkbn";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= /" + fakeName + "\n" +
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

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTwoBNFSameName()
                    {
                        //arrange
                        var name = "a";
                        var name2 = name;
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= a\n" +
                        name2 + " ::= b\n" +
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

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTwoBNFReference()
                    {
                        //arrange
                        var name = "a";
                        var name2 = "b";

                        var str = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= " + str + "\n" +
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

                        var resultBNFRule = resultRules[name] as BNFRule;


                        var expBasicBNF = new BasicBNFRule();
                        expBasicBNF.elements.Add(new BNFString { Value = str });
                        TestUtil.AssertBNF(resultBNFRule, name,expBasicBNF);


                        Assert.IsTrue(resultRules.ContainsKey(name));

                        resultBNFRule = resultRules[name2] as BNFRule;



                        expBasicBNF = new BasicBNFRule();
                        expBasicBNF.elements.Add(new BNFReference { Name = name });
                        TestUtil.AssertBNF(resultBNFRule, name2,expBasicBNF);

                    }
                }
            }
        }
    }
}
