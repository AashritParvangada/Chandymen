using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{

    public void SceneChange(string _SceneName)//Isn't being used yet. Use when shifting levels.
    {
        SceneManager.LoadScene(_SceneName);
    }

    public void ReloadScene()//Is being used on player death.
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
