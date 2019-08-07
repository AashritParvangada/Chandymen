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
    public static event LastFugeHit OnLastFugeHit;

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

    public void LastFugeHitEvent()
    {
        if (OnLastFugeHit != null) OnLastFugeHit();
    }
}
