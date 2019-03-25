using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;

namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        public partial class TransformToRules
        {
            [TestClass]
            public partial class WithLanguages
            {
                [TestMethod]
                public void SimpleOneLanguage()
                {
                    //Arrange
                    var rules = "/start\n" +
                        "a\n" +
                        "/language_start a\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/end\n";

                    var component = new TransformationComponent();

                    //Act
                    var result = component.TransformToRules(rules);

                    //Assert
                    Assert.AreEqual(1, result.Languages.Count);
                    
                }

                [TestMethod]
                public void SimpleTenLanguages()
                {
                    //Arrange
                    var rules = "/start\n" +
                        "a\n" +
                        "/language_start a\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start b\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start c\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start d\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start e\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start f\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start g\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start h\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start i\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/language_start j\n" +
                        "a ::= a\n" +
                        "/language_end\n" +
                        "/end\n";

                    var component = new TransformationComponent();

                    //Act
                    var result = component.TransformToRules(rules);

                    //Assert
                    Assert.AreEqual(10, result.Languages.Count);

                }


                [TestMethod]
                public void RequireReg()
                {
                    //Arrange
                    var rules = "/start\n" +
                        "/reg b ::= [0-9]\n" +
                        "a\n" +
                        "/language_start a\n" +
                        "a ::= abc\n" +
                        "/language_end\n" +
                        "/end\n";

                    var component = new TransformationComponent();

                    //Act
                    try
                    {
                        var result = component.TransformToRules(rules);
                    }
                    catch(RuleParseException ex)
                    {
                        Assert.IsInstanceOfType(ex.InnerException, typeof(LangRuleParseException));
                        Assert.IsInstanceOfType(ex.InnerException.InnerException, typeof(SyntaxErrorPlaced));
                        Assert.IsInstanceOfType(ex.InnerException.InnerException.InnerException, typeof(TranslateRuleRequired));
                        return;
                    }
                    //Assert
                    Assert.Fail();

                }


                [TestMethod]
                public void RequireBNFRealisation()
                {
                    //Arrange
                    var rules = "/start\n" +
                        "a\n" +
                        "/language_start a\n" +
                        "/language_end\n" +
                        "/end\n";

                    var component = new TransformationComponent();

                    //Act
                    try
                    {
                        var result = component.TransformToRules(rules);
                    }
                    catch (RuleParseException ex)
                    {
                        Assert.IsInstanceOfType(ex.InnerException, typeof(LangRuleParseException));
                        Assert.IsInstanceOfType(ex.InnerException.InnerException, typeof(SyntaxErrorPlaced));
                        Assert.IsInstanceOfType(ex.InnerException.InnerException.InnerException, typeof(EOSException));
                        return;
                    }
                    //Assert
                    Assert.Fail();

                }


                [TestMethod]
                public void NoLanguageEnd()
                {
                    //Arrange
                    var rules = "/start\n" +
                        "a\n" +
                        "/language_start a\n" +
                        "/end\n";

                    var component = new TransformationComponent();

                    //Act
                    try
                    {
                        var result = component.TransformToRules(rules);
                    }
                    catch (RuleParseException ex)
                    {
                        Assert.IsInstanceOfType(ex.InnerException, typeof(LangRuleParseException));
                        Assert.IsInstanceOfType(ex.InnerException.InnerException, typeof(SyntaxErrorPlaced));
                        Assert.IsInstanceOfType(ex.InnerException.InnerException.InnerException, typeof(EOSException));
                    }
                    //Assert
                    Assert.Fail();

                }

            }
        }
    }
}
