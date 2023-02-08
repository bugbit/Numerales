using Numerales;
using System.Diagnostics;
using static Numerales.Numerales;

namespace Numerales.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var str = ToCardinal(1, OpcionesGramatica.Masculino);

        Debug.Assert(str == "uno");
    }
}
