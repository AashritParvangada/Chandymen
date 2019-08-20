using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int I_SceneNumber;
    public bool B_Token1, B_Token2, B_ChandyOfficeDone;
    public string S_CameFromScene;

    public PlayerData(Scene_Manager _ScnMan, GameManager _GmMan)
    {
        I_SceneNumber = _ScnMan.GetActiveSceneInt();
        B_Token1 = _GmMan.B_Token1; B_Token2 = _GmMan.B_Token2;
        S_CameFromScene = _GmMan.S_CameFromSceneName;
        B_ChandyOfficeDone = _GmMan.B_ChandyOfficeDone;
    }
}
