using System.Linq;
using Levels;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.SceneManagement;
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
    
    private const string NEW_LEVEL_SO_PATH = "Assets/Resources/Levels/";
    private const string NEW_LEVEL_SCENE_PATH = "Assets/Scenes/Levels/";

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    
    private TextField _newLevelNameTextField;
    private TextField _newLevelDescriptionTextField;
    private IntegerField _newLevelPointsToEarnIntegerField;
    private Button _createNewLevelButton;

    [MenuItem("Window/UI Toolkit/LevelCreator")]
    public static void ShowExample()
    {
        LevelCreator wnd = GetWindow<LevelCreator>();
        wnd.titleContent = new GUIContent("LevelCreator");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        
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
