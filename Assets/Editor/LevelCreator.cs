using System;
using System.Collections.Generic;
using System.Linq;
using Levels;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelCreator : EditorWindow
{
    private const string LEVEL_CREATE_NEW_LEVEL_NAME = "level-create-new-level-name";
    private const string LEVEL_CREATE_NEW_LEVEL_DESCRIPTION = "level-create-new-level-description";
    private const string LEVEL_CREATE_NEW_LEVEL_POINTS = "level-create-new-level-points";
    private const string LEVEL_CREATE_BUTTON = "level-create-button";

    public SceneAsset sceneAssetPrefabForNewLevels;
    public Coin coinPrefab;
    
    private const string NEW_LEVEL_SO_PATH = "Assets/Resources/Levels/";
    private const string NEW_LEVEL_SCENE_PATH = "Assets/Scenes/Levels/";

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    
    private TextField _newLevelNameTextField;
    private TextField _newLevelDescriptionTextField;
    private IntegerField _newLevelPointsToEarnIntegerField;
    private Button _createNewLevelButton;

    private VisualElement root;
    private List<LevelData> levelDatas;

    [MenuItem("Window/UI Toolkit/LevelCreator")]
    public static void ShowExample()
    {
        LevelCreator wnd = GetWindow<LevelCreator>();
        wnd.titleContent = new GUIContent("LevelCreator");
    }

    public void CreateGUI()
    {
        levelDatas = Resources.LoadAll<LevelData>("Levels").ToList();
        // Each editor window contains a root VisualElement object
        root = rootVisualElement;

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        
        InitLevelCreationSection(root);
        
        DropdownField dropdownField = root.Q<DropdownField>("level-select-dropdown");
        dropdownField.choices = levelDatas.Select(data => data.name).ToList();
        dropdownField.RegisterValueChangedCallback(Callback);
    }
    private void Callback(ChangeEvent<string> p_evt) => SetupLevelToEdit(p_evt.newValue);

    private LevelData selectedLevelData;
    
    private void SetupLevelToEdit(string levelDataName)
    {
        selectedLevelData = levelDatas.FirstOrDefault(data => data.name == levelDataName);
        VisualElement visualElement = root.Q<VisualElement>("so-editor");
        if (selectedLevelData == null) return;
        
        visualElement.Clear();
        LevelDataCuystomEditor editor = Editor.CreateEditor(selectedLevelData) as LevelDataCuystomEditor;
        editor.ClearSubscribers();
        VisualElement scriptableInspector = editor.CreateInspectorGUI();
        visualElement.Add(scriptableInspector);
        List<VisualElement> visualElements = scriptableInspector.Children().ToList();
  

        if (CheckLevelSceneName(levelDataName, visualElement))
            return;

        if (CheckLevelAssetIfExists(levelDataName, visualElement))
            return;

        if (CheckLevelAssetIsInBuildSettings(levelDataName, visualElement))
            return;
        
        //Check Coins
        Scene scene = EditorSceneManager.OpenScene(NEW_LEVEL_SCENE_PATH + selectedLevelData.SceneName + ".unity");
        GameObject[] rootGameObjects = scene.GetRootGameObjects();
        int coinCount = 0;
        foreach (GameObject rootGameObject in rootGameObjects)
        {
            coinCount += rootGameObject.GetComponentsInChildren<Coin>().Length;
        }
        if(coinCount < selectedLevelData.PointsToEarn)
            visualElement.Add(CreateErrorWithFixButton($"Not enough coins in scene! {selectedLevelData.PointsToEarn-coinCount} missing", "Add Coins To Scene", () =>
            {
                Scene newScene = EditorSceneManager.OpenScene(NEW_LEVEL_SCENE_PATH + selectedLevelData.SceneName + ".unity");
                for (int i = 0; i < selectedLevelData.PointsToEarn-coinCount; i++)
                {
                    PrefabUtility.InstantiatePrefab(coinPrefab);
                }
                EditorSceneManager.SaveScene(newScene);
                SetupLevelToEdit(levelDataName);
            }));

        VisualElement UpdateWarnings = new VisualElement();
        visualElement.Add(UpdateWarnings);
        
        editor.ValueChanged += () =>
        {
            UpdateWarnings.Clear();
            UpdateWarnings.Add(CreateErrorWithFixButton("Value of SO Changed", "Reload",
                    () => { SetupLevelToEdit(levelDataName); }));
        };

    }

    private bool CheckLevelAssetIsInBuildSettings(string levelDataName, VisualElement visualElement)
    {
        if (EditorBuildSettings.scenes.Any(x => x.path.Contains(selectedLevelData.SceneName + ".unity") && x.enabled) ==
            false)
        {
            visualElement.Add(CreateErrorWithFixButton("Scene is not in build settings!", "Fix", () =>
            {
                EditorBuildSettingsScene newScene =
                    new EditorBuildSettingsScene(NEW_LEVEL_SCENE_PATH + selectedLevelData.SceneName + ".unity", true);
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                EditorBuildSettings.scenes = scenes.Append(newScene).ToArray();
                SetupLevelToEdit(levelDataName);
            }));
            return true;
        }

        return false;
    }

    private bool CheckLevelAssetIfExists(string levelDataName, VisualElement visualElement)
    {
        if (AssetDatabase.LoadAssetAtPath<SceneAsset>(NEW_LEVEL_SCENE_PATH + selectedLevelData.SceneName + ".unity") ==
            null)
        {
            visualElement.Add(CreateErrorWithFixButton("Scene is not in Assets!", "Fix", () =>
            {
                EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(sceneAssetPrefabForNewLevels));
                Scene scene = EditorSceneManager.GetActiveScene();
                string newLevelScenePath = NEW_LEVEL_SCENE_PATH + selectedLevelData.SceneName + ".unity";
                EditorSceneManager.SaveScene(scene, newLevelScenePath, true);
                EditorSceneManager.OpenScene(newLevelScenePath);
                SetupLevelToEdit(levelDataName);
            }));
            return true;
        }
        return false;
    }

    private bool CheckLevelSceneName(string levelDataName, VisualElement visualElement)
    {
        if (selectedLevelData.SceneName != selectedLevelData.name)
        {
            visualElement.Add(CreateErrorWithFixButton("SceneName and ScriptableObject name must be the same!", "Fix", () =>
            {
                selectedLevelData.SceneName = selectedLevelData.name;
                EditorUtility.SetDirty(selectedLevelData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                SetupLevelToEdit(levelDataName);
            }));
            return true;
        }

        return false;
    }

    private VisualElement CreateErrorWithFixButton(string labelText, string buttonText, Action buttonAction)
    {
        VisualElement nameValidation = new VisualElement();
        nameValidation.style.flexDirection = FlexDirection.Row;
        Label element = new Label();
        element.text =  labelText;
        element.style.color = Color.red;
        Button fixButton = new Button(buttonAction);
        fixButton.text = buttonText;
        nameValidation.Add(element);
        nameValidation.Add(fixButton);
        return nameValidation;
    }

    private void InitLevelCreationSection(VisualElement root)
    {
        _newLevelNameTextField = root.Q<TextField>(LEVEL_CREATE_NEW_LEVEL_NAME);
        _newLevelDescriptionTextField = root.Q<TextField>(LEVEL_CREATE_NEW_LEVEL_DESCRIPTION);
        _newLevelPointsToEarnIntegerField = root.Q<IntegerField>(LEVEL_CREATE_NEW_LEVEL_POINTS);
        _createNewLevelButton = root.Q<Button>(LEVEL_CREATE_BUTTON);
        _createNewLevelButton.clicked += CreateNewLevel;
    }

    private void CreateNewLevel()
    {
        //Creation of the scene Copy
        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(sceneAssetPrefabForNewLevels));
        Scene scene = EditorSceneManager.GetActiveScene();
        string newLevelScenePath = NEW_LEVEL_SCENE_PATH + _newLevelNameTextField.text + ".unity";
        EditorSceneManager.SaveScene(scene, newLevelScenePath, true);
        EditorSceneManager.OpenScene(newLevelScenePath);
        
        //ADD NEW SCENE TO BUILD SETTINGS
        EditorBuildSettingsScene newScene = new EditorBuildSettingsScene(newLevelScenePath, true);
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        EditorBuildSettings.scenes = scenes.Append(newScene).ToArray();
        
        //Creation of the LevelData ScriptableObject
        LevelData newLevelData = ScriptableObject.CreateInstance<LevelData>();
        newLevelData.SceneName = _newLevelNameTextField.text;
        newLevelData.Description = _newLevelDescriptionTextField.value;
        newLevelData.PointsToEarn = _newLevelPointsToEarnIntegerField.value;
        AssetDatabase.CreateAsset(newLevelData, NEW_LEVEL_SO_PATH + newLevelData.SceneName + ".asset");
    }
}
