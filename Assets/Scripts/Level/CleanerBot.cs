using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleanerBot : MonoBehaviour
{
    NavMeshAgent navmesh_agent;
    public Transform[] Trans_Arr_PatrolPathPoints;
    int i_currentDestination = 0;
    public float F_TimeInterval = 1;
    public float F_ResetTimeInterval = 1;
    PlayerController plctrl_playa;
    Animator animor_anim;

    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
        CheckArraySizeToPatrol();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PlayerLanded(other);
        }
    }

    void PlayerLanded(Collider other)
    {
        animor_anim.SetTrigger("Land");
        other.transform.SetParent(transform);
        plctrl_playa.B_AboveAcid = true;
    }

    //This is currently to ensure that when the dash mode turns off, Jai doesn't start getting killed. This should be edited later.
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.transform.parent = transform;
            plctrl_playa.B_AboveAcid = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.transform.parent = null;
            plctrl_playa.B_AboveAcid = false;
        }
    }

    void PlayerGotOff(Collider other)
    {
        other.transform.parent = null;
        plctrl_playa.B_AboveAcid = false;
    }

    //Find the player, your own agent, animator.
    void GetVariables()
    {
        plctrl_playa = GameObject.FindObjectOfType<PlayerController>();
        navmesh_agent = GetComponent<NavMeshAgent>();
        animor_anim = GetComponent<Animator>();
    }

    //If there is an array that is greater than 1 point in length, start patrolling.
    public void CheckArraySizeToPatrol()
    {
        if (Trans_Arr_PatrolPathPoints.Length > 0)
        {
            Patrol();
        }
    }

    void Patrol()//Goes through the array by moving. Has a different reset time than the regular time because agent returns to spot 1.
    {
        if (Trans_Arr_PatrolPathPoints.Length <= i_currentDestination + 1)
        {
            i_currentDestination = 0;
            StartCoroutine(GoToNextDestination(i_currentDestination, F_ResetTimeInterval));

        }

        else
        {
            i_currentDestination++;
            StartCoroutine(GoToNextDestination(i_currentDestination, F_TimeInterval));
        }

    }

    IEnumerator GoToNextDestination(int _currDest, float _time)
    {
        navmesh_agent.SetDestination(Trans_Arr_PatrolPathPoints[i_currentDestination].position);
        yield return new WaitForSeconds(_time);
        Patrol();
    }
}
