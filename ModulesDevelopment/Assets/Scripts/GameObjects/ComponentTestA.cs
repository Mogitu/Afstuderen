using UnityEngine;
using System.Collections;

public class ComponentTestA : AmcComponent
{
    [PrefabAttribute(RequiresDefinition = true)]
    public string title;

    [PrefabAttribute(RequiresDefinition = false)]  
    public string color;

    [PrefabAttribute(RequiresDefinition = true)]
    public Stuff stuff;

    public enum Stuff
    {
        Stuff1,
        Stuff2,
        Stuff3,
        Stuff4
    }
}
