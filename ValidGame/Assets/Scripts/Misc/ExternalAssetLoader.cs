using UnityEngine;
using System.Collections.Generic;

public class ExternalAssetLoader
{
    private string AssetPath;
    private Dictionary<string, AssetBundle> AssetBundles;
    private bool LoadSuccess;

    public ExternalAssetLoader()
    {
        AssetBundles = new Dictionary<string, AssetBundle>();
    }

    public void LoadBundle(string path, string lookupName)
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(path);
        AssetBundles.Add(lookupName, bundle);
    }

    public GameObject LoadObject(string bundleName, string objectName)
    {
        return AssetBundles[bundleName].LoadAsset(objectName) as GameObject;
    }
}
