using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor.Build.Reporting;
#endif

public class PreBuildConfig : ScriptableObject
{
#if UNITY_EDITOR

    [CustomEditor(typeof(PreBuildConfig))]
    internal class PreBuildConfigEditor : Editor
    {
        private PreBuildConfig preBuildConfig;
        private SerializedProperty buildConfigComponentsProperty;
        private Dictionary<string, bool> foldoutCheckDictionary = new Dictionary<string, bool>();
        private StringBuilder foldoutCheckStringBuilder = new StringBuilder();
        
        private void OnEnable()
        {
            preBuildConfig = target as PreBuildConfig;
            buildConfigComponentsProperty = serializedObject.FindProperty(nameof(preBuildConfig.buildConfigComponents));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ShowAndHidePropertyField("BuildConfigComponents", 
                buildConfigComponentsProperty, ref preBuildConfig.useBuildConfigComponents);
            ShowBuildComponent(ref buildConfigComponentsProperty);

            BuildConfigList();
            
            GUILayout.Space(5);
            GUILayout.Label("[Build Option]");
            preBuildConfig.buildMode = (BuildMode)EditorGUILayout.EnumPopup("Build Mode", preBuildConfig.buildMode);
            preBuildConfig.buildOptions = (BuildOptionParts)EditorGUILayout.EnumPopup("Build Option", preBuildConfig.buildOptions);
            
            GUILayout.Space(5);
            if (GUILayout.Button("Build"))
            {
                Build();
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void ShowAndHidePropertyField(string label,
            SerializedProperty property, ref bool checker, Action action = null)
        {
            checker = EditorGUILayout.Toggle($"[{label}]", checker);
            using EditorGUILayout.FadeGroupScope group 
                = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(checker));
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(property, new GUIContent($"{label}"));

                GUILayout.Space(5);

                if (action is not null)
                {
                    if (GUILayout.Button($"Set {label}"))
                    {
                        action.Invoke();
                    }
                    GUILayout.Space(5);
                }
                EditorGUI.indentLevel--;
            }
            GUILayout.Space(5);
        }
        
        private void ShowBuildComponent(ref SerializedProperty serializedProperty)
        {
            if (!preBuildConfig.useBuildConfigComponents) { return; }
            if (serializedProperty.arraySize > 0)
            {
                for (int i = 0; i < serializedProperty.arraySize; i++)
                {
                    SerializedProperty serializedPropertyElement = serializedProperty.GetArrayElementAtIndex(i);
                    Object objectReferenceValue = serializedPropertyElement.objectReferenceValue;
                    
                    if(objectReferenceValue is null) { continue; }
                    SerializedObject componentObject = new SerializedObject(objectReferenceValue);
                    SerializedProperty componentProperty = componentObject.GetIterator();
                    
                    EditorGUILayout.BeginHorizontal();
                    componentProperty.NextVisible(true);
                    EditorGUILayout.LabelField($"[{objectReferenceValue.name}]");
                    //EditorGUILayout.LabelField(objectReferenceValue.GetType().Name);
                    if (GUILayout.Button($"Apply"))
                    {
                        preBuildConfig.buildConfigComponents[i].Apply();
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    componentObject.Update();
                    while (componentProperty.NextVisible(false))
                    {
                        if (componentProperty.isArray)
                        {
                            foldoutCheckStringBuilder.Clear();
                            foldoutCheckStringBuilder.Append(objectReferenceValue.name).Append(componentProperty.name);
                            string key = foldoutCheckStringBuilder.ToString();
                            EditorGUI.indentLevel++;
                            if (foldoutCheckDictionary.ContainsKey(key))
                            {
                                bool foldout = foldoutCheckDictionary[key];
                                 foldoutCheckDictionary[key] 
                                     = foldout = EditorGUILayout.Foldout (foldout, $"{objectReferenceValue.name} (Count: {componentProperty.arraySize})", true);
                                if (foldout)
                                {
                                    EditorGUI.indentLevel++;
                                    EditorGUILayout.BeginVertical("box");
                                    for (int j = 0; j < componentProperty.arraySize; j++)
                                    {
                                        EditorGUILayout.PropertyField(
                                            componentProperty.GetArrayElementAtIndex(j), true);
                                    }
                                    EditorGUILayout.EndVertical();
                                    EditorGUI.indentLevel--;
                                }
                            }
                            else
                            {
                                EditorGUILayout.LabelField($"{objectReferenceValue.name}");
                                foldoutCheckDictionary.Add(key, false);
                            }
                            EditorGUI.indentLevel--;
                            continue; 
                        }
                        EditorGUILayout.PropertyField(componentProperty, true);
                    }
                    componentObject.ApplyModifiedProperties();
                    GUILayout.Space(15);
                }
            }
        }
        
        private void BuildConfigList()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("[Build Configs]");
            if (GUILayout.Button("+", GUILayout.Width(30)))
            {
                preBuildConfig.AddBuildConfigs();
                EditorUtility.SetDirty(preBuildConfig);
            }
            EditorGUILayout.EndHorizontal();
            
            for (int i = 0; i < preBuildConfig.buildConfigs.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                BuildComposition buildConfigs = preBuildConfig.buildConfigs[i];
                
                // Radio Button
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Toggle(preBuildConfig.selectedBuildConfigIndex == i, string.Empty, EditorStyles.radioButton))
                {
                    preBuildConfig.selectedBuildConfigIndex = i;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical();
                buildConfigs.configName = EditorGUILayout.TextField(preBuildConfig.buildConfigs[i].configName);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Product Name");
                buildConfigs.generalBuildSettings.productName = EditorGUILayout.TextField(buildConfigs.generalBuildSettings.productName);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Company Name");
                buildConfigs.generalBuildSettings.companyName = EditorGUILayout.TextField(buildConfigs.generalBuildSettings.companyName);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Version");
                buildConfigs.generalBuildSettings.version = EditorGUILayout.TextField(buildConfigs.generalBuildSettings.version);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();

                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    preBuildConfig.RemoveAtBuildConfigs(i--);
                    EditorUtility.SetDirty(preBuildConfig);
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(5);
            }
            
            if (GUI.changed)
            {
                EditorUtility.SetDirty(preBuildConfig);
                Repaint();
            }
        }

