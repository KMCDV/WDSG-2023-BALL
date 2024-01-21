using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public PlayerSkin CurrentPlayerSkin;
    
    private void Start()
    {
        string savedPlayerSkinName = PlayerPrefs.GetString("PLAYER_SKIN");
        PlayerSkin playerSkin = Resources.Load<PlayerSkin>("Skins/" + savedPlayerSkinName);
        CurrentPlayerSkin = playerSkin;
        MeshRenderer.material = playerSkin.Material;
        MeshRenderer.material.color = playerSkin.Color;
    }
}
