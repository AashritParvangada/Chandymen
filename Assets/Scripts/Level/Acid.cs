using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    bool b_PlayerisinAcid;
    bool b_DamageCoolingdown = false;
    [SerializeField] float F_timeInterval = .1f;
    [SerializeField] int I_damageCaused = 2;
    private void OnTriggerEnter(Collider other)
    {
        b_PlayerisinAcid = true;
    }

    //When the player is in acid, start the damage function.
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            TriggerPlayerAcidDamage(other.GetComponent<PlayerController>());
    }

    //To stop the loop of the player being damaged.
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            b_PlayerisinAcid = false;
    }


    //Start damaging the player and set a loop.
    void TriggerPlayerAcidDamage(PlayerController playa)
    {
        if (!b_DamageCoolingdown)
        {
            if (b_PlayerisinAcid)
            {
                playa.AcidDamageHealth(I_damageCaused);
                StartCoroutine(AcidCooldown());

            }
        }
    }

    //To damage the player gradually over time.
    IEnumerator AcidCooldown()
    {
        b_DamageCoolingdown = true;
        yield return new WaitForSeconds(F_timeInterval);
        b_DamageCoolingdown = false;
    }
}