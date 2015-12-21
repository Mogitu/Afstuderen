using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmcCustomPrefab
{
    public class SpriteParser : IComponentParser
    {
        public bool ParseComponent(string component, ref Lexer lex, ref GameObject go)
        {
            SpriteRenderer spr = go.AddComponent<SpriteRenderer>();
            while (!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput)
            {
                string field = lex.GetToken();
                lex.NextToken();//equals symbol
                if (lex.Match("="))
                {
                    lex.NextToken();
                }
                else
                {
                    Debug.Log("Syntax Error: Expected `=` after field name");
                    lex.NextToken();//try to continue anyway?
                }
                switch (field.ToLower())
                {
                    case "color":
                        System.Object color = lex.GetObject();
                        Lexer.FinializeSpecialTypes(ref color, lex.GetTokenType());
                        Color col;
                        ColorUtility.TryParseHtmlString(((string)color).ToLower(), out col);
                        spr.color = col;
                        break;
                }
                lex.NextToken();
            }
            return true;
        }
    }


}
