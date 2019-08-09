using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    Scene_Manager scnMan;
    public BoxCollider boxCollider;
    [SerializeField] string S_loadSceneName;

    int i_token1FugesLit = 0;

    private void OnEnable()
    {
        EventManager.OnDialogueComplete += CheckOnDialogueFinished;
        EventManager.OnToken1FugeHit += Token1AllFugesLit;
        EventManager.OnLastEnemyKilledLevel2 += Level2AllEnemiesKilled;
        EventManager.OnLastEnemyKilledLevel3 += Level3AllEnemiesKilled;

    }

    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= CheckOnDialogueFinished;
        EventManager.OnToken1FugeHit -= Token1AllFugesLit;
        EventManager.OnLastEnemyKilledLevel2 -= Level2AllEnemiesKilled;
        EventManager.OnLastEnemyKilledLevel3 -= Level3AllEnemiesKilled;

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

    public void Level3AllEnemiesKilled()
    {
        boxCollider.enabled = true;
    }

    public void Level2AllEnemiesKilled()
    {
        boxCollider.enabled = true;
    }

    void Token1AllFugesLit()
    {
        boxCollider.enabled = true;
    }

}