        private void AllBuildConfigComponentsApply()
        {
            if (!preBuildConfig.useBuildConfigComponents) { return; }
            
            PreBuildConfigComponent[] components = preBuildConfig.buildConfigComponents;
            for (int i = 0; i < components.Length; i++)
            {
                components[i].Apply();
            }
        }
        
        private void Build()
        {
            if(preBuildConfig.buildConfigs.Count <= 0) { return; }
            
            BuildPlayerOptions = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(new BuildPlayerOptions());
            BuildPlayerOptionsInit();
            
            AllBuildConfigComponentsApply();
            
            BuildComposition buildConfigs = preBuildConfig.buildConfigs[preBuildConfig.selectedBuildConfigIndex];
            GeneralBuildSettings generalBuildSettings = buildConfigs.generalBuildSettings;
            SetGeneralBuildSettings(generalBuildSettings.productName, 
                                    generalBuildSettings.companyName, 
                                    generalBuildSettings.version);
            
            SetBuildOptions(ref BuildPlayerOptions, ref preBuildConfig.buildOptions);
            SetDevelopmentMode(preBuildConfig.buildMode);

            BuildAndReport(in BuildPlayerOptions);
            //BuildPipeline.BuildPlayer(BuildPlayerOptions);
            //BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(buildPlayerOptions);
        }

        private void BuildPlayerOptionsInit()
        {
            BuildPlayerOptions.options = BuildOptions.None;
        }

        private void BuildAndReport(in BuildPlayerOptions buildPlayerOptions)
        {
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;
            
            if (summary.result == BuildResult.Succeeded)
            {
                string buildFolderPath = EditorFunction.GetDirectoryName(summary.outputPath);
                Application.OpenURL($"file://{buildFolderPath}");
                Debug.Log($"<color=green>[Build Success]</color> The build has been completed. Folder Path: {buildFolderPath}");
            }
            else
            {
                Debug.LogError($"<color=red>[Build Failed]</color> Build failed: {summary.result}");
            }
        }
    }

