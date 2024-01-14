using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level Data", order = 0)]
    public class LevelData : ScriptableObject
    {
        public string SceneName;
        public string Description;
        public Sprite Image;
        public int PointsToEarn;
    }
    
    [UnityEditor.CustomEditor(typeof(LevelData))]
    public class LevelDataCuystomEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            SerializedObject serializedObject = new SerializedObject(target);

            LevelData levelData = (LevelData)target;
            
            VisualElement root = new VisualElement();
                    
            SerializedProperty sceneNameProperty = serializedObject.FindProperty(nameof(levelData.SceneName));
            PropertyField propertyField = new PropertyField(sceneNameProperty);
            propertyField.name = "Scene Name";
            propertyField.BindProperty(sceneNameProperty);
            root.Add(propertyField);
            
            SerializedProperty descriptionProperty = serializedObject.FindProperty(nameof(levelData.Description));
            PropertyField element = new PropertyField(descriptionProperty);
            element.name = "Description";
            element.BindProperty(descriptionProperty);
            root.Add(element);
            
            SerializedProperty imageProperty = serializedObject.FindProperty(nameof(levelData.Image));
            PropertyField child = new PropertyField(imageProperty);
            child.name = "Image";
            child.BindProperty(imageProperty);
            root.Add(child);
            
            SerializedProperty pointsToEarnProperty = serializedObject.FindProperty(nameof(levelData.PointsToEarn));
            PropertyField field = new PropertyField(pointsToEarnProperty);
            field.name = "Points To Earn";
            field.BindProperty(pointsToEarnProperty);
            root.Add(field);
            
            //ADD CUSTOM BUTTON
            Button visualElement = new Button(LoadScene) { text = "Open Scene" };
            root.Add(visualElement);
            
            //UXML LOADING
            /*VisualTreeAsset loadAssetAtPath = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/LevelCreator.uxml");
            root.Add(loadAssetAtPath.CloneTree());*/
            return root;
        }

        private void LoadScene() => EditorSceneManager.OpenScene("Assets/Scenes/Levels/" + ((LevelData)target).SceneName + ".unity");
    }
}