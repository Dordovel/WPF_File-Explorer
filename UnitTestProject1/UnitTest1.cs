using System;
using System.IO;
using System.Linq;
using Microsoft . VisualStudio . TestTools . UnitTesting;
using WpfApp1.File;
using WpfApp1.Controller;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PersonFile file = new PersonFile();

            string path = "D:\\Test";

            string msg = path + "\\fla";

            Assert.AreEqual(msg, file.getFile(path)[0]);
        }



        [TestMethod]
        public void TestMethod2()
        {
            PersonFile file = new PersonFile ( );

            string path = "D:\\Test";

            string msg = path + "\\1.txt";

            Assert . AreEqual ( msg , file . getFile ( path ) [ 1 ] );
        }
    }
}
