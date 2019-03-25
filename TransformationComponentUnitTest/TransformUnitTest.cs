using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;
using ModelTransformationComponent.SystemRules;


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

        #region BaseOnly

        #region Basic
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
            Assert.AreEqual(actual.Languages.Count,0);
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
            Assert.AreEqual(actual.Languages.Count,0);
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
            Assert.AreEqual(actual.Languages.Count,0);
            Assert.AreEqual(actual.GetBaseRules.Count,0);
        }

        #endregion
        #region SystemRule
        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesFakeSys()
        {
            var fakeName = "aofjpawf";
            //arrange
            var rules = "Some strange comment-like text\n" +
            "with some new lines and //////star\n" +
            "but eventually /start\n" +
            fakeName+"\n"+
            "/end\n" +
            "Some more comments";
            var component = new TransformationComponent();

            //act
            try
            {
                var actual = component.TransformToRules(rules);

            }//assert
            catch(TransformComponentException tr)
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
            expBasicBNF.elements.Add(new BNFSystemRef{ rule = rule});
            TestUtil.AssertBNF(resultBNFRule, name,
                new BasicBNFRule[1] { expBasicBNF });

        }

        #endregion
        #region Reg
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
            Assert.AreEqual(actual.Languages.Count,0);

            var resultRules = actual.GetBaseRules;
            Assert.AreEqual(resultRules.Count,1);

            Assert.IsTrue(resultRules.ContainsKey(name));

            var resultRegRule = resultRules[name] as RegexRule;
            TestUtil.AssertReg(resultRegRule, name, pattern);
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
            Assert.AreEqual(actual.Languages.Count,0);

            var resultRules = actual.GetBaseRules;
            Assert.AreEqual(resultRules.Count,2);

            Assert.IsTrue(resultRules.ContainsKey(name));

            var resultRegRule = resultRules[name] as RegexRule;

            TestUtil.AssertReg(resultRegRule, name, pattern);

            resultRegRule = resultRules[name2] as RegexRule;
            TestUtil.AssertReg(resultRegRule, name2, pattern2);
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
        #endregion Reg
        #region BNF
        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesBNF(){
            //arrange
            var name = "a";
            var str = "a";
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            name + " ::= "+str+ "\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.Languages.Count,0);

            var resultRules = actual.GetBaseRules;
            Assert.AreEqual(1,resultRules.Count);

            Assert.IsTrue(resultRules.ContainsKey(name));

            var resultBNFRule = resultRules[name] as BNFRule;

            var expBasicBNF = new BasicBNFRule();
            expBasicBNF.elements.Add(new BNFString { Value = str });
            TestUtil.AssertBNF(resultBNFRule, name,
                new BasicBNFRule[1] { expBasicBNF });
            
        }

        
        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesBNFEmpty(){
            //arrange
            var name = "a";
            var rules = "Some strange comment-like text\n"+
            "with some new lines and //////star\n"+
            "but eventually /start\n"+
            name +"\n"+
            "/end\n"+
            "Some more comments";
            var component = new TransformationComponent();

            //act
            var actual = component.TransformToRules(rules);
            
            //assert
            Assert.AreEqual(actual.Languages.Count,0);

            var resultRules = actual.GetBaseRules;
            Assert.AreEqual(1,resultRules.Count);

            Assert.IsTrue(resultRules.ContainsKey(name));

            var resultBNFRule = resultRules[name] as BNFRule;

            TestUtil.AssertBNF(resultBNFRule, name,
                new BasicBNFRule[0] );
            
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
            name + " ::= "+str+"\n" +
            name2 + " ::= "+str2+"\n" +
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
            TestUtil.AssertBNF(resultBNFRule, name2,
                new BasicBNFRule[1] { expBasicBNF });
        }


        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesBNFFakeSys()
        {
            //arrange
            var name = "a";
            var fakeName = "adkjlxfkbn" ;
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
            name + " ::= "+str+"\n" +
            name2 + " ::= <"+name+">\n" +
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


            Assert.IsTrue(resultRules.ContainsKey(name));

            resultBNFRule = resultRules[name2] as BNFRule;



            expBasicBNF = new BasicBNFRule();
            expBasicBNF.elements.Add(new BNFReference{ Name = name });
            TestUtil.AssertBNF(resultBNFRule, name2,
                new BasicBNFRule[1] { expBasicBNF });

        }
        #endregion BNF
        #region Type

        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesEmptyType(){
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

            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[0]);
            
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

            var basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFString() { Value = "a" });
            basicBNFRule.elements.Add(new BNFSystemRef() { rule = new Child() });

            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });
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

            var basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            basicBNFRule.elements.Add(new BNFSystemRef() { rule = new Child() });
            

            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });
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

            var basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            basicBNFRule.elements.Add(new BNFString() { Value = "a" });
            basicBNFRule.elements.Add(new BNFSystemRef() { rule = new Child() });


            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });
        }
        
        #endregion Type
        #region Params

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
            "/params_start\n"+
            "/params_end\n"+
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
            "/type " + name  + " ::= " + bnf+"\n" +
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
            basicBNFRule.elements.Add(new BNFString() {Value = "a" });
            basicBNFRule.elements.Add(new BNFSystemRef() { rule = new Child() });

            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule});


        }

        [TestMethod]
        [TestCategory("TransformToRules")]
        public void ToRulesTypeOneParamNoBody()
        {
            //arrange
            var name = "a";
            var param_name = "b";
            var bnf = "<"+param_name+">/child";
            var rules = "Some strange comment-like text\n" +
            "with some new lines and //////star\n" +
            "but eventually /start\n" +
            "/type " + name + " ::= " + bnf + "\n" +
            "/params_start\n" +
            param_name + "\n"+
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
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            basicBNFRule.elements.Add(new BNFSystemRef() { rule = new Child() });

            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });

            var full_par_n = name + "." + param_name;
            var resultBNFRule = resultRules[full_par_n] as BNFRule;

            TestUtil.AssertBNF(resultBNFRule, param_name,
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
            var param_body = "<"+n1+">";
            var bnf = "<" + param_name + ">/child";
            var rules = "Some strange comment-like text\n" +
            "with some new lines and //////star\n" +
            "but eventually /start\n" +
            n1 + "\n" +
            "/type " + name + " ::= " + bnf + "\n" +
            "/params_start\n" +
            param_name + " ::= "+ param_body +"\n" +
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
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            basicBNFRule.elements.Add(new BNFSystemRef() { rule = new Child() });

            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });

            var full_par_n = name + "." + param_name;
            var resultBNFRule = resultRules[full_par_n] as BNFRule;

            basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = n1 });

            TestUtil.AssertBNF(resultBNFRule, param_name,
              new BasicBNFRule[] {basicBNFRule});

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
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            basicBNFRule.elements.Add(new BNFSystemRef() { rule = new Child() });

            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });

            var full_par_n = name + "." + param_name;
            var resultBNFRule = resultRules[full_par_n] as BNFRule;

            basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = n1 });

            TestUtil.AssertBNF(resultBNFRule, param_name,
              new BasicBNFRule[] { basicBNFRule });


            var full_par_n_2 = name + "." + param_name_2;
            resultBNFRule = resultRules[full_par_n_2] as BNFRule;

            basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = n2 });

            TestUtil.AssertBNF(resultBNFRule, param_name_2,
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
            basicBNFRule.elements.Add(new BNFString() { Value = "a" });
            
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
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            
            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });

            var full_par_n = name + "." + param_name;
            var resultBNFRule = resultRules[full_par_n] as BNFRule;

            TestUtil.AssertBNF(resultBNFRule, param_name,
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
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            
            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });

            var full_par_n = name + "." + param_name;
            var resultBNFRule = resultRules[full_par_n] as BNFRule;

            basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = n1 });

            TestUtil.AssertBNF(resultBNFRule, param_name,
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
            basicBNFRule.elements.Add(new BNFReference() { Name = "b" });
            
            TestUtil.AssertBNF(resultTypeRule, name,
                new BasicBNFRule[] { basicBNFRule });

            var full_par_n = name + "." + param_name;
            var resultBNFRule = resultRules[full_par_n] as BNFRule;

            basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = n1 });

            TestUtil.AssertBNF(resultBNFRule, param_name,
              new BasicBNFRule[] { basicBNFRule });


            var full_par_n_2 = name + "." + param_name_2;
            resultBNFRule = resultRules[full_par_n_2] as BNFRule;

            basicBNFRule = new BasicBNFRule();
            basicBNFRule.elements.Add(new BNFReference() { Name = n2 });

            TestUtil.AssertBNF(resultBNFRule, param_name_2,
              new BasicBNFRule[] { basicBNFRule });

        }

        #endregion Params

        #region TypeEx
        //TO DO!
        #endregion TypeEx
        #region Mix
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
            "/reg " + name + " ::= "+pattern+"\n" +
            name2 + " ::= <"+name+">\n" +
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

            

            var expBasicBNF = new BasicBNFRule();
            expBasicBNF.elements.Add(new BNFReference { Name = name });
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
            name + " ::= " + str1 + "|" + str2 + "\n"+
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
            expBasicBNF.elements.Add(new BNFString { Value = str1});

            var expBasicBNF2 = new BasicBNFRule();
            expBasicBNF2.elements.Add(new BNFString { Value = str2 });
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
            name + " ::= " + "/empty |"+str+"<" + name + ">\n"+
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
            expBasicBNF.elements.Add(new BNFSystemRef { rule= new Empty()});
            
            var expBasicBNF2 = new BasicBNFRule();
            expBasicBNF2.elements.Add(new BNFString { Value = str });
            expBasicBNF2.elements.Add(new BNFReference { Name = name});
            TestUtil.AssertBNF(resultBNFRule, name,
                new BasicBNFRule[2] { expBasicBNF, expBasicBNF2 });

        }
        #endregion Mix
        #endregion BaseOnly

        #region WithLanguages
        #endregion
        #endregion

        #region Transform(string,AllRules,string,string)

        [TestMethod]
        [TestCategory("TransformWithRules")]
        public void TransfromNoLanguages()
        {
            //arrange
            string text = "Some text";

            AllRules allRules = new AllRules();
            allRules.AddBaseRules(new System.Collections.Generic.Dictionary<string, Rule>());
            string sourceLang = "a";

            string targetLang = "b";

            TransformationComponent transformationComponent = new TransformationComponent();

            //act
            try
            {
                transformationComponent.Transform(text, allRules, sourceLang, targetLang);

            }catch(TransformComponentException tr)
            {
                Assert.IsInstanceOfType(tr, typeof(ModelParseException));
                Assert.IsInstanceOfType(tr.InnerException, typeof(NoLanguageRulesFound));
            }
        }

        [TestMethod]
        [TestCategory("TransformWithRules")]
        public void TransfromNoSourceLang()
        {
            //arrange
            string text = "Some text";

            AllRules allRules = new AllRules();

            allRules.AddBaseRules(new System.Collections.Generic.Dictionary<string, Rule>());

            string sourceLang = "a";

            string targetLang = "b";

            allRules.AddLanguageRules(targetLang, new System.Collections.Generic.Dictionary<string, Rule>());

            

            TransformationComponent transformationComponent = new TransformationComponent();

            //act
            try
            {
                transformationComponent.Transform(text, allRules, sourceLang, targetLang);

            }
            catch (TransformComponentException tr)
            {
                Assert.IsInstanceOfType(tr, typeof(ModelParseException));
                Assert.IsInstanceOfType(tr.InnerException, typeof(NoLanguageRulesFound));
            }
        }

        [TestMethod]
        [TestCategory("TransformWithRules")]
        public void TransfromNoTargetLanguage()
        {
            //arrange
            string text = "Some text";

            AllRules allRules = new AllRules();

            allRules.AddBaseRules(new System.Collections.Generic.Dictionary<string, Rule>());

            string sourceLang = "a";

            string targetLang = "b";

            allRules.AddLanguageRules(sourceLang, new System.Collections.Generic.Dictionary<string, Rule>());


            TransformationComponent transformationComponent = new TransformationComponent();

            //act
            try
            {
                transformationComponent.Transform(text, allRules, sourceLang, targetLang);

            }
            catch (TransformComponentException tr)
            {
                Assert.IsInstanceOfType(tr, typeof(ModelParseException));
                Assert.IsInstanceOfType(tr.InnerException, typeof(NoLanguageRulesFound));
            }
        }

        [TestMethod]
        [TestCategory("TransformWithRules")]
        public void TransfromNoBase()
        {
            //arrange
            string text = "Some text";

            AllRules allRules = new AllRules();
            
            string sourceLang = "a";

            string targetLang = "b";

            allRules.AddLanguageRules(sourceLang, new System.Collections.Generic.Dictionary<string, Rule>());
            allRules.AddLanguageRules(targetLang, new System.Collections.Generic.Dictionary<string, Rule>());


            TransformationComponent transformationComponent = new TransformationComponent();

            //act
            try
            {
                transformationComponent.Transform(text, allRules, sourceLang, targetLang);

            }
            catch (TransformComponentException tr)
            {
                Assert.IsInstanceOfType(tr, typeof(ModelParseException));
                Assert.IsInstanceOfType(tr.InnerException, typeof(NoBaseRulesFound));
            }
        }


        #endregion
        
    }
}
