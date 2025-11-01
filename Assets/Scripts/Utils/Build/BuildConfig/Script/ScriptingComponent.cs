using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ScriptingComponent : PreBuildConfigComponent
{
#if UNITY_EDITOR
    private new const string DefaultName = "Scripting"; 
    
    [MenuItem("Assets/Create/PreBuildConfig/PreBuildComponent/" + DefaultName)]
    internal new static void CreateAsset()
    {
        string path = EditorFunction.SaveFilePanelInProject("BuildComponent", DefaultName, "asset", string.Empty);
        if (string.IsNullOrEmpty(path)) { return; }

        ScriptingComponent configComponent = CreateInstance<ScriptingComponent>();
        AssetDatabase.CreateAsset(configComponent, path);
    }

    [SerializeField] private ScriptingImplementation scriptingImplementation;
    [SerializeField] private ManagedStrippingLevel managedStrippingLevel;
    [SerializeField] private bool useGCIncremental = true;

    internal override void Apply()
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        switch (scriptingImplementation)
        {
            case ScriptingImplementation.Mono2x:
                SetScriptingMono(buildTargetGroup);
                break;
            case ScriptingImplementation.IL2CPP:
                SetScriptingIL2CPP(buildTargetGroup);
                break;
        }
        SetManagedScriptLevel(buildTargetGroup, managedStrippingLevel);
        SetGCIncremental(useGCIncremental);
    }
    
    private void SetScriptingMono(BuildTargetGroup buildTargetGroup)
    {
        PlayerSettings.SetScriptingBackend(buildTargetGroup, ScriptingImplementation.Mono2x);   
    }
    
    private void SetScriptingIL2CPP(BuildTargetGroup buildTargetGroup)
    {
        PlayerSettings.SetScriptingBackend(buildTargetGroup, ScriptingImplementation.IL2CPP);
    }
    
    private void SetManagedScriptLevel(BuildTargetGroup buildTargetGroup, ManagedStrippingLevel strippingLevel)
    {
        PlayerSettings.SetManagedStrippingLevel(buildTargetGroup, strippingLevel);
    }
    
    private void SetGCIncremental(bool use)
    {
        PlayerSettings.gcIncremental = use;
    }
#endif
}
