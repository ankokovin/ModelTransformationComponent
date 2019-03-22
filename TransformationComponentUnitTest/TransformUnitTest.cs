using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;
namespace TransformationComponentUnitTest
{
    [TestClass]
    public class TransformUnitTest
    {
        #region Transform(string, string, string, string)
        [TestMethod]
        [TestCategory("FullTransform")]
        public void HasTwoLang_BNF()
        {
            //arrange
            var rules = "/start\n" +
                        "main\n" +
                        "/language_start a\n" +
                        "main ::= a\n" +
                        "/language_end\n" +
                        "/language_start b\n" +
                        "main ::= b\n" +
                        "/language_end\n"+
                        "/end";
            var text = "a";
            var component = new TransformationComponent();
            var expected = "b";

            //act
            var actual = component.Transform(text, rules, "a", "b");

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region TransformToRules(string)
        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesEmpty(){
            //arrange
            var rules = string.Empty;
            var component = new TransformationComponent();

            //act
            try{
                var actual = component.TransformToRules(rules);
            }
            catch(System.Exception e){
                //assert

                Assert.IsInstanceOfType(e, typeof(RuleParseException));
                
                Assert.IsInstanceOfType(e.InnerException, typeof(InputIsEmpty));
            }

        }

        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesNoStart(){
            //arrange
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star";
            var component = new TransformationComponent();

            //act
            try{
                var actual = component.TransformToRules(rules);
            }
            catch(System.Exception e){
                //assert

                Assert.IsInstanceOfType(e, typeof(RuleParseException));
                
                Assert.IsInstanceOfType(e.InnerException, typeof(NoStartDetected));
            }
        }


        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesNoEnd(){
            //arrange
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            "however there is no end /eeee /en";
            var component = new TransformationComponent();

            //act
            try{
                var actual = component.TransformToRules(rules);
            }
            catch(System.Exception e){
                //assert

                Assert.IsInstanceOfType(e, typeof(RuleParseException));
                
                Assert.IsInstanceOfType(e.InnerException, typeof(NoEndDetected));
            }
        }

        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesEmptyBody(){
            //arrange
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.GetLanguages.Count,0);
            Assert.AreEqual(actual.GetBaseRules.Count,0);
        }
        
        
        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesEmptyBodyClean(){
            //arrange
            var rules = "/start\n"+
                        "/end\n";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.GetLanguages.Count,0);
            Assert.AreEqual(actual.GetBaseRules.Count,0);
        }
        

        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesEmptyBodyWithSpaces(){
            //arrange
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            "\n\n\n\n\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.GetLanguages.Count,0);
            Assert.AreEqual(actual.GetBaseRules.Count,0);
        }

        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesOneReg(){
            //arrange
            var pattern = "[1-9][0-9]*|0";
            var name = "a";
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            "/reg "+ name + " ::= "+ pattern +"\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.GetLanguages.Count,0);

            var resultRules = actual.GetBaseRules;
            Assert.AreEqual(resultRules.Count,1);

            Assert.IsTrue(resultRules.ContainsKey(name));

            var resultRegRule = resultRules[name] as RegexRule;

            Assert.IsNotNull(resultRegRule);
        
            Assert.AreEqual(resultRegRule.Pattern, pattern);
        }


        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesTwoReg(){
            //arrange
            var pattern = "[1-9][0-9]*|0";
            var name = "a";
            var pattern2 = @"\w+";
            var name2 = "b";
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            "/reg "+ name + " ::= "+ pattern +"\n"+
            "/reg "+ name2+ " ::= "+ pattern2+"\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.GetLanguages.Count,0);

            var resultRules = actual.GetBaseRules;
            Assert.AreEqual(resultRules.Count,2);

            Assert.IsTrue(resultRules.ContainsKey(name));

            var resultRegRule = resultRules[name] as RegexRule;

            Assert.IsNotNull(resultRegRule);
        
            Assert.AreEqual(resultRegRule.Pattern, pattern);

            Assert.IsTrue(resultRules.ContainsKey(name2));

            resultRegRule = resultRules[name2] as RegexRule;

            Assert.IsNotNull(resultRegRule);
        
            Assert.AreEqual(resultRegRule.Pattern, pattern2);
        }


        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesTwoRegSameName(){
            //arrange
            var pattern = "[1-9][0-9]*|0";
            var name = "a";
            var pattern2 = @"\w+";
            var name2 = name;
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            "/reg "+ name + " ::= "+ pattern +"\n"+
            "/reg "+ name2+ " ::= "+ pattern2+"\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            try{
                var actual = component.TransformToRules(rules);
            }catch(System.Exception e){
                //assert
                Assert.IsInstanceOfType(e, typeof(RuleParseException));
                Assert.IsInstanceOfType(e.InnerException, typeof(BaseRuleParseException));
                Assert.IsInstanceOfType(e.InnerException.InnerException, typeof(SyntaxErrorPlaced));
                Assert.IsInstanceOfType(e.InnerException.InnerException.InnerException, typeof(ConstructAlreadyDefined));
            }
        }

        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesBNF(){
            //arrange
            var name = "a";
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            name + " ::= a\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.GetLanguages.Count,0);

            var resultRules = actual.GetBaseRules;
            Assert.AreEqual(1,resultRules.Count);

            Assert.IsTrue(resultRules.ContainsKey(name));

            var resultBNFRule = resultRules[name] as BNFRule;

            Assert.IsNotNull(resultBNFRule);

            Assert.AreEqual(1,resultBNFRule.OrSplits.Count);

            var resultSimpleBNFRule = resultBNFRule.OrSplits[0];

            /*
            TO DO: when i know, wtf is basebnfrule, assert it
                         */
        }
        
        #endregion
    }
}
