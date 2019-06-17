using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    GameManager GM;
    void Start()//This finds the game manager's location and spawns there at the beginning of the game.
    {
        GM = FindObjectOfType<GameManager>();
        transform.position = GM.V3_LastCheckpointPos;
    }

}
