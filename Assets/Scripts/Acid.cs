using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    bool b_PlayerisinAcid;
    bool b_damangeCoolingdown = false;
    [SerializeField] float F_TimeInterval = 1;
    [SerializeField] int I_DamageCaused = 35;
    private void OnTriggerEnter(Collider other)
    {
        b_PlayerisinAcid = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            TriggerPlayerAcidDamage(other.GetComponent<PlayerController>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            b_PlayerisinAcid = false;
    }

    void TriggerPlayerAcidDamage(PlayerController playa)
    {
        if (!b_damangeCoolingdown)
        {
            if (b_PlayerisinAcid)
            {
                playa.DamageHealth(I_DamageCaused);
                StartCoroutine(AcidCooldown());

            }
        }
    }

    IEnumerator AcidCooldown()
    {
        b_damangeCoolingdown = true;
        yield return new WaitForSeconds(F_TimeInterval);
        b_damangeCoolingdown = false;
    }
}