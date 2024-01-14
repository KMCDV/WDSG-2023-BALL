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
