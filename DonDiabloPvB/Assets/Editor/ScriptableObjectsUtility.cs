using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectsUtility
{
    private const string PATH = "Assets/Resources/";
    private const string ASSET_EXTENSION = ".asset";

    /// <summary>
    //  This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static void CreateAsset<T>(string name) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == string.Empty)
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != string.Empty)
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), string.Empty);
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(PATH + "/" + name + ASSET_EXTENSION);

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}