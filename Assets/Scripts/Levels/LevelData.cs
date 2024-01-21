using System;
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
        public event Action ValueChanged;

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }
        public override VisualElement CreateInspectorGUI()
        {
            SerializedObject serializedObject = new SerializedObject(target);

            LevelData levelData = (LevelData)target;
            
            VisualElement root = new VisualElement();


            string oldValue = levelData.SceneName;
            SerializedProperty sceneNameProperty = serializedObject.FindProperty(nameof(levelData.SceneName));
            PropertyField propertyField = new PropertyField(sceneNameProperty);
            propertyField.name = "Scene Name";
            propertyField.BindProperty(sceneNameProperty);
            root.Add(propertyField);
            propertyField.RegisterCallback<ChangeEvent<string>>((x) =>
            {
                if(x.newValue != oldValue) 
                    ValueChanged?.Invoke();
            });

            string oldDescriptionValue = levelData.Description;
            SerializedProperty descriptionProperty = serializedObject.FindProperty(nameof(levelData.Description));
            PropertyField element = new PropertyField(descriptionProperty);
            element.name = "Description";
            element.BindProperty(descriptionProperty);
            root.Add(element);
            element.RegisterCallback<ChangeEvent<string>>((x) =>
            {
                if(x.newValue != oldDescriptionValue)
                    ValueChanged?.Invoke();
            });


            Sprite oldSpriteValue = levelData.Image;
            SerializedProperty imageProperty = serializedObject.FindProperty(nameof(levelData.Image));
            PropertyField child = new PropertyField(imageProperty);
            child.name = "Image";
            child.BindProperty(imageProperty);
            root.Add(child);
            child.RegisterCallback<ChangeEvent<Sprite>>((x) =>
            {
                if(oldSpriteValue != x.newValue)
                    ValueChanged?.Invoke();
            });

            int pointstoEarnValue = levelData.PointsToEarn;
            SerializedProperty pointsToEarnProperty = serializedObject.FindProperty(nameof(levelData.PointsToEarn));
            PropertyField field = new PropertyField(pointsToEarnProperty);
            field.name = "Points To Earn";
            field.BindProperty(pointsToEarnProperty);
            root.Add(field);
            field.RegisterCallback<ChangeEvent<int>>((x) =>
            {
                if(pointstoEarnValue != x.newValue)
                    ValueChanged?.Invoke();
            });

            //ADD CUSTOM BUTTON
            Button visualElement = new Button(LoadScene) { text = "Open Scene" };
            root.Add(visualElement);
            return root;
        }

        private void LoadScene() => EditorSceneManager.OpenScene("Assets/Scenes/Levels/" + ((LevelData)target).SceneName + ".unity");
    }
}