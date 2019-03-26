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

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFString(typep + exbnf)
                        };

                        TestUtil.AssertBNF(bnfrule, exname, basicBNFRule );

                        var expected = component.TransformToRules("/start\n" +
                            "b ::= ab\n" +
                            "/end").GetBaseRules["b"] as BNFRule;

                        Assert.IsTrue(expected.Equals(resultRules["b"] as BNFRule));
                    }


                    [TestMethod]
                    public void TransformRulesTypeTypeEx()
                    {
                        //arrange
                        var typename = "a";
                        var typep = "a";
                        var typebnf = typep + "/child";
                        var exname = "b";
                        var exbnf = "b/child";
                        var rules_txt = "/start\n" +
                            "/type " + typename + " ::= " + typebnf + "\n" +
                            "/type= " + typename + " /type " + exname + " ::= " + exbnf + '\n' +
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

                        var bnfrule = resultRules[exname] as TypeRule;

                        Assert.IsNotNull(bnfrule);

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFString("ab" ),
                            new BNFSystemRef(new Child())
                        };
                        
                        TestUtil.AssertBNF(bnfrule, exname, basicBNFRule);

                        var expected = component.TransformToRules("/start\n" +
                            "/type b ::= ab/child\n" +
                            "/end").GetBaseRules["b"] as TypeRule;

                        Assert.IsTrue(expected.Equals(bnfrule));
                    }

                    [TestMethod]
                    public void TransformRulesTypeWithRefTypeEx()
                    {
                        //arrange
                        var reg = "r";
                        var pattern = "[0-9]";
                        var typename = "a";
                        var typep = "<"+reg+">";
                        var typebnf = typep + "/child";
                        var exname = "b";
                        var exbnf = "b/child";
                        var rules_txt = "/start\n" +
                            "/reg "+reg + " ::= "+ pattern + "\n" +
                            "/type " + typename + " ::= " + typebnf + "\n" +
                            "/type= " + typename + " /type " + exname + " ::= " + exbnf + '\n' +
                            "/end";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules_txt);

                        //assert
                        Assert.AreEqual(0, actual.Languages.Count);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(3, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, typename);
                        CollectionAssert.Contains(resultRules.Keys, exname);

                        var bnfrule = resultRules[exname] as BNFRule;

                        Assert.IsNotNull(bnfrule);

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFReference("r"),
                            new BNFString("b" ),
                            new BNFSystemRef( new Child())
                        };

                        TestUtil.AssertBNF(bnfrule, exname, basicBNFRule);

                        var expected = component.TransformToRules("/start\n" +
                            "/reg r ::= [0-9]\n" +
                            "/type b ::= <r>b/child\n" +
                            "/end").GetBaseRules["b"] as TypeRule;

                        Assert.IsTrue(expected.Equals(resultRules["b"] as TypeRule));
                    }

                    [TestMethod]
                    public void TransformRulesTypeTypeTypeEx()
                    {
                        //arrange
                        var reg = "r";
                        var pattern = "[0-9]";
                        var typename = "a";
                        var typep = "<" + reg + ">";
                        var typebnf = typep + "/child";
                        var exname = "b";
                        var exname2 = "c";
                        var exbnf = "b/child";
                        var exbnf2 = "<r>/child";
                        var rules_txt = "/start\n" +
                            "/reg " + reg + " ::= " + pattern + "\n" +
                            "/type " + typename + " ::= " + typebnf + "\n" +
                            "/type= " + typename + " /type " + exname + " ::= " + exbnf + '\n' +
                            "/type= " + exname + " /type " + exname2 + " ::= " + exbnf2 + '\n' +
                            "/end";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules_txt);

                        //assert
                        Assert.AreEqual(0, actual.Languages.Count);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(4, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, typename);
                        CollectionAssert.Contains(resultRules.Keys, exname);

                        var bnfrule = resultRules[exname] as BNFRule;

                        Assert.IsNotNull(bnfrule);

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFReference("r"),
                            new BNFString("b" ),
                            new BNFSystemRef(new Child())
                        };

                        TestUtil.AssertBNF(bnfrule, exname, basicBNFRule);

                        var expected = component.TransformToRules("/start\n" +
                            "/reg r ::= [0-9]\n" +
                            "/type b ::= <r>b/child\n" +
                            "/end").GetBaseRules["b"] as TypeRule;

                        Assert.IsTrue(expected.Equals(resultRules["b"] as TypeRule));
                    }


                    [TestMethod]
                    public void TransformRulesTypeTypeExWithParam()
                    {
                        //arrange
                        var reg = "r";
                        var pattern = "[0-9]";
                        var typename = "a";
                        var typename2 = "b";
                        var paramname = "c";
                        var typep = "<" + paramname + ">";
                        var typebnf =  "a/child";
                        var typebnf2 = "<"+paramname+">"+"/child";
                        var param_txt = "<" + reg + ">";
                        var rules_txt = "/start\n" +
                            "/reg " + reg + " ::= " + pattern + "\n" +
                            "/type " + typename + " ::= " + typebnf + "\n"+
                            "/type= "+typename+" /type " + typename2 + " ::= " + typebnf2 + "\n" +
                            "/params_start\n" +
                            paramname + " ::= " + param_txt + "\n" +
                            "/params_end\n" +
                            "/end";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules_txt);

                        //assert
                        Assert.AreEqual(0, actual.Languages.Count);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(4, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, typename);
                        CollectionAssert.Contains(resultRules.Keys, reg);
                        CollectionAssert.Contains(resultRules.Keys, typename2);
                        CollectionAssert.Contains(resultRules.Keys, typename2 + "."+paramname);

                        var bnfrule = resultRules[typename2] as BNFRule;

                        Assert.IsNotNull(bnfrule);

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFString("a"),
                            new BNFReference(typename2 + "."+paramname),
                            new BNFSystemRef(new Child())
                        };

                        TestUtil.AssertBNF(bnfrule, typename2, basicBNFRule);
                        
                    }


                    [TestMethod]
                    public void TransformRulesTypeWithParamTypeEx()
                    {
                        //arrange
                        var reg = "r";
                        var pattern = "[0-9]";
                        var typename = "a";
                        var typename2 = "b";
                        var paramname = "c";
                        var typep = "<" + paramname + ">";
                        var typebnf = "<" + paramname + ">" + "/child";
                        var typebnf2 = "a/child";
                        var param_txt = "<" + reg + ">";
                        var rules_txt = "/start\n" +
                            "/reg " + reg + " ::= " + pattern + "\n" +
                            "/type " + typename + " ::= " + typebnf + "\n" +
                            "/params_start\n" +
                            paramname + " ::= " + param_txt + "\n" +
                            "/params_end\n" +
                            "/type= " + typename + " /type " + typename2 + " ::= " + typebnf2 + "\n" +
                            "/end";
                        var component = new TransformationComponent();

                        //act
                        var actual = component.TransformToRules(rules_txt);

                        //assert
                        Assert.AreEqual(0, actual.Languages.Count);

                        var resultRules = actual.GetBaseRules;
                        Assert.AreEqual(4, resultRules.Count);

                        CollectionAssert.Contains(resultRules.Keys, typename);
                        CollectionAssert.Contains(resultRules.Keys, reg);
                        CollectionAssert.Contains(resultRules.Keys, typename2);
                        CollectionAssert.Contains(resultRules.Keys, typename + "." + paramname);

                        var bnfrule = resultRules[typename2] as BNFRule;

                        Assert.IsNotNull(bnfrule);

                        var basicBNFRule = new BasicBNFRule
                        {
                            new BNFReference("a.c"),
                            new BNFString("a" ),
                            new BNFSystemRef(new Child())
                        };

                        TestUtil.AssertBNF(bnfrule, typename2, basicBNFRule);

                        
                    }
                }
            }
        }
    }
}
