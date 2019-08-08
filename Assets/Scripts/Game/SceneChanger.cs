using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    Scene_Manager scnMan;
    BoxCollider boxCollider;
    [SerializeField] string S_loadSceneName;

    int i_token1FugesLit = 0;

    private void OnEnable()
    {
        EventManager.OnDialogueComplete += CheckOnDialogueFinished;
        EventManager.OnLastFugeHit += Token1FugesAdd;
    }

    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= CheckOnDialogueFinished;
        EventManager.OnLastFugeHit -= Token1FugesAdd;

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

    public void Level3EnemyKilled()
    {
        if (scnMan.GetActiveSceneString() == "Level3")
        {
            boxCollider.enabled = true;
        }
    }

    public void Level2AllEnemiesKilled()
    {
        if (scnMan.GetActiveSceneString() == "Level2")
        {
            boxCollider.enabled = true;
        }
    }

    void Token1FugesAdd()
    {

        i_token1FugesLit++;

        if (i_token1FugesLit >= 2)
            Token1AllFugesLit();
    }

    void Token1AllFugesLit()
    {
        if (scnMan.GetActiveSceneString() == "Token1")
        {
            boxCollider.enabled = true;
        }
    }

}
