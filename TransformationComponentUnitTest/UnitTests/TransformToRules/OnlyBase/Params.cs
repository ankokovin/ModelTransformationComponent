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
                public class Params
                {
                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeEmptyParamsEmpty()
                    {
                        //arrange
                        var name = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/type " + name + "\n" +
                        "/params_start\n" +
                        "/params_end\n" +
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

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[0]);
                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeParamsEmpty()
                    {
                        //arrange
                        var name = "a";
                        var bnf = "a/child";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/type " + name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        "/params_end\n" +
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

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFString("a" ));
                        basicBNFRule.Add(new BNFSystemRef(new Child() ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });


                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeOneParamNoBody()
                    {
                        //arrange
                        var name = "a";
                        var param_name = "b";
                        var bnf = "<" + param_name + ">/child";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        "/type " + name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        param_name + "\n" +
                        "/params_end\n" +
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

                        var resultTypeRule = resultRules[name] as TypeRule;

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference("a.b" ));
                        basicBNFRule.Add(new BNFSystemRef(new Child() ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });

                        var full_par_n = name + "." + param_name;
                        var resultBNFRule = resultRules[full_par_n] as BNFRule;

                        TestUtil.AssertBNF(resultBNFRule, full_par_n,
                          new BasicBNFRule[0]);

                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeOneParam()
                    {
                        //arrange
                        var n1 = "c";
                        var name = "a";
                        var param_name = "b";
                        var param_body = "<" + n1 + ">";
                        var bnf = "<" + param_name + ">/child";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        n1 + "\n" +
                        "/type " + name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        param_name + " ::= " + param_body + "\n" +
                        "/params_end\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(3, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, n1);

                        TestUtil.AssertBNF((BNFRule)resultRules[n1], n1, new BasicBNFRule[0]);



                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultTypeRule = resultRules[name] as TypeRule;

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference("a.b" ));
                        basicBNFRule.Add(new BNFSystemRef(new Child() ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });

                        var full_par_n = name + "." + param_name;
                        var resultBNFRule = resultRules[full_par_n] as BNFRule;

                        basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference(n1 ));

                        TestUtil.AssertBNF(resultBNFRule, full_par_n,
                          new BasicBNFRule[] { basicBNFRule });

                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesTypeTwoParams()
                    {
                        //arrange
                        var n1 = "c";
                        var n2 = "d";
                        var name = "a";
                        var param_name = "b";
                        var param_name_2 = "b2";
                        var param_body = "<" + n1 + ">";
                        var param_body_2 = "<" + n2 + ">";
                        var bnf = "<" + param_name + ">/child";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        n1 + "\n" +
                        n2 + "\n" +
                        "/type " + name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        param_name + " ::= " + param_body + "\n" +
                        param_name_2 + " ::= " + param_body_2 + "\n" +
                        "/params_end\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(5, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, n1);

                        TestUtil.AssertBNF((BNFRule)resultRules[n1], n1, new BasicBNFRule[0]);

                        CollectionAssert.Contains(resultRules.Keys, n2);

                        TestUtil.AssertBNF((BNFRule)resultRules[n2], n2, new BasicBNFRule[0]);

                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultTypeRule = resultRules[name] as TypeRule;

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference("a.b" ));
                        basicBNFRule.Add(new BNFSystemRef(new Child() ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });

                        var full_par_n = name + "." + param_name;
                        var resultBNFRule = resultRules[full_par_n] as BNFRule;

                        basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference(n1 ));

                        TestUtil.AssertBNF(resultBNFRule, full_par_n,
                          new BasicBNFRule[] { basicBNFRule });


                        var full_par_n_2 = name + "." + param_name_2;
                        resultBNFRule = resultRules[full_par_n_2] as BNFRule;

                        basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference(n2 ));

                        TestUtil.AssertBNF(resultBNFRule, full_par_n_2,
                          new BasicBNFRule[] { basicBNFRule });

                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFEmptyParamsEmpty()
                    {
                        //arrange
                        var name = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + "\n" +
                        "/params_start\n" +
                        "/params_end\n" +
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

                        var resultTypeRule = resultRules[name] as BNFRule;

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[0]);
                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFParamsEmpty()
                    {
                        //arrange
                        var name = "a";
                        var bnf = "a";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        "/params_end\n" +
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

                        var resultTypeRule = resultRules[name] as BNFRule;

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFString("a" ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });


                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFOneParamNoBody()
                    {
                        //arrange
                        var name = "a";
                        var param_name = "b";
                        var bnf = "<" + param_name + ">";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        param_name + "\n" +
                        "/params_end\n" +
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

                        var resultTypeRule = resultRules[name] as BNFRule;

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference("a.b" ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });

                        var full_par_n = name + "." + param_name;
                        var resultBNFRule = resultRules[full_par_n] as BNFRule;

                        TestUtil.AssertBNF(resultBNFRule, full_par_n,
                          new BasicBNFRule[0]);

                    }


                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFOneParam()
                    {
                        //arrange
                        var n1 = "c";
                        var name = "a";
                        var param_name = "b";
                        var param_body = "<" + n1 + ">";
                        var bnf = "<" + param_name + ">";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        n1 + "\n" +
                        name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        param_name + " ::= " + param_body + "\n" +
                        "/params_end\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(3, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, n1);

                        TestUtil.AssertBNF((BNFRule)resultRules[n1], n1, new BasicBNFRule[0]);



                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultTypeRule = resultRules[name] as BNFRule;

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference("a.b" ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });

                        var full_par_n = name + "." + param_name;
                        var resultBNFRule = resultRules[full_par_n] as BNFRule;

                        basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference(n1));

                        TestUtil.AssertBNF(resultBNFRule, full_par_n,
                          new BasicBNFRule[] { basicBNFRule });

                    }

                    [TestMethod]
                    [TestCategory("TransformToRules")]
                    public void ToRulesBNFTwoParams()
                    {
                        //arrange
                        var n1 = "c";
                        var n2 = "d";
                        var name = "a";
                        var param_name = "b";
                        var param_name_2 = "b2";
                        var param_body = "<" + n1 + ">";
                        var param_body_2 = "<" + n2 + ">";
                        var bnf = "<" + param_name + ">";
                        var rules = "Some strange comment-like text\n" +
                        "with some new lines and //////star\n" +
                        "but eventually /start\n" +
                        n1 + "\n" +
                        n2 + "\n" +
                        name + " ::= " + bnf + "\n" +
                        "/params_start\n" +
                        param_name + " ::= " + param_body + "\n" +
                        param_name_2 + " ::= " + param_body_2 + "\n" +
                        "/params_end\n" +
                        "/end\n" +
                        "Some more comments";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules);

                        //assert
                        Assert.AreEqual(actual.Languages.Count, 0);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(5, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, n1);

                        TestUtil.AssertBNF((BNFRule)resultRules[n1], n1, new BasicBNFRule[0]);

                        CollectionAssert.Contains(resultRules.Keys, n2);

                        TestUtil.AssertBNF((BNFRule)resultRules[n2], n2, new BasicBNFRule[0]);

                        Assert.IsTrue(resultRules.ContainsKey(name));

                        var resultTypeRule = resultRules[name] as BNFRule;

                        var basicBNFRule = new BasicBNFRule();
                        basicBNFRule.Add(new BNFReference( "a.b" ));

                        TestUtil.AssertBNF(resultTypeRule, name,
                            new BasicBNFRule[] { basicBNFRule });

                        var full_par_n = name + "." + param_name;
                        var resultBNFRule = resultRules[full_par_n] as BNFRule;

                        basicBNFRule = new BasicBNFRule
                        {
                            new BNFReference(n1)
                        };

                        TestUtil.AssertBNF(resultBNFRule, full_par_n,
                          new BasicBNFRule[] { basicBNFRule });


                        var full_par_n_2 = name + "." + param_name_2;
                        resultBNFRule = resultRules[full_par_n_2] as BNFRule;

                        basicBNFRule = new BasicBNFRule
                        {
                            new BNFReference(n2)
                        };

                        TestUtil.AssertBNF(resultBNFRule, full_par_n_2,
                          new BasicBNFRule[] { basicBNFRule });

                    }
                    
                }
            }
        }
    }
}
