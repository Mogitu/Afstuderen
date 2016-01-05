using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmcCustomPrefab
{
    /// <summary>
    /// Author  :   Maikel van Munsteren
    /// Desc    :   Parses non-default unity3d components.
    /// </summary>
    public class CustomComponentParser : IComponentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="lex"></param>
        /// <param name="go"></param>
        /// <returns></returns>
        public bool ParseComponent(string componentName, ref Lexer lex, ref GameObject go)
        {
            //AmcComponent customComponent = UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(go, "Assets/Scripts/Tools/AdvCustomPrefab.cs (223,34)", componentName) as AmcComponent;
            AmcComponent customComponent = go.AddComponent(System.Type.GetType(componentName)) as AmcComponent;
            bool setDataSuccess = false;
            if (customComponent != null)
            {
                setDataSuccess = customComponent.SetData(ref lex);
            }
            else
            {
                Debug.Log("Component " + componentName + " couldn't be added! Ensure it exists or isn't being added twice.");
            }
            return setDataSuccess;
        }
    }
}


