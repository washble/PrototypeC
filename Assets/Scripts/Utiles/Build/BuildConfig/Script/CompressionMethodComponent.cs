using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CompressionMethodComponent : PreBuildConfigComponent
{
#if UNITY_EDITOR
    private new const string DefaultName = "CompressionMethod"; 
    
    [MenuItem("Assets/Create/PreBuildConfig/PreBuildComponent/" + DefaultName)]
    internal new static void CreateAsset()
    {
        string path = EditorFunction.SaveFilePanelInProject("BuildComponent", DefaultName, "asset", string.Empty);
        if (string.IsNullOrEmpty(path)) { return; }

        CompressionMethodComponent configComponent = CreateInstance<CompressionMethodComponent>();
        AssetDatabase.CreateAsset(configComponent, path);
    }

    [SerializeField] private CompressionType compressionType;

    internal override void Apply()
    {
        SetCompressionMethod();
    }
    
    private void SetCompressionMethod()
    {
        BuildPlayerOptions buildPlayerOptions = PreBuildConfig.BuildPlayerOptions;
        
        switch (compressionType)
        {
            case CompressionType.None:
                break;
            case CompressionType.Lz4:
                buildPlayerOptions.options |= BuildOptions.CompressWithLz4;
                break;
            case CompressionType.Lzma:
                break;
            case CompressionType.Lz4HC:
                buildPlayerOptions.options |= BuildOptions.CompressWithLz4HC;
                break;
        }
    }
#endif
}
