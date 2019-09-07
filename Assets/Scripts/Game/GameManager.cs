using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gamMan_instance;
    public Vector3 V3_LastCheckpointPos;
    public string S_CameFromSceneName;
    public bool B_Token1, B_Token2;

    public bool B_ChandyOfficeDone = false;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.OnToken1FugeHit += SetToken1;
        EventManager.OnLastEnemyKilledToken2 += SetToken2;
    }

    private void OnDisable()
    {
        EventManager.OnToken1FugeHit -= SetToken1;
        EventManager.OnLastEnemyKilledToken2 -= SetToken2;
    }

    public void SetCameFromSceneName(string _sceneName)
    {
        S_CameFromSceneName = _sceneName;
    }

    private void Awake()
    {
        if (gamMan_instance == null)
        {
            gamMan_instance = this;
            DontDestroyOnLoad(this);
            SpawnPlayer();
        }

        else
        {
            SetOtherGMLocation();
            Destroy(this.gameObject);
        }
    }

    public void SetOtherGMLocation()
    {
        GameManager _GM = null;
        foreach (GameManager _gm in FindObjectsOfType<GameManager>())
            if (_gm != this) _GM = _gm;

        if (FindObjectOfType<Scene_Manager>().GetActiveSceneString() != _GM.S_CameFromSceneName)
        {
            if (_GM != null)
            {
                _GM.V3_LastCheckpointPos = V3_LastCheckpointPos;

                foreach (SceneChanger _scnChng in FindObjectsOfType<SceneChanger>())
                    if (_scnChng.S_LoadSceneName == _GM.S_CameFromSceneName) _GM.V3_LastCheckpointPos = _scnChng.transform.position;
            }
        }
        SpawnPlayer();

    }

    void SpawnPlayer()
    {
        PlayerRespawn _PlRspwn = FindObjectOfType<PlayerRespawn>();
        _PlRspwn.SpawnPlayer();
    }

    public void SetLastCheckpoint(Checkpoint _Checkpoint)//Called from Checkpoint Script. When the player dies, they respawn here.
    {
        Debug.Log("Setting last checkpoint");
        V3_LastCheckpointPos = _Checkpoint.transform.position;
    }

    void SetToken1()
    {
        B_Token1 = true;
    }


    void SetToken2()
    {
        B_Token2 = true;
    }


}
