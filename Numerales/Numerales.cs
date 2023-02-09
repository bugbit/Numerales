// Copyright © 2023 Oscar Hernandez Baño. All rights reserved.
// Use of this source code is governed by a GLP3.0 license that can be found in the LICENSE file.
// This file is part of Algebra.

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

    private static readonly Lazy<string[]> decenasStr = new(() =>
    new[]
    {
        "cero", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"
    });

    private static readonly Lazy<string[]> centenasStr = new(() =>
    new[]
    {
        "","ciento", "doscient", "trescient", "cuatrocient", "quinient", "seiscient", "setecient", "ochocient", "novecient"
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

        AddClase(sb, periodos.First(), 1, opciones);

        return sb.ToString();
    }

    private static void AddClase(StringBuilder sb, uint uniDecCen, int nperiodo, OpcionesGramatica opciones)
    {
        if (uniDecCen >= 100)
            AddCentenas(sb, uniDecCen, nperiodo, opciones);
        else
            AddUnidadesYDecenas(sb, uniDecCen, nperiodo, opciones);
    }

    private static void AddUnidadesYDecenas(StringBuilder sb, uint unidad, int nperiodo, OpcionesGramatica opciones)
    {
        if (unidad == 0 && nperiodo > 1)
            return;

        var unidadesStrTmp = unidadesStr.Value;

        if (unidad < unidadesStrTmp.Length)
        {
            sb.Append(unidadesStrTmp[unidad]);
            if (unidad == 1)
                AddGenero(sb, opciones);
        }
        else if (unidad < 100)
        {
            var decenas = Math.DivRem(unidad, 10);

            sb.Append(decenasStr.Value[decenas.Quotient]);

            if (decenas.Remainder != 0)
            {
                sb.Append(" y ");
                AddUnidadesYDecenas(sb, decenas.Remainder, nperiodo, opciones);
            }
        }
    }

    private static void AddCentenas(StringBuilder sb, uint numero, int nperiodo, OpcionesGramatica opciones)
    {
        switch (numero)
        {
            case 0:
                break;
            case 100:
                // La centena se expresa como «cien» si va sola y como «ciento» si va acompañada de decenas o unidades
                sb.Append("cien");
                break;
            default:
                if (numero > 999)
                    return;

                var centenas = Math.DivRem(numero, 100);

                sb.Append(centenasStr.Value[centenas.Quotient]);
                // La centena se expresa como «ciento» si va acompañada de decenas o unidades
                // Para expresar varias centenas, se usa el plural «cientos», uniéndose esta palabra al número que está multiplicando a «cien», aunque pueden surgir irregularidades en dicho número o en la palabra entera.
                if (centenas.Quotient > 1)
                    AddGenero(sb, opciones, true);
                if (centenas.Remainder > 0)
                {
                    sb.Append(' ');
                    AddUnidadesYDecenas(sb, centenas.Remainder, nperiodo, opciones);
                }
                break;
        }

    }

    private static void AddGenero(StringBuilder sb, OpcionesGramatica opciones, bool plurar = false)
    {
        switch (opciones)
        {
            case OpcionesGramatica.Masculino:
                sb.Append(!plurar ? "o" : "os");
                break;
            case OpcionesGramatica.Fenemino:
                sb.Append(!plurar ? "a" : "as");
                break;
            case OpcionesGramatica.Apocope:
                if (plurar)
                    sb.Append("os");
                break;
        }
    }
}
