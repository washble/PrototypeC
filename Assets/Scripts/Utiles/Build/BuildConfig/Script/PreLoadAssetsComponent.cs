using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PreLoadAssetsComponent : PreBuildConfigComponent
{
#if UNITY_EDITOR
    private new const string DefaultName = "PreLoadAssets";
    
    [MenuItem("Assets/Create/PreBuildConfig/PreBuildComponent/" + DefaultName)]
    internal new static void CreateAsset()
    {
        string path = EditorFunction.SaveFilePanelInProject("BuildComponent", DefaultName, "asset", string.Empty);
        if (string.IsNullOrEmpty(path)) { return; }

        PreLoadAssetsComponent configComponent = CreateInstance<PreLoadAssetsComponent>();
        AssetDatabase.CreateAsset(configComponent, path);
    }

    [SerializeField] private Object[] preLoadAssets;
    
    internal override void Apply()
    {
        SetPreLoadAssets();
    }
    
    private void SetPreLoadAssets()
    {
        if(preLoadAssets.Length <= 0) { return; }
        
        PlayerSettings.SetPreloadedAssets(preLoadAssets);
    }
    
    private Object[] GetPreLoadAssets()
    {
        return PlayerSettings.GetPreloadedAssets();
    }
#endif
}
