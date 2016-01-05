using UnityEngine;
using System.Collections;
using AmcCustomPrefab;

public class CardComponent : AmcComponent {

    [PrefabAttribute(RequiresDefinition = true)]
    public string fileName;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    void Awake()
    {       
       meshRenderer = gameObject.AddComponent<MeshRenderer>();
       meshFilter = gameObject.AddComponent<MeshFilter>();
       meshFilter.mesh = Resources.Load<Mesh>(fileName);        
     
    }
}
