using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Vector3 V3_LastCheckpointPos;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(this);
        }
    }

    public void SetLastCheckpoint(Checkpoint _Checkpoint)
    {
        V3_LastCheckpointPos = _Checkpoint.transform.position;
    }

}
