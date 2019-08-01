using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceShield : MonoBehaviour
{
    [SerializeField] int I_damage = 40;
    //Destroy bullets that hit the shield, but damage the player who hits it.
    private void OnTriggerEnter(Collider other)
    {
        CheckPlasmaBullet(other);
        CheckPlayer(other);
        CheckPlayerGun(other);
    }

    void CheckPlasmaBullet(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            Destroy(other.gameObject);
        }
    }

    void CheckPlayer(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            StartCoroutine(ShockPlayer(other.GetComponent<PlayerController>()));
        }
    }

    IEnumerator ShockPlayer(PlayerController _playa)
    {
        _playa.enabled = false;
        _playa.DamageHealth(I_damage);
        _playa.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        _playa.enabled = true;

    }


    void CheckPlayerGun(Collider other)
    {
        if (other.GetComponent<Gun>())
        {
            StartCoroutine(ShockPlayer(other.GetComponentInParent<PlayerController>()));
        }
    }
}
