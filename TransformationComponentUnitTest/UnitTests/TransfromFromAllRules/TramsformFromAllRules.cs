using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;


namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        [TestClass]
        public partial class TramsformFromAllRules
        {
            [TestMethod]
            [TestCategory("TransformWithRules")]
            public void TransformNoLanguages()
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

                }
                catch (TransformComponentException tr)
                {
                    Assert.IsInstanceOfType(tr, typeof(ModelParseException));
                    Assert.IsInstanceOfType(tr.InnerException, typeof(NoLanguageRulesFound));
                }
            }

            [TestMethod]
            [TestCategory("TransformWithRules")]
            public void TransformNoSourceLang()
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
            public void TransformNoTargetLanguage()
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
            public void TransformNoBase()
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



            [TestMethod]
            public void SimpleTransform()
            {
                //arrange
                var text = "a";
                var sourceLang = "a";
                var targetLang = "b";
                AllRules allRules = new AllRules();
                allRules.AddBaseRules(
                    new System.Collections.Generic.Dictionary<string, Rule>
                    {
                        ["Program"] = new BNFRule("Program")
                    }
                    );
                allRules.AddLanguageRules(sourceLang, new System.Collections.Generic.Dictionary<string, Rule>
                {
                    ["Program"] = new BNFRule("Program") {new BasicBNFRule { new BNFString("a") }}
                });
                allRules.AddLanguageRules(targetLang, new System.Collections.Generic.Dictionary<string, Rule>
                {
                    ["Program"] = new BNFRule("Program") { new BasicBNFRule { new BNFString("b") } }
                });
                var transformationComponent = new TransformationComponent();
                var expected = "b";


                //act
                var actual = transformationComponent.Transform(text, allRules, sourceLang, targetLang);

                //assert
                Assert.AreEqual(expected, actual);
            }


            [TestMethod]
            public void TransformPascalToCSharp()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void TransformCSharpToPascal()
            {
                Assert.Fail();
            }
        }
    }
}
