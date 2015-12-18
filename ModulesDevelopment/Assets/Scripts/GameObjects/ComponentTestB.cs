using UnityEngine;
using System.Collections;

public class ComponentTestB:AmcComponent   {
   [PrefabAttribute(RequiresDefinition=true)]
   public string test;

   [PrefabAttribute(RequiresDefinition=false)]
   public int count;

   void Start()
   {
       Debug.Log("start");
   }

   void Update()
   {
       Debug.Log("Update");
   }
}
