using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelCreator : EditorWindow
{
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
        
        _newLevelNameTextField = root.Q<TextField>("level-create-new-level-name");
        _newLevelDescriptionTextField = root.Q<TextField>("level-create-new-level-description");
        _newLevelPointsToEarnIntegerField = root.Q<IntegerField>("level-create-new-level-points");
        _createNewLevelButton = root.Q<Button>("level-create-button");
        
        _createNewLevelButton.clicked += CreateNewLevel;
    }

    private void CreateNewLevel()
    {
        Debug.Log(_newLevelNameTextField.text);
        Debug.Log(_newLevelDescriptionTextField.text);
        Debug.Log(_newLevelPointsToEarnIntegerField.value);
    }
}
