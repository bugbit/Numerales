// Copyright © 2023 Oscar Hernandez Baño. All rights reserved.
// Use of this source code is governed by a GLP3.0 license that can be found in the LICENSE file.
// This file is part of Algebra.

using System.Text;

namespace Numerales;

public class Words
{
    private StringBuilder sb = new StringBuilder();

    public void AddCaracters(string cars) => sb.Append(cars);

    public void AddWord(string word)
    {
        if (sb.Length > 0)
        {
            if (sb[sb.Length - 1] != ' ')
                sb.Append(' ');
        }
        AddCaracters(word);
    }    

    public override string ToString() => sb.ToString();
}

