using UnityEngine;
using System.Collections;
using UnityEditor;

public class BundleCreater : Editor
{
    [MenuItem("Tools/SetAssetBundleName")]
    public static void CreatBundle()
    {
        string path = Application.streamingAssetsPath;
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows);
    }

}
