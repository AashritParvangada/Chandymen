using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    Scene_Manager scnMan;
    BoxCollider boxCollider;
    [SerializeField] string S_loadSceneName;

    private void OnEnable()
    {
        EventManager.OnDialogueComplete += CheckOnDialogueFinished;
    }

    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= CheckOnDialogueFinished;

    }
    void GetVariables()
    {
        scnMan = FindObjectOfType<Scene_Manager>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        GetVariables();
        boxCollider.enabled = false;
        CheckToActivateOnStart();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            scnMan.SceneChangeString(S_loadSceneName);
        }
    }

    void CheckOnDialogueFinished()
    {
        Debug.Log(scnMan.GetActiveSceneString());
        if (scnMan.GetActiveSceneString() == "Chandy_House")
        {
            boxCollider.enabled = true;
        }
        else if (scnMan.GetActiveSceneString() == "Chandy_Office")
        {
            boxCollider.enabled = true;
        }
    }

    void CheckToActivateOnStart()
    {
        if (scnMan.GetActiveSceneString() == "Level1")
        {
            boxCollider.enabled = true;
        }

    }

}
