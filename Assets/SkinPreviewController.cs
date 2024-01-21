using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SkinPreviewController : MonoBehaviour
{
    public PlayerSkin PlayerSkin;
    public MeshRenderer MeshRenderer;

    private void OnValidate()
    {
        UpdateSkin();
    }

    public void SetSkin(PlayerSkin newSkin)
    {
        PlayerSkin = newSkin;
        UpdateSkin();
    }

    private void UpdateSkin()
    {
        if(PlayerSkin == null) return;

        if (Application.isPlaying == false)
        {
            MeshRenderer.sharedMaterial = PlayerSkin.Material;
            MeshRenderer.sharedMaterial.color = PlayerSkin.Color; 
            return;
        }
        MeshRenderer.material = PlayerSkin.Material;
        MeshRenderer.material.color = PlayerSkin.Color;
    }
}
