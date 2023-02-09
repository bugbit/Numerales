// Copyright � 2023 Oscar Hernandez Ba�o. All rights reserved.
// Use of this source code is governed by a GLP3.0 license that can be found in the LICENSE file.
// This file is part of Algebra.

using System;

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
        "trece", "catorce", "quince", "diecis�is", "diecisiete", "dieciocho", "diecinueve", "veinte", "veintiuno",
        "veintid�s", "veintitr�s", "veinticuatro", "veinticinco", "veintis�is", "veintisiete", "veintiocho", "veintinueve"
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

        Debug.Assert(str == "cien");

        str = ToCardinal(100, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "cien");

        str = ToCardinal(101, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "ciento una");

        str = ToCardinal(101, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento uno");

        str = ToCardinal(102, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento dos");

        str = ToCardinal(123, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento veintitrés");

        str = ToCardinal(153, OpcionesGramatica.Masculino);
        Debug.Assert(str == "ciento cincuenta y tres");

        str = ToCardinal(200, OpcionesGramatica.Masculino);
        Debug.Assert(str == "doscientos");

        str = ToCardinal(200, OpcionesGramatica.Masculino);
        Debug.Assert(str == "doscientos");

        str = ToCardinal(999, OpcionesGramatica.Fenemino);
        Debug.Assert(str == "novecientas noventa y nueve");
    }
}
