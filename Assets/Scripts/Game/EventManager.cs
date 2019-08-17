using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    public delegate void DialogueDone();
    public static event DialogueDone OnDialogueComplete;

    public delegate void LastFugeHit();
    public static event LastFugeHit OnFugeHit;

    public delegate void Token1FugeHit();
    public static event Token1FugeHit OnToken1FugeHit;

    public delegate void LastEnemyKilledLevel2();
    public static event LastEnemyKilledLevel2 OnLastEnemyKilledLevel2;
    public delegate void LastEnemyKilledLevel3();
    public static event LastEnemyKilledLevel3 OnLastEnemyKilledLevel3;
    public delegate void LastEnemyKilledToken2();
    public static event LastEnemyKilledToken2 OnLastEnemyKilledToken2;
    int i_markedFugeHit = 0;

    Scene_Manager ScnMan;
    private void Start()
    {
        ScnMan = FindObjectOfType<Scene_Manager>();
    }

    public void CountEnemyKilled()//When this function is called, it runs the OnEnemyKilled event.
    {
        if (OnEnemyKilled != null)
            OnEnemyKilled();
    }

    public void FinishDialogueEvent()
    {
        if (OnDialogueComplete != null)
            OnDialogueComplete();
    }

    public void FugeHitEvent()
    {
        i_markedFugeHit++;
        if (OnFugeHit != null) OnFugeHit();

        if (i_markedFugeHit >= 2 && ScnMan.GetActiveSceneString() == "Token1")
        {
            if (OnToken1FugeHit != null) OnToken1FugeHit();
        }
    }

    public void LastEnemyKilledEvent()
    {
        if (ScnMan.GetActiveSceneString() == "Level1" && FindObjectOfType<GameManager>().B_ChandyOfficeDone) OnLastEnemyKilledLevel2();
        else if (ScnMan.GetActiveSceneString() == "Level3") OnLastEnemyKilledLevel3();
        else if (ScnMan.GetActiveSceneString() == "Token2") OnLastEnemyKilledToken2();
    }
}
