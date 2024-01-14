using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class MainMenuLevelGridItem : MonoBehaviour
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private TMPro.TextMeshProUGUI _descriptionText;
    [SerializeField] private TMPro.TextMeshProUGUI _pointsText;
    [SerializeField] private UnityEngine.UI.Image _image;
    [SerializeField] private UnityEngine.UI.Button _button;
    
    private void OnValidate()
    {
        SetItemBasedOnLevelData();
    }
    
    private void SetItemBasedOnLevelData()
    {
        if (_levelData == null) return;
        _descriptionText.text = _levelData.Description;
        _pointsText.text = "Points To Earn: " + _levelData.PointsToEarn.ToString();
        _image.sprite = _levelData.Image;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        GameManager.RequestLevelLoad(_levelData.SceneName);
    }
    
    public void SetItem(LevelData levelData)
    {
        _levelData = levelData;
        SetItemBasedOnLevelData();
    }
}
