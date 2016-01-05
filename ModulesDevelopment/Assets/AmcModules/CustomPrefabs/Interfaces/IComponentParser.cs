using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmcCustomPrefab
{
    /// <summary>
    /// Author  :   Maikel van Munsteren.
    /// Desc    :   Define funcionality needed to parse components.
    /// </summary>
    public interface IComponentParser
    {
        bool ParseComponent(string component, ref Lexer lex, ref GameObject go);
    }
}

