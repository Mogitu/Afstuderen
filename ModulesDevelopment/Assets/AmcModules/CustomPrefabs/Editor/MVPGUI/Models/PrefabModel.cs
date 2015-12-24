using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace AmcCustomPrefab
{
    public class PrefabModel : IPrefabModel
    {
        //script stuff
        private string dataString="";
        private AmcCustomPrefab customPrefab;

        //select stuff
        private List<SerializedProperty> requiredProperties;
        private List<SerializedProperty> optionalProperties;
        private MonoScript script;

        private AmcComponent tmpComp;
        private GameObject tmpGo;

        public PrefabModel()
        {
            
        }

        public string DataString
        {
            get
            {
                return dataString;
            }

            set
            {
                dataString = value;
            }
        }

        public AmcCustomPrefab CustomPrefab
        {
            get
            {
                return customPrefab;
            }

            set
            {
                customPrefab = value;
            }
        }

        public List<SerializedProperty> RequiredProperties
        {
            get
            {
                return requiredProperties;
            }

            set
            {
                requiredProperties = value;
            }
        }

        public List<SerializedProperty> OptionalProperties
        {
            get
            {
                return optionalProperties;
            }

            set
            {
                optionalProperties = value;
            }
        }

        public MonoScript Script
        {
            get
            {
                return script;
            }

            set
            {
                script = value;
            }
        }

        public AmcComponent TmpComp
        {
            get
            {
                return tmpComp;
            }

            set
            {
                tmpComp = value;
            }
        }

        public GameObject TmpGo
        {
            get
            {
                return tmpGo;
            }

            set
            {
                tmpGo = value;
            }
        }
    }
}
