using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleanerBot : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] PatrolPathPoints;
    int currentDestination = 0;
    [SerializeField] float f_TimeInterval = 1;
    [SerializeField] float f_ResetTimeInterval = 1;
    PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("Got Player");
            other.transform.SetParent(gameObject.transform);
            player.b_AboveAcid = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
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
        player = GameObject.FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        Patrol();
    }

    void Patrol()
    {
        if (PatrolPathPoints.Length <= currentDestination + 1)
        {
            currentDestination = 0;
            StartCoroutine(GoToNextDestination(currentDestination, f_ResetTimeInterval));

        }

        else
        {
            currentDestination++;
            StartCoroutine(GoToNextDestination(currentDestination, f_TimeInterval));
        }

    }

    IEnumerator GoToNextDestination(int _currDest, float _time)
    {
        agent.SetDestination(PatrolPathPoints[currentDestination].position);
        yield return new WaitForSeconds(_time);
        Patrol();
    }
}
