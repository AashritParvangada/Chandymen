using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gamMan_instance;
    public Vector3 V3_LastCheckpointPos;

    public bool B_Token1, B_Token2;
    // Start is called before the first frame update
    private void Awake()
    {
        if (gamMan_instance == null)
        {
            gamMan_instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(this);
        }
    }

    public void SetLastCheckpoint(Checkpoint _Checkpoint)//Called from Checkpoint Script. When the player dies, they respawn here.
    {
        V3_LastCheckpointPos = _Checkpoint.transform.position;
    }

}
