using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Scene_Manager ScnMan_SceneManager;
    void GetVariables()
    {
        ScnMan_SceneManager = FindObjectOfType<Scene_Manager>();
    }

    private void Start()
    {
        GetVariables();
    }

    public void PlayButton()
    {
        ScnMan_SceneManager.SceneChangeInt(1);
    }

    public void QuitButton()
    {
        ScnMan_SceneManager.QuitGame();
    }
}
