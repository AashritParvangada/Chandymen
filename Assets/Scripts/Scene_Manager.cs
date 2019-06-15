using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{

    GameManager GmMngr_CheckpointManager;

    private void Start()
    {
        GmMngr_CheckpointManager = FindObjectOfType<GameManager>();
    }

    public void SceneChange(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
