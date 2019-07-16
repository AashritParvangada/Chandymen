using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    bool b_loadedGame;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SavePlayer();
            Debug.Log("Saving Player");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayer();
            Debug.Log("Loading Player");
        }
    }

    public void SavePlayer()
    {
        SaveGame.SavePlayer(this, FindObjectOfType<GameManager>());
    }

    public void LoadPlayer()
    {
        PlayerData _data = SaveGame.LoadPlayer();
        SceneChange(_data.I_SceneNumber);
    }

    public void SceneChange(int _SceneName)//Isn't being used yet. Use when shifting levels.
    {
        SceneManager.LoadScene(_SceneName);
    }

    public void ReloadScene()//Is being used on player death.
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public int GetActiveSceneInt()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }


}
