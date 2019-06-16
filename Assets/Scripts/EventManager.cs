using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    public void CountEnemyKilled()//When this function is called, it runs the OnEnemyKilled event.
    {
        OnEnemyKilled();
    }
}
