using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IComponentParser
{
    bool ParseComponent(string component, ref Lexer lex, ref GameObject go);
}
