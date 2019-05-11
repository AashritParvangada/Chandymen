using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBot : MonoBehaviour
{
    public int i_health = 100;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator RetargetPlayer(PlayerController _target)
    {
        yield return new WaitForSeconds(.3f);
        SetTarget(_target);
    }

    public void SetTarget(PlayerController _target)
    {
        agent.SetDestination(_target.transform.position);
        StartCoroutine(RetargetPlayer(_target));
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<PlasmaBullet>())
        {
            DecreaseHealth(100);
        }
    }

    void DecreaseHealth(int _DecreaseBy)
    {
        i_health-=_DecreaseBy;
        if(i_health<=0)
        {
            Destroy(gameObject);
        }
    }
}
