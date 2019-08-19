using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyInScene : MonoBehaviour
{
    GameManager gamMan;
    [SerializeField] bool B_chandyOfficeDone;

    private void Start()
    {
        gamMan = FindObjectOfType<GameManager>();
        if (gamMan.B_ChandyOfficeDone != B_chandyOfficeDone)
        {
            Destroy(gameObject);
        }
    }
}
