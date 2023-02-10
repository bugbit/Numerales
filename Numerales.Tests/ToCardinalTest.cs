// Copyright � 2023 Oscar Hernandez Ba�o. All rights reserved.
// Use of this source code is governed by a GLP3.0 license that can be found in the LICENSE file.
// This file is part of Algebra.

using System;
using Humanizer;

namespace Numerales.Tests;

[TestClass]
public class ToCardinalTest
{
    [TestMethod]
    public void TestUno()
    {
        var str = ToCardinal(1, OpcionesGramatica.Masculino);

        Debug.Assert(str == "uno");

        str = ToCardinal(1, OpcionesGramatica.Apocope);

        Debug.Assert(str == "un");

        str = ToCardinal(1, OpcionesGramatica.Fenemino);

        Debug.Assert(str == "una");
    }

    [TestMethod]
    public void TestDecenasHasta30()
    {
        string[] decenas ={
            "cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", "once", "doce",
        "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve", "veinte", "veintiuno",
        "veintidós", "veintitrés", "veinticuatro", "veinticinco", "veintiséis", "veintisiete", "veintiocho", "veintinueve"
        };

        for (var i = 0; i < decenas.Length; i++)
        {
            var str = ToCardinal(i, OpcionesGramatica.Masculino);

            Debug.Assert(str == decenas[i], str);
        }
    }

    [TestMethod]
    public void TestDecenasDesde30()
    {
        string[] decenas ={
            "treinta","treinta y uno","treinta y dos","treinta y tres","treinta y cuatro","treinta y cinco","treinta y seis","treinta y siete","treinta y ocho","treinta y nueve",
            "cuarenta","cuarenta y uno","cuarenta y dos","cuarenta y tres","cuarenta y cuatro","cuarenta y cinco","cuarenta y seis","cuarenta y siete","cuarenta y ocho","cuarenta y nueve",
            "cincuenta","cincuenta y uno","cincuenta y dos","cincuenta y tres","cincuenta y cuatro","cincuenta y cinco","cincuenta y seis","cincuenta y siete","cincuenta y ocho","cincuenta y nueve",
            "sesenta","sesenta y uno","sesenta y dos","sesenta y tres","sesenta y cuatro","sesenta y cinco","sesenta y seis","sesenta y siete","sesenta y ocho","sesenta y nueve",
            "setenta","setenta y uno","setenta y dos","setenta y tres","setenta y cuatro","setenta y cinco","setenta y seis","setenta y siete","setenta y ocho","setenta y nueve",
            "ochenta","ochenta y uno","ochenta y dos","ochenta y tres","ochenta y cuatro","ochenta y cinco","ochenta y seis","ochenta y siete","ochenta y ocho","ochenta y nueve",
            "noventa","noventa y uno","noventa y dos","noventa y tres","noventa y cuatro","noventa y cinco","noventa y seis","noventa y siete","noventa y ocho","noventa y nueve",
        };

        for (var i = 0; i < decenas.Length; i++)
        {
            var str = ToCardinal(30 + i, OpcionesGramatica.Masculino);

            Debug.Assert(str == decenas[i], str);
        }
    }

    [TestMethod]
    public void TestDecenasDesde40En10()
    {
        string[] decenas ={
            "cuarenta","cincuenta","sesenta","setenta","ochenta","noventa"
        };

        for (var i = 0; i < decenas.Length; i++)
        {
            var str = ToCardinal(40 + 10 * i, OpcionesGramatica.Masculino);

            Debug.Assert(str == decenas[i], str);
        }
    }

    [TestMethod]
    public void TestCentenas()
    {
        var str = ToCardinal(100, OpcionesGramatica.Masculino);

        Debug.Assert(str == "cien", str);

        str = ToCardinal(100, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "cien", str);

        str = ToCardinal(101, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "ciento una", str);

        str = ToCardinal(101, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento uno", str);

        str = ToCardinal(102, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento dos", str);

        str = ToCardinal(123, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento veintitrés", str);

        str = ToCardinal(153, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento cincuenta y tres", str);

        str = ToCardinal(200, OpcionesGramatica.Masculino);
        Debug.Assert(str == "doscientos", str);

        str = ToCardinal(200, OpcionesGramatica.Masculino);
        Debug.Assert(str == "doscientos", str);

        str = ToCardinal(210, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "doscientas diez", str);

        str = ToCardinal(700, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "setecientas", str);

        str = ToCardinal(900, OpcionesGramatica.Masculino);
        Debug.Assert(str == "novecientos", str);

        str = ToCardinal(999, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "novecientas noventa y nueve", str);
    }

    [TestMethod]
    public void TestMillares()
    {
        var millares = new Dictionary<int, string>
        {
            [1000] = "mil",
            [2000] = "dos mil",
            [3000] = "tres mil",
            [4000] = "cuatro mil",
            [5000] = "cinco mil",
            [6000] = "seis mil",
            [7000] = "siete mil",
            [8000] = "ocho mil",
            [9000] = "nueve mil",
            [10000] = "diez mil",
            [15000] = "quince mil",
            [20000] = "veinte mil",
            [25000] = "veinticinco mil",
            [30000] = "treinta mil",
            [35000] = "treinta y cinco mil",
            [40000] = "cuarenta mil",
            [50000] = "cincuenta mil",
            [60000] = "sesenta mil",
            [70000] = "setenta mil",
            [80000] = "ochenta mil",
            [90000] = "noventa mil",
            [100000] = "cien mil",
            [101000] = "ciento un mil",
            [108000] = "ciento ocho mil",
            [160000] = "ciento sesenta mil",
            [585000] = "quinientos ochenta y cinco mil",
            [999000] = "novecientos noventa y nueve mil"
        };
        var opciones = OpcionesGramatica.Masculino;

        foreach (var millar in millares)
        {
            var str = ToCardinal(millar.Key, opciones);

            Debug.Assert(str == millar.Value, str);
        }

        millares = new()
        {
            [585000] = "quinientas ochenta y cinco mil",
            [999000] = "novecientas noventa y nueve mil"
        };
        opciones = OpcionesGramatica.Fenemino;

        foreach (var millar in millares)
        {
            var str = ToCardinal(millar.Key, opciones);

            Debug.Assert(str == millar.Value, str);
        }
    }

    [TestMethod]
    public void TestCompareToHumanizer()
    {
        for (int i = 0; i < 999999; i++)
        {
            var strH = i.ToWords(GrammaticalGender.Masculine);
            var strMy = ToCardinal(i, OpcionesGramatica.Masculino);

            Debug.Assert(strH == strMy, $"{i} = H'{strH}' My'{strMy}'");

            strH = i.ToWords(GrammaticalGender.Feminine);
            strMy = ToCardinal(i, OpcionesGramatica.Fenemino);

            Debug.Assert(strH == strMy, $"Fem {i} = H'{strH}' My'{strMy}'");
        }
    }
}
