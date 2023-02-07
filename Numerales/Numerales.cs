using System.Text;
using System.Numerics;


namespace Numerales;

public static class Numerales
{
    public static string ToCardinal(BigInteger numero, OpcionesGramatica opciones)
    {
        var texto = new StringBuilder();

        if (numero < BigInteger.Zero)
        {
            texto.Append("menos ");
            numero = -numero;
        }

        var ndigits = (int)BigInteger.Log10(numero) + 1;
        var div10 = BigInteger.Pow(10, ndigits / 3);

        return texto.ToString();
    }
}
