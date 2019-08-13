using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    bool b_loadedGame;
    GameManager GmMan_Manager;
    [SerializeField] SceneChanger ScnChng_Token1, ScnChng_Token2;
    private void Start()
    {
        GetVariables();
    }

    void CheckGameManagerTokens()
    {
        if (GmMan_Manager.B_Token1 && ScnChng_Token1) ScnChng_Token1.boxCollider.enabled = false;
        if (GmMan_Manager.B_Token2 && ScnChng_Token2) ScnChng_Token2.boxCollider.enabled = false;
    }

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
        GmMan_Manager.B_Token1 = _data.B_Token1; GmMan_Manager.B_Token2 = _data.B_Token2;
        SceneChangeInt(_data.I_SceneNumber);
    }

    void GetVariables()
    {
        GmMan_Manager = FindObjectOfType<GameManager>();
    }

    public void SceneChangeInt(int _SceneIndex)//Isn't being used yet. Use when shifting levels.
    {
        SceneManager.LoadScene(_SceneIndex);
    }

    public void SceneChangeString(string _SceneName)//Isn't being used yet. Use when shifting levels.
    {
        FindObjectOfType<GameManager>().SetCameFromSceneName(GetActiveSceneString());
        SceneManager.LoadScene(_SceneName);
    }

    public void ReloadScene()//Is being used on player death.
    {
        Scene scene = SceneManager.GetActiveScene();
        FindObjectOfType<GameManager>().S_CameFromSceneName = scene.name;
        SceneManager.LoadScene(scene.name);
    }

    public int GetActiveSceneInt()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public string GetActiveSceneString()
    {
        return SceneManager.GetActiveScene().name;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
