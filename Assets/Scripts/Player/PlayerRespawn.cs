using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    GameManager GM;

    public void SpawnPlayer()
    {
        StartCoroutine(WaitForFrameThenSpawn());
    }

    IEnumerator WaitForFrameThenSpawn()
    {
        yield return new WaitForEndOfFrame();
        GM = FindObjectOfType<GameManager>();
        Debug.Log(GM.V3_LastCheckpointPos);
        transform.position = GM.V3_LastCheckpointPos;
    }
}
