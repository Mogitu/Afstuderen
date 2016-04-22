using UnityEngine;
using System.Collections.Generic;

public class ExternalAssetLoader{

    private string AssetPath;
    private Dictionary<string, AssetBundle> AssetBundles;
    private bool LoadSuccess;

	public ExternalAssetLoader()
    {
        AssetBundles = new Dictionary<string, AssetBundle>();
    }

    public bool LoadBundle(string path, string lookupName)
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(path);       
        if (bundle != null)
        {           
            AssetBundles.Add(lookupName, bundle);
            return true;
        }
        return false;
    }

    public GameObject LoadObject(string bundleName, string objectName)
    {
        return AssetBundles[bundleName].LoadAsset(objectName) as GameObject;
    }
}
