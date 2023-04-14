using MinecraftConnectionBE;

namespace LibTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var command = new MinecraftCommands("localhost", 8080);
        }
    }
}