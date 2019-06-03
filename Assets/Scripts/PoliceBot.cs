using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBot : MonoBehaviour
{
    public int i_health = 100;
    NavMeshAgent agent;
    [SerializeField] bool b_AttackOnStart = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CheckAttackOnStart();
    }

    void CheckAttackOnStart()
    {
        if(b_AttackOnStart)
        {
           PlayerController _playa = FindObjectOfType<PlayerController>();
           SetTarget(_playa);
        }
    }

    IEnumerator RetargetPlayer(PlayerController _target)
    {
        yield return new WaitForSeconds(.3f);
        SetTarget(_target);
    }

    //Sets the bot to target the player and loops a coroutine that retargets the player's location every .3 seconds.
    public void SetTarget(PlayerController _target)
    {
        Debug.Log(_target.name);
        agent.SetDestination(_target.transform.position);
        StartCoroutine(RetargetPlayer(_target));
    }

//Decrease the bot's health when shot. Currently is a 1HKO.
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
