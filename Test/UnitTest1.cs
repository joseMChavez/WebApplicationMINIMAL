using NUnit.Framework;
using WebApplicationMINIMAL.Services;

namespace Test
{
    public class Tests
    {

        private  ITodoService service;
        [SetUp]
        public void Setup()
        {
            service = new ToDoService();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}