using UnityEngine;
using UnityEngine.SceneManagement;

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
}