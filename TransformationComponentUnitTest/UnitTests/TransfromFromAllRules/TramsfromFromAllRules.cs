using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;


namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        [TestClass]
        public partial class TramsfromFromAllRules
        {
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

                }
                catch (TransformComponentException tr)
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
            
        }
    }
}
