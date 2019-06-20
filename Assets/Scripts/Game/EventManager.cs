using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    public delegate void DialogueDone();
    public static event DialogueDone OnDialogueComplete;

    public void CountEnemyKilled()//When this function is called, it runs the OnEnemyKilled event.
    {
        OnEnemyKilled();
    }

    public void FinishDialogueEvent()
    {
        OnDialogueComplete();
    }
}
