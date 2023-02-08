using System.Text;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Numerales;

public static class Numerales
{
    private const uint millonUInt = 1000000u;
    //private const ulong billonUInt = (ulong)millonUInt * millonUInt;
    //private const ulong trillonUInt = billonUInt * millonUInt;
    //private const ulong cuatrillonUInt = (ulong)trillonUInt * millonUInt;
    private static readonly Lazy<string[]> unidadesStr = new(() =>
    new[]
    {
        "cero", "un", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", "once", "doce",
        "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve", "veinte", "veintiuno",
        "veintidós", "veintitrés", "veinticuatro", "veinticinco", "veintiséis", "veintisiete", "veintiocho", "veintinueve"
    });

    //private static readonly Lazy<uint[]> pows1000Int = new(() => new[]
    //{
    //    1000000u,1000000000000u
    //});
    public static string ToCardinal(uint numero, OpcionesGramatica opciones) => ToCardinal(false, numero, opciones);
    public static string ToCardinal(int numero, OpcionesGramatica opciones)
    {
        bool negative = numero < 0;

        if (negative)
            numero = -numero;

        return ToCardinal(negative, (uint)numero, opciones);

    }
    public static string ToCardinal(ulong numero, OpcionesGramatica opciones) => ToCardinal(false, numero, opciones);
    public static string ToCardinal(long numero, OpcionesGramatica opciones)
    {
        bool negative = numero < 0;

        if (negative)
            numero = -numero;

        return ToCardinal(negative, (ulong)numero, opciones);
    }
    //public static string ToCardinal(BigInteger numero, OpcionesGramatica opciones)
    //{
    //    var texto = new StringBuilder();

    //    if (numero < BigInteger.Zero)
    //    {
    //        texto.Append("menos ");
    //        numero = -numero;
    //    }

    //    var ndigits = (int)BigInteger.Log10(numero) + 1;
    //    var div10 = BigInteger.Pow(10, ndigits / 3);

    //    return texto.ToString();
    //}

    private static void AddPeriodos(List<uint> periodos, ulong numero)
    {
        //if (numero > trillonUInt)
        //{
        //    AddPeriodos(periodos, (BigInteger)numero);

        //    return;
        //}
    }

    private static void AddPeriodos(List<uint> periodos, uint numero)
    {
        var part = numero / millonUInt;

        if (part > 0)
        {
            periodos.Add(part);
            numero = numero % millonUInt;
        }

        periodos.Add(numero);
    }

    private static string ToCardinal(bool negative, uint numero, OpcionesGramatica opciones)
    {
        var periodos = new List<uint>();

        AddPeriodos(periodos, numero);

        return ToCardinal(negative, periodos.Count, periodos, opciones);
    }

    private static string ToCardinal(bool negative, ulong numero, OpcionesGramatica opciones)
    {
        var periodes = new List<uint>();

        AddPeriodos(periodes, numero);

        return string.Empty;
    }

    private static string ToCardinal(bool negative, int numperiodos, IEnumerable<uint> periodos, OpcionesGramatica opciones)
    {
        var sb = new StringBuilder();

        if (negative)
            sb.Append("menos ");

        AddUnidades(sb, periodos.First(), opciones);

        return sb.ToString();
    }

    private static void AddUnidades(StringBuilder sb, uint unidad, OpcionesGramatica opciones)
    {
        var unidadesStrTmp = unidadesStr.Value;

        if (unidad < unidadesStrTmp.Length)
        {
            sb.Append(unidadesStrTmp[unidad]);
            if (unidad == 1)
                AddGenero(sb, opciones);
        }
    }

    private static void AddGenero(StringBuilder sb, OpcionesGramatica opciones)
    {
        switch (opciones)
        {
            case OpcionesGramatica.Masculino:
                sb.Append('o');
                break;
            case OpcionesGramatica.Fenemino:
                sb.Append('o');
                break;
        }
    }
}
