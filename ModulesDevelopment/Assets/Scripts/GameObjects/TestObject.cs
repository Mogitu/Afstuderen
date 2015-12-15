using UnityEngine;
using System.Collections;

public class TestObject:AmcComponent   {
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
