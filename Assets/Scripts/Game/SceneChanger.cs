using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    Scene_Manager scnMan;
    [SerializeField] string S_loadSceneName;
    void GetVariables()
    {
        scnMan = FindObjectOfType<Scene_Manager>();
    }
    private void Start()
    {
        GetVariables();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            scnMan.SceneChangeString(S_loadSceneName);
        }
    }

}
