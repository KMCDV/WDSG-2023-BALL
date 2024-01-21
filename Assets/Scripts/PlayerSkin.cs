using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Skin", fileName = "New Skin")]
    public class PlayerSkin : ScriptableObject
    {
        public string SkinName;
        public Material Material;
        public Color Color;
        public Sprite preview;
    }
}