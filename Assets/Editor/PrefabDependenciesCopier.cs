using UnityEditor;
using UnityEngine;
using System.IO;

public class PrefabDependenciesCopier : MonoBehaviour
{
    [MenuItem("Assets/Copy Prefab and Dependencies")]
    static void CopyPrefabAndDependencies()
    {
        // Get selected prefab
        Object selectedObject = Selection.activeObject;
        string prefabPath = AssetDatabase.GetAssetPath(selectedObject);

        if (string.IsNullOrEmpty(prefabPath) || !prefabPath.EndsWith(".prefab"))
        {
            Debug.LogError("Please select a prefab.");
            return;
        }

        // Get dependencies
        string[] dependencies = AssetDatabase.GetDependencies(prefabPath);

        // Create a new directory for the copied prefab and dependencies
        string copyDir = "Assets/CopiedPrefab";
        if (!Directory.Exists(copyDir))
        {
            Directory.CreateDirectory(copyDir);
        }

        // Copy prefab and dependencies
        foreach (string dependency in dependencies)
        {
            string destPath = Path.Combine(copyDir, Path.GetFileName(dependency));
            AssetDatabase.CopyAsset(dependency, destPath);
        }

        AssetDatabase.Refresh();
        Debug.Log("Prefab and dependencies copied to " + copyDir);
    }
}
