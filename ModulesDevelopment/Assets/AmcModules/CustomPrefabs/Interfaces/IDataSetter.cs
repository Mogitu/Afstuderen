﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AmcCustomPrefab
{
    public interface IDataSetter
    {
        bool SetData(ref Lexer lex);
    }
}



