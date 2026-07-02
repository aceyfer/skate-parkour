#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class MergeDropSequence
{
    private const string BaseSource   = "Assets/_Recovery/0.unity";
    private const string DropSource   = "Assets/_Recovery/0 (1).unity";
    private const string TargetScene  = "Assets/Scenes/Level_01_WithDrop.unity";

    private static readonly string[] ObjectsToMove = { "Level2_Downwards", "DropLaunch", "Portal_L2" };

    [MenuItem("SkateParkour/Merge Drop Sequence into Level_01_WithDrop")]
    public static void Run()
    {
        // --- Step 1: Copy 0.unity → Level_01_WithDrop.unity (new GUID, _Recovery untouched) ---
        if (!File.Exists(TargetScene))
        {
            if (!AssetDatabase.CopyAsset(BaseSource, TargetScene))
            {
                Debug.LogError("[MergeDropSequence] CopyAsset failed: " + BaseSource + " → " + TargetScene);
                return;
            }
            AssetDatabase.Refresh();
            Debug.Log("[MergeDropSequence] Created " + TargetScene + " (copy of " + BaseSource + ").");
        }
        else
        {
            Debug.Log("[MergeDropSequence] " + TargetScene + " already exists — opening existing file.");
        }

        // --- Step 2: Open Level_01_WithDrop as the single active scene ---
        Scene mainScene = EditorSceneManager.OpenScene(TargetScene, OpenSceneMode.Single);
        if (!mainScene.IsValid())
        {
            Debug.LogError("[MergeDropSequence] Failed to open " + TargetScene);
            return;
        }
        Debug.Log("[MergeDropSequence] Opened main scene: " + mainScene.name);

        // --- Step 3: Open 0 (1).unity additively (read intent — will NOT be saved) ---
        Scene dropScene = EditorSceneManager.OpenScene(DropSource, OpenSceneMode.Additive);
        if (!dropScene.IsValid())
        {
            Debug.LogError("[MergeDropSequence] Failed to open additive scene: " + DropSource);
            return;
        }
        Debug.Log("[MergeDropSequence] Opened additive scene: " + dropScene.name);

        // --- Step 4: Move target objects from drop scene into main scene ---
        int moved = 0;
        foreach (string objName in ObjectsToMove)
        {
            GameObject found = FindInScene(dropScene, objName);
            if (found != null)
            {
                found.transform.SetParent(null, true);
                SceneManager.MoveGameObjectToScene(found, mainScene);
                Debug.Log("[MergeDropSequence] Moved '" + objName + "' → " + mainScene.name);
                moved++;
            }
            else
            {
                Debug.LogWarning("[MergeDropSequence] '" + objName + "' not found in drop scene — skipped.");
            }
        }

        // --- Step 5: Close drop scene WITHOUT saving ---
        EditorSceneManager.CloseScene(dropScene, false);
        Debug.Log("[MergeDropSequence] Closed additive scene (unsaved). _Recovery/0 (1).unity unchanged.");

        // --- Step 6: Save Level_01_WithDrop.unity ---
        if (moved > 0)
            EditorSceneManager.MarkSceneDirty(mainScene);

        bool saved = EditorSceneManager.SaveScene(mainScene, TargetScene);
        if (saved)
            Debug.Log("[MergeDropSequence] DONE. Saved " + TargetScene + " (" + moved + " objects merged).");
        else
            Debug.LogError("[MergeDropSequence] SaveScene returned false — check for Unity errors above.");
    }

    private static GameObject FindInScene(Scene scene, string name)
    {
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            if (root.name == name) return root;
            Transform child = root.transform.Find(name);
            if (child != null) return child.gameObject;
        }
        return null;
    }
}
#endif