    [MenuItem("Assets/Create/PreBuildConfig/PreBuildConfig")]
    internal static void CreateAsset()
    {
        string path = EditorFunction.SaveFilePanelInProject("PreBuild Config", "PreBuildConfig", "asset", string.Empty);
        if (string.IsNullOrEmpty(path)) { return; }

        PreBuildConfig preBuildConfig = CreateInstance<PreBuildConfig>();
        AssetDatabase.CreateAsset(preBuildConfig, path);
    }

    public static BuildPlayerOptions BuildPlayerOptions;
    
    [HideInInspector][SerializeField] private int selectedBuildConfigIndex;
    
    [HideInInspector][SerializeField] private List<BuildComposition> buildConfigs;
    [Serializable]
    private class BuildComposition
    {
        [SerializeField] internal string configName;
        [SerializeField] internal GeneralBuildSettings generalBuildSettings;
    }
    [Serializable]
    private struct GeneralBuildSettings
    {
        [SerializeField] internal string productName;
        [SerializeField] internal string companyName;
        [SerializeField] internal string version;
    }
    
    [HideInInspector][SerializeField] private BuildMode buildMode = BuildMode.Debug;
    private enum BuildMode
    {
        Debug,
        Release,
        Test,
    }
    
    [HideInInspector][SerializeField] private BuildOptionParts buildOptions = BuildOptionParts.Build;
    private enum BuildOptionParts  // Use part of UnityEditor.BuildOptions
    {
        Build = 1,
        BuildAndRun = 4,
        CleanBuild = 128,
    }

    [SerializeField] private bool useBuildConfigComponents = false;
    [SerializeReference] private PreBuildConfigComponent[] buildConfigComponents;

    private void AddBuildConfigs()
    {
        BuildComposition newBuildComposition = new BuildComposition();
        (newBuildComposition.generalBuildSettings.productName, 
                newBuildComposition.generalBuildSettings.companyName, 
                newBuildComposition.generalBuildSettings.version) = GetGeneralBuildSettings();
        buildConfigs.Add(newBuildComposition);
    }
    
    private void RemoveAtBuildConfigs(int num)
    {
        buildConfigs.RemoveAt(num);

        if (selectedBuildConfigIndex >= num && selectedBuildConfigIndex > 0)
        {
            selectedBuildConfigIndex--;
        }
    }

    private static (string, string, string) GetGeneralBuildSettings()
    {
        string productName = PlayerSettings.productName;
        string companyName = PlayerSettings.companyName;
        string currentVersion = PlayerSettings.bundleVersion;

        return (productName, companyName, currentVersion);
    }

    private static void SetGeneralBuildSettings(
        string productName, string companyName, string version)
    {
        PlayerSettings.productName = productName;
        PlayerSettings.companyName = companyName;
        PlayerSettings.bundleVersion = version;
    }

    private static void SetDevelopmentMode(BuildMode buildMode)
    {
        bool isBuildRelease = buildMode == BuildMode.Release;
        EditorUserBuildSettings.development = !isBuildRelease;
        EditorUserBuildSettings.allowDebugging = !isBuildRelease;
    }

    private static void SetBuildOptions(ref BuildPlayerOptions buildPlayerOptions, ref BuildOptionParts buildOptions)
    {
        buildPlayerOptions.options |= (BuildOptions)buildOptions;
    }

    [InitializeOnLoadMethod]
    private static void OnEditorLoad()
    {
        BuildPlayerWindow.RegisterBuildPlayerHandler(BuildPlayerHandler);
    }

    private static void BuildPlayerHandler(BuildPlayerOptions options)
    {
        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
    }
    
#endif
}

[Serializable]
public abstract class PreBuildConfigComponent : ScriptableObject
{
#if UNITY_EDITOR
    /**
     * MenuItem Sample
     */
    protected const string DefaultName = "PreBuildComponent";
    
    [MenuItem("Assets/Create/PreBuildConfig/" + DefaultName, false)]
    internal static void CreateAsset()
    {
        string path = EditorFunction.SaveFilePanelInProject("BuildComponent", DefaultName, "asset", string.Empty);
        if (string.IsNullOrEmpty(path)) { return; }
    
        PreBuildConfigComponent configComponent = CreateInstance<PreBuildConfigComponent>();
        AssetDatabase.CreateAsset(configComponent, path);
    }

    internal virtual void Apply()
    {
        Debug.Log($"Anything Setting in {this.name} components");
    }
#endif
}
