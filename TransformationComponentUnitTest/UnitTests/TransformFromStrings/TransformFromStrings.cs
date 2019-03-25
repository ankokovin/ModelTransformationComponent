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
                            "main\n" +
                            "/language_start a\n" +
                            "main ::= a\n" +
                            "/language_end\n" +
                            "/language_start b\n" +
                            "main ::= b\n" +
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
        }
    }
}
