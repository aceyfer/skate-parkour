using UnityEngine;
using UnityEditor;
using System.IO;

public class ModelSetupUtility : EditorWindow
{
    [MenuItem("Tools/Setup Player Character")]
    public static void SetupPlayer()
    {
        string modelPath = "Assets/Models/PlayerCharacter_Assets/selected.glb";
        string texturePath = "Assets/Textures/Player_Texture_BlueBlack.png";
        string materialPath = "Assets/Materials/PlayerCharacter_Mat.mat";

        Debug.Log("--- Starting Player Character Setup ---");

        // 1. Configure Model Importer
        AssetImporter importer = AssetImporter.GetAtPath(modelPath);
        if (importer == null)
        {
            Debug.LogError($"Could not find asset at {modelPath}");
        }
        else
        {
            if (importer is ModelImporter modelImporter)
            {
                modelImporter.animationType = ModelImporterAnimationType.Human;
                modelImporter.avatarSetup = ModelImporterAvatarSetup.CreateFromThisModel;
                modelImporter.SaveAndReimport();
                Debug.Log($"Successfully set {modelPath} to Humanoid and triggered Avatar creation.");
            }
            else
            {
                Debug.LogWarning($"Importer for {modelPath} is NOT a ModelImporter. Importer type: {importer.GetType().FullName}");
                Debug.Log("Note: Humanoid animation type can only be set on assets using ModelImporter.");
            }
        }

        // 2. Create Material and Assign Texture
        if (!Directory.Exists("Assets/Materials"))
        {
            Directory.CreateDirectory("Assets/Materials");
            AssetDatabase.Refresh();
        }

        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
        if (texture == null)
        {
            Debug.LogError($"Could not find texture at {texturePath}");
        }

        Material mat = new Material(Shader.Find("Standard"));
        if (texture != null)
        {
            mat.mainTexture = texture;
        }
        
        // Reasonable Standard shader properties
        mat.color = Color.white; // Ensure texture color is unaffected
        mat.SetFloat("_Glossiness", 0.4f); // Slightly matte
        mat.SetFloat("_Metallic", 0.1f);   // Some slight metallic sheen for apparel
        
        AssetDatabase.CreateAsset(mat, materialPath);
        AssetDatabase.SaveAssets();
        Debug.Log($"Created material at {materialPath} and assigned texture {texturePath}.");
        
        Debug.Log("--- Player Character Setup Complete ---");
    }
}
