using GraphicLibrary.Items;
namespace IlodarAcademyTest
{
    [TestClass]
    public class ArVectorTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ArFloatVector2Test()
        {
            ArFloatVector2 f1 = new ArFloatVector2(4, 5);
            ArFloatVector2 f2 = new ArFloatVector2(0, 1);
            ArFloatVector2 f3 = new ArFloatVector2(1, 0);
            ArFloatVector2 f4 = new ArFloatVector2(-1.677f, 987.54f);
            //Assert.IsTrue(f2)
            TestContext.WriteLine(f2.AngleBetween(f3).ToString());
            TestContext.WriteLine(f2.Determinant(f3).ToString());
            TestContext.WriteLine(f1.Normalize().ToString());
            TestContext.WriteLine(f1.Normalize().GetLength().ToString());
            Console.WriteLine(ArFloatVector2.Parse("3.6, -4.1").ToString());
            Console.WriteLine(ArFloatVector2.Parse("(3.6, 60)").ToString());

            Console.WriteLine(f4.ToString("G"));
            Console.WriteLine(f4.ToString("N3"));            
            Console.WriteLine(f4.ToString("R1"));
            Console.WriteLine(f4.ToString("F1"));
            Assert.IsTrue(f1 == f1);
            Assert.IsFalse(f1 == f2);

            ArFloatVector3 f6 = new ArFloatVector3(3, 3, 2);
            ArFloatVector3 f7 = new ArFloatVector3(3, 3, 3);
            Assert.IsTrue(f6 < f7);
            Console.WriteLine(f6.Normalize().ToString());
            f2 = (ArFloatVector2)f6;
            f6 = f3;
            Console.WriteLine(f6.ToString());
            f6 = ArFloatVector3.Parse("3.7, 3.9, -444");
            Console.WriteLine(f6.ToString());
        }
    }
}