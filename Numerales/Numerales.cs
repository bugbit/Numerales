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
    private const ulong billonUInt = (ulong)millonUInt * millonUInt;
    private const ulong trillonUInt = billonUInt * millonUInt;
    private static readonly ulong[] millonsULong = { trillonUInt, billonUInt, millonUInt };
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

    private static readonly Lazy<string[]> millonesStr = new(() =>
    new[]
    {
        "mill","bill","trill"
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
    //    var texto = new Words();

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

    private static string ToCardinal(bool negative, uint numero, OpcionesGramatica opciones)
    {
        var words = new Words();

        if (numero == 0)
            AddZero(words);
        else
        {
            AddIfNegative(negative, words);

            var part = numero / millonUInt;

            if (part > 0)
            {
                AddPeriodo(words, 2, part, opciones);
                numero = numero % millonUInt;
            }

            if (numero > 0)
                AddPeriodo(words, 1, numero, opciones);
        }

        return words.ToString();
    }

    private static string ToCardinal(bool negative, ulong numero, OpcionesGramatica opciones)
    {
        var words = new Words();

        if (numero == 0)
            AddZero(words);
        else
        {
            AddIfNegative(negative, words);

            int nperiodo = millonsULong.Length + 1;

            foreach (var partMillon in millonsULong)
            {
                var part = numero / partMillon;

                if (part > 0)
                {
                    AddPeriodo(words, nperiodo, (uint)part, opciones);
                    numero = numero % partMillon;
                }
                nperiodo--;
            }

            if (numero > 0)
                AddPeriodo(words, nperiodo, (uint)numero, opciones);
        }

        return words.ToString();
    }

    private static void AddIfNegative(bool negative, Words words)
    {
        if (negative)
            AddNegative(words);
    }

    private static void AddZero(Words words) => words.AddWord("cero");

    private static void AddNegative(Words words)
    {
        words.AddWord("menos");
    }

    private static void AddPeriodo(Words words, int nperiodo, uint uniDecCenMil, OpcionesGramatica opciones)
    {
        // es zero
        if (uniDecCenMil == 0)
            return;

        // Los múltiplos de un millón siempre son masculinos: se dice «quinientos millones de personas»
        var op = nperiodo > 1 ? OpcionesGramatica.Apocope : opciones;
        var plural = uniDecCenMil > 1;

        if (uniDecCenMil > 999)
        {
            var millares = Math.DivRem(uniDecCenMil, 1000);

            // La norma general es simplemente escribir los millares, seguidos de «mil», más el número de tres cifras 
            // que siga, excepto si la cifra de millares es 1. En este caso se omiten los millares, 
            //escribiendo solo "mil" en lugar de "un mil"
            if (millares.Quotient > 1)
                AddClase(words, millares.Quotient, nperiodo, (op == OpcionesGramatica.Masculino) ? OpcionesGramatica.Apocope : op);
            words.AddWord("mil");

            uniDecCenMil = millares.Remainder;
        }
        if (uniDecCenMil > 0)
            AddClase(words, uniDecCenMil, nperiodo, op);

        if (nperiodo >= 2)
        {
            words.AddWord(millonesStr.Value[nperiodo - 2]);
            words.AddCaracters(plural ? "ones" : "ón");
        }
    }

    private static void AddClase(Words words, uint uniDecCen, int nperiodo, OpcionesGramatica opciones)
    {
        if (uniDecCen >= 100)
            AddCentenas(words, uniDecCen, nperiodo, opciones);
        else
            AddUnidadesYDecenas(words, uniDecCen, nperiodo, opciones);
    }

    private static void AddUnidadesYDecenas(Words words, uint unidad, int nperiodo, OpcionesGramatica opciones)
    {
        if (unidad == 0)
            return;

        var unidadesStrTmp = unidadesStr.Value;

        if (unidad == 21 && opciones != OpcionesGramatica.Masculino)
        {
            words.AddWord((opciones == OpcionesGramatica.Apocope) ? "veintiún" : "veintiuna");
        }
        else if (unidad < unidadesStrTmp.Length)
        {
            words.AddWord(unidadesStrTmp[unidad]);
            if (unidad == 1)
                words.AddGenero(opciones, true, false);
        }
        else if (unidad < 100)
        {
            var decenas = Math.DivRem(unidad, 10);

            words.AddWord(decenasStr.Value[decenas.Quotient]);

            if (decenas.Remainder != 0)
            {
                words.AddWord("y");
                AddUnidadesYDecenas(words, decenas.Remainder, nperiodo, opciones);
            }
        }
    }

    private static void AddCentenas(Words words, uint numero, int nperiodo, OpcionesGramatica opciones)
    {
        switch (numero)
        {
            case 0:
                break;
            case 100:
                // La centena se expresa como «cien» si va sola y como «ciento» si va acompañada de decenas o unidades
                words.AddWord("cien");
                break;
            default:
                if (numero > 999)
                    return;

                var centenas = Math.DivRem(numero, 100);

                words.AddWord(centenasStr.Value[centenas.Quotient]);
                // La centena se expresa como «ciento» si va acompañada de decenas o unidades
                // Para expresar varias centenas, se usa el plural «cientos», uniéndose esta palabra al número que está multiplicando a «cien», aunque pueden surgir irregularidades en dicho número o en la palabra entera.
                if (centenas.Quotient > 1)
                    words.AddGenero(opciones, false, true);
                if (centenas.Remainder > 0)
                    AddUnidadesYDecenas(words, centenas.Remainder, nperiodo, opciones);
                break;
        }
    }
}
