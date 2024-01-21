using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class MainMenuLevelSelectionGrid : MonoBehaviour
{
    public MainMenuLevelGridItem LevelGridItemPrefab;

    public void Start()
    {
        LevelData[] levelDatas = Resources.LoadAll<Levels.LevelData>("Levels");
        foreach (LevelData levelData in levelDatas)
        {
            MainMenuLevelGridItem levelGridItem = Instantiate(LevelGridItemPrefab, transform);
            levelGridItem.SetItem(levelData);
        }
    }
}





/*
 * SCRIPTABLE OBJECT -> PlayerSkin
 *
 * public Material 
 * public Color
 * public string
 * public Sprite skinPreview 
 *
 * [CreateAssetMenu]
 * 
 *
 * Resources -> folder Skins -> 4 objecty skinów
 *
 * 
 */











