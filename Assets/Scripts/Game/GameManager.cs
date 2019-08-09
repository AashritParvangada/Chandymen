using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gamMan_instance;
    public Vector3 V3_LastCheckpointPos;

    public bool B_Token1, B_Token2;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.OnToken1FugeHit += SetToken1;
    }

    private void OnDisable()
    {
        EventManager.OnToken1FugeHit -= SetToken1;
    }

    private void Awake()
    {
        if (gamMan_instance == null)
        {
            gamMan_instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(this.gameObject);
        }
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
