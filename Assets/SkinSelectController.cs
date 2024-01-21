using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinSelectController : MonoBehaviour
{
    public List<PlayerSkin> PlayerSkins = new List<PlayerSkin>();
    public TextMeshProUGUI SkinName;
    public Button NextSkinButton;
    public Button PreviousSkinButton;
    public SkinPreviewController SkinPreviewController;

    private int currentSkillIndex;
    
    private void Start()
    {
        PlayerSkins = Resources.LoadAll<PlayerSkin>("Skins").ToList();
        string savedPlayerSkinName = PlayerPrefs.GetString("PLAYER_SKIN");
        int indexToSet = PlayerSkins.FindIndex(x=> x.name == savedPlayerSkinName);
        currentSkillIndex = indexToSet;
        UpdateSkin();
    }
    
    public void NextSkin()
    {
        currentSkillIndex++;
        if (currentSkillIndex > PlayerSkins.Count - 1)
            currentSkillIndex = 0;
        UpdateSkin();
    }

    public void PreviousSkill()
    {
        currentSkillIndex--;
        if (currentSkillIndex < 0)
            currentSkillIndex = PlayerSkins.Count - 1;
        UpdateSkin();
    }

    public void UpdateSkin()
    {
        SkinPreviewController.SetSkin(PlayerSkins[currentSkillIndex]);
        SkinName.text = PlayerSkins[currentSkillIndex].SkinName;
    }

    public void Save()
    {
        PlayerPrefs.SetString("PLAYER_SKIN", PlayerSkins[currentSkillIndex].name);
        SceneManager.LoadScene("MainMenu");
    }
}
