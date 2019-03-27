using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;


namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        [TestClass]
        public partial class TransformFromStrings
        {
            [TestMethod]
            [TestCategory("FullTransform")]
            public void HasTwoLang_BNF()
            {
                //arrange
                var rules = "/start\n" +
                            "Program\n" +
                            "/language_start a\n" +
                            "Program ::= a\n" +
                            "/language_end\n" +
                            "/language_start b\n" +
                            "Program ::= b\n" +
                            "/language_end\n" +
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


            [TestMethod]
            public void PascalToCSharp()
            {
                //arrange
                var rules = TransformationComponentUnitTest.Resource1.CSharpPascalRules;
                var source = TransformationComponentUnitTest.Resource1.PascalSource;
                var component = new TransformationComponent();

                //act
                var actual = component.Transform(source, rules, "Pascal", "CSharp");

                System.Diagnostics.Debug.WriteLine(actual);
            }
        }
    }
}
