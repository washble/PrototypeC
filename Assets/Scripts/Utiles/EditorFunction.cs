using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public static class EditorFunction
{
#if UNITY_EDITOR
    public static bool IsDebugging { private get; set; } = false;
    
    public static string OpenFolderPanel(string title, string folder, string defaultName)
    {
        string absolutePath = EditorUtility.OpenFolderPanel(title, folder, defaultName);
        
        return absolutePath;
    }

    public static string SaveFolderPanel(string title, string folder, string defaultName)
    {
        string absolutePath = EditorUtility.SaveFolderPanel(title, folder, defaultName);

        return absolutePath;
    }
    
    public static string SaveFilePanelInProject(string title, string defaultName, string extension, string message)
    {
        string absolutePath = EditorUtility.SaveFilePanelInProject(title, defaultName, extension, message);

        return absolutePath;
    }
    
    public static string SaveFilePanelInProject(string title, string defaultName, string extension, string message, string path)
    {
        string absolutePath = EditorUtility.SaveFilePanelInProject(title, defaultName, extension, message, path);

        return absolutePath;
    }

    public static void SaveAsset<T>(T asset, string saveAssetPath) where T : Object
    {
        if (File.Exists(saveAssetPath))
        {
            T existingAsset = (T)AssetDatabase.LoadAssetAtPath(saveAssetPath, typeof(T));
            
            // Add more of the settings you want about existingAsset
        }
        else
        {
            AssetDatabase.CreateAsset(asset, saveAssetPath);
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static string AbsoluteToRelativePath(string absolutePath)
    {
        string relativePath = $"Assets{absolutePath.Substring(Application.dataPath.Length)}";

        return relativePath;
    }
    
    public static string CreateFolderIfNeeded(string parentPath, string newFolderName)
    {
        string createFolderPath = $"{parentPath}/{newFolderName}";
        if (!AssetDatabase.IsValidFolder(createFolderPath))
        {
            string guid = AssetDatabase.CreateFolder(parentPath, newFolderName);
            createFolderPath = AssetDatabase.GUIDToAssetPath(guid);
            return createFolderPath;
        }
			
        return createFolderPath;
    }
    
    public static string FindPrefabPath(GameObject gameObject)
    {
        string path;
        PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(gameObject);
        if (prefabStage != null)
        {
            path = prefabStage.assetPath;
        }
        else
        {
            path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
        }

        if (IsDebugging) { Debug.Log($"Prefab Path: {path}"); }

        return path;
    }
    
    public static string FindPrefabFolderPath(GameObject gameObject)
    {
        string path = FindPrefabPath(gameObject);
        string folderPath = string.Empty;
        if (!string.IsNullOrEmpty(path))
        {
            folderPath = Path.GetDirectoryName(path);
            folderPath = folderPath!.Replace("\\", "/");
            if (IsDebugging) { Debug.Log("Prefab Folder Path: " + folderPath); }
        }
        else
        {
            if (IsDebugging) { Debug.LogError("This GameObject is not part of a Prefab."); }
        }

        return folderPath;
    }

    public static string GetPrefabInstancePath(GameObject prefab)
    {
        return PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab);
    }

    public static T LoadAssetAtPath<T>(string path) where T : Object
    {
        return AssetDatabase.LoadAssetAtPath<T>(path);
    }

    public static string GetAssetPath<T>(T asset) where T : Object
    {
        return AssetDatabase.GetAssetPath(asset);
    }
    
    public static string GetDirectoryName(string path)
    {
        return Path.GetDirectoryName(path);
    }

    public static string GetFileNameWithoutExtension(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }

    public static Scene GetSceneAtBuildSettingNum(int sceneNum)
    {
        EditorBuildSettingsScene selectSceneSetting = EditorBuildSettings.scenes[sceneNum];
        Scene selectScene = EditorSceneManager.OpenScene(selectSceneSetting.path);

        return selectScene;
    }

    public static Scene GetSceneByPath(string scenePath)
    {
        Scene selectScene = SceneManager.GetSceneByPath(scenePath);

        return selectScene;
    }

    public static void MoveGameObjectToScene(GameObject gameObject, Scene scene)
    {
        SceneManager.MoveGameObjectToScene(gameObject, scene);
    }

    public static void ActiveSceneReloadAndSave()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneReloadAndSave(activeScene);
    }

    public static void PrefabGeneralSave(GameObject gameObject)
    {
        PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage != null)
        {
            GameObject prefab = prefabStage.prefabContentsRoot;
            PrefabUtility.SaveAsPrefabAsset(prefab, prefabStage.assetPath);
        }
        else
        {
            bool sceneDirty = EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            if (sceneDirty && PrefabUtility.IsPartOfPrefabInstance(gameObject))
            {
                PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);    
            }
            else
            {
                MarkSceneDirtyInScene(gameObject);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void PrefabRecordInScene(GameObject gameObject)
    {
        PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
    }

    public static void PrefabSaveInScene(GameObject gameObject, InteractionMode interactionMode)
    {
        // If not apply, try SceneReload Function
        PrefabUtility.ApplyPrefabInstance(gameObject, interactionMode);
    }
    
    private static void MarkSceneDirtyInScene(GameObject gameObject)
    {
        if (PrefabStageUtility.GetPrefabStage(gameObject))
        {
            EditorSceneManager.MarkSceneDirty(PrefabStageUtility.GetCurrentPrefabStage().scene);    
        }
    }

    public static void PrefabSaveInPrefabScene(GameObject gameObject)
    {
        PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        GameObject prefab = prefabStage.prefabContentsRoot;
        PrefabUtility.SaveAsPrefabAsset(prefab, prefabStage.assetPath);
    }

    public static void SceneReloadAndSave(Scene scene)
    {
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
    }

    public static void SceneReload(Scene scene)
    {
        EditorSceneManager.MarkSceneDirty(scene);
    }
    
#endif
}
