using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;


namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        public partial class TransformToRules
        {
            [TestClass]
            public partial class OnlyBase
            {
                #region Basic
                [TestMethod]
                [TestCategory("TransformToRules")]
                public void ToRulesEmpty()
                {
                    //arrange
                    var rules = string.Empty;
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

                        Assert.IsInstanceOfType(e.InnerException, typeof(InputIsEmpty));
                    }

                }

                [TestMethod]
                [TestCategory("TransformToRules")]
                public void ToRulesNoStart()
                {
                    //arrange
                    var rules = "Some strange comment-like text\n" +
                    "with some new lines and //////star";
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

                        Assert.IsInstanceOfType(e.InnerException, typeof(NoStartDetected));
                    }
                }


                [TestMethod]
                [TestCategory("TransformToRules")]
                public void ToRulesNoEnd()
                {
                    //arrange
                    var rules = "Some strange comment-like text\n" +
                    "with some new lines and //////star\n" +
                    "but eventually /start\n" +
                    "however there is no end /eeee /en";
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

                        Assert.IsInstanceOfType(e.InnerException, typeof(NoEndDetected));
                    }
                }

                [TestMethod]
                [TestCategory("TransformToRules")]
                public void ToRulesEmptyBody()
                {
                    //arrange
                    var rules = "Some strange comment-like text\n" +
                    "with some new lines and //////star\n" +
                    "but eventually /start\n" +
                    "/end\n" +
                    "Some more comments";
                    var component = new TransformationComponent();

                    //act
                    var actual = component.TransformToRules(rules);

                    //assert
                    Assert.AreEqual(actual.Languages.Count, 0);
                    Assert.AreEqual(actual.GetBaseRules.Count, 0);
                }


                [TestMethod]
                [TestCategory("TransformToRules")]
                public void ToRulesEmptyBodyClean()
                {
                    //arrange
                    var rules = "/start\n" +
                                "/end\n";
                    var component = new TransformationComponent();

                    //act
                    var actual = component.TransformToRules(rules);

                    //assert
                    Assert.AreEqual(actual.Languages.Count, 0);
                    Assert.AreEqual(actual.GetBaseRules.Count, 0);
                }


                [TestMethod]
                [TestCategory("TransformToRules")]
                public void ToRulesEmptyBodyWithSpaces()
                {
                    //arrange
                    var rules = "Some strange comment-like text\n" +
                    "with some new lines and //////star\n" +
                    "but eventually /start\n" +
                    "\n\n\n\n\n" +
                    "/end\n" +
                    "Some more comments";
                    var component = new TransformationComponent();

                    //act
                    var actual = component.TransformToRules(rules);

                    //assert
                    Assert.AreEqual(actual.Languages.Count, 0);
                    Assert.AreEqual(actual.GetBaseRules.Count, 0);
                }

#endregion
            }
        }
    }
}
