using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ScriptingDefineSymbolsComponent : PreBuildConfigComponent
{
#if UNITY_EDITOR
    private new const string DefaultName = "ScriptingDefineSymbols"; 
    
    [MenuItem("Assets/Create/PreBuildConfig/PreBuildComponent/" + DefaultName)]
    internal new static void CreateAsset()
    {
        string path = EditorFunction.SaveFilePanelInProject("BuildComponent", DefaultName, "asset", string.Empty);
        if (string.IsNullOrEmpty(path)) { return; }

        ScriptingDefineSymbolsComponent configComponent = CreateInstance<ScriptingDefineSymbolsComponent>();
        AssetDatabase.CreateAsset(configComponent, path);
    }

    [SerializeField] private DefineSymbol[] defineSymbols;
    [Serializable]
    private struct DefineSymbol
    {
        [HideInInspector][SerializeField] public string elementName;
        [SerializeField] public string symbol;
        [SerializeField] public SymbolType type;
    }
    private enum SymbolType
    {
        Add,
        Sub
    }
    private const string semicolon = ";";
    
    private void OnValidate()
    {
        for (int i = 0; i < defineSymbols.Length; i++)
        {
            defineSymbols[i].elementName = $"Symbol {i + 1}";
        }
    }

    internal override void Apply()
    {
        ApplySymbols();
    }
    
    private void ApplySymbols()
    {
        if(defineSymbols.Length <= 0) { return; }
        
        StringBuilder symbolsBuilder = new StringBuilder();
        StringBuilder logBuilder = new StringBuilder();

        BuildTargetGroup buildTargetGroup 
            = !PreBuildConfig.BuildPlayerOptions.Equals(default(BuildPlayerOptions))
            ? PreBuildConfig.BuildPlayerOptions.targetGroup
            : BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
        string currentSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        
        //string[] splitSymbols = currentSymbols.Split(new[] { semicolon }, System.StringSplitOptions.RemoveEmptyEntries);
        string[] splitSymbols = currentSymbols.Split(semicolon, System.StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> symbolSet = new HashSet<string>(splitSymbols);

        for (int i = 0; i < defineSymbols.Length; i++)
        {
            string symbol = defineSymbols[i].symbol;
            switch (defineSymbols[i].type)
            {
                case SymbolType.Add:
                    if (!symbolSet.Contains(symbol))
                    {
                        symbolsBuilder.Append(symbol).Append(semicolon);
                        symbolSet.Add(symbol);
                        logBuilder.AppendLine($"Add define symbol: {symbol}");
                    }
                    break;
                case SymbolType.Sub:
                    if (symbolSet.Remove(symbol))
                    {
                        logBuilder.AppendLine($"Removed define symbol: {symbol}");
                    }
                    if (currentSymbols.Contains(symbol))
                    {
                        currentSymbols = currentSymbols.Replace(symbol + semicolon, string.Empty).Replace(semicolon + symbol, string.Empty);
                        if (currentSymbols.StartsWith(semicolon))
                        {
                            currentSymbols = currentSymbols.Substring(semicolon.Length);
                        }
                        logBuilder.AppendLine($"Removed define symbol: {symbol}");
                    }
                    break;
            }
        }
        string finalSymbols 
            = symbolsBuilder.Length > 0 ? currentSymbols + semicolon + symbolsBuilder : currentSymbols;
        HashSet<string> finalSymbolSet = new HashSet<string>(finalSymbols.Split(semicolon, System.StringSplitOptions.RemoveEmptyEntries));
        string cleanedFinalSymbols = string.Join(semicolon, finalSymbolSet) + semicolon;
        PlayerSettings.SetScriptingDefineSymbolsForGroup
        (
            buildTargetGroup, cleanedFinalSymbols.TrimEnd(semicolon.ToCharArray())
        );
        
        if (logBuilder.Length > 0)
        {
            Debug.Log(logBuilder.ToString());
        }
    }
#endif
}
