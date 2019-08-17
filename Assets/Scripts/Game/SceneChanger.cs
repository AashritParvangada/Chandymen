using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    Scene_Manager scnMan; GameManager gameManager;
    public BoxCollider boxCollider;
    public string S_LoadSceneName;

    int i_token1FugesLit = 0;

    private void OnEnable()
    {
        EventManager.OnDialogueComplete += CheckOnDialogueFinished;
        EventManager.OnToken1FugeHit += Token1AllFugesLit;
        EventManager.OnLastEnemyKilledLevel2 += Level2AllEnemiesKilled;
        EventManager.OnLastEnemyKilledLevel3 += Level3AllEnemiesKilled;
        EventManager.OnLastEnemyKilledToken2 += Token2AllEnemiesKilled;

    }

    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= CheckOnDialogueFinished;
        EventManager.OnToken1FugeHit -= Token1AllFugesLit;
        EventManager.OnLastEnemyKilledLevel2 -= Level2AllEnemiesKilled;
        EventManager.OnLastEnemyKilledLevel3 -= Level3AllEnemiesKilled;
        EventManager.OnLastEnemyKilledToken2 -= Token2AllEnemiesKilled;

    }
    void GetVariables()
    {
        scnMan = FindObjectOfType<Scene_Manager>();
        boxCollider = GetComponent<BoxCollider>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        GetVariables();
        boxCollider.enabled = false;
        CheckToActivateChandyOffice();
        CheckLevel4Activate();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            scnMan.SceneChangeString(S_LoadSceneName);
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

    void CheckToActivateChandyOffice()
    {
        if (scnMan.GetActiveSceneString() == "Level1" && S_LoadSceneName == "Chandy_Office" && !gameManager.B_ChandyOfficeDone)
        {
            boxCollider.enabled = true;
        }
    }

    void CheckLevel4Activate()
    {
        if (scnMan.GetActiveSceneString() == "Level4")
        {
            if (S_LoadSceneName == "Token1") boxCollider.enabled = !gameManager.B_Token1;
            if (S_LoadSceneName == "Token2") boxCollider.enabled = !gameManager.B_Token2;
            if (S_LoadSceneName == "Timboi" && gameManager.B_Token1 && gameManager.B_Token2) boxCollider.enabled = true;
        }
    }
    public void Token2AllEnemiesKilled()
    {
        boxCollider.enabled = true;
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
