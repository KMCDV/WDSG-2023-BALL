using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSkinSceneLoad : MonoBehaviour
{
    public void LoadSkinScene()
    {
        SceneManager.LoadScene("SkinSelection");
    }
}
