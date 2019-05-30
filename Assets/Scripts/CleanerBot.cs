using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleanerBot : MonoBehaviour
{
    NavMeshAgent NavMes_agent;
    public Transform[] Trans_Arr_PatrolPathPoints;
    int currentDestination = 0;
    public float F_TimeInterval = 1;
    public float F_ResetTimeInterval = 1;
    PlayerController player;
    Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            anim.SetTrigger("Land");
            other.transform.SetParent(gameObject.transform);
            player.b_AboveAcid = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.transform.parent = transform;
            player.b_AboveAcid = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.transform.parent = null;
            player.b_AboveAcid = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetVariables();

        CheckArraySizeToPatrol();
    }

    void GetVariables()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        NavMes_agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public void CheckArraySizeToPatrol()
    {
        if (Trans_Arr_PatrolPathPoints.Length > 0)
        {
            Patrol();
        }
    }

    void Patrol()//Goes through the array by moving.
    {
        if (Trans_Arr_PatrolPathPoints.Length <= currentDestination + 1)
        {
            currentDestination = 0;
            StartCoroutine(GoToNextDestination(currentDestination, F_ResetTimeInterval));

        }

        else
        {
            currentDestination++;
            StartCoroutine(GoToNextDestination(currentDestination, F_TimeInterval));
        }

    }

    IEnumerator GoToNextDestination(int _currDest, float _time)
    {
        NavMes_agent.SetDestination(Trans_Arr_PatrolPathPoints[currentDestination].position);
        yield return new WaitForSeconds(_time);
        Patrol();
    }
}
