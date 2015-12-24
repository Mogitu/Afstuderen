using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace AmcCustomPrefab
{
    public interface IPrefabModel
    {
        //script stuff
        AmcCustomPrefab CustomPrefab { get; set; }
        string DataString { get; set; }

        //select stuff
        MonoScript Script { get; set; }
        AmcComponent TmpComp { get; set; }
        GameObject TmpGo { get; set; }
        List<SerializedProperty> RequiredProperties { get; set; }
        List<SerializedProperty> OptionalProperties { get; set; }
    }
}
