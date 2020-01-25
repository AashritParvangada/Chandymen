using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceShield : MonoBehaviour
{
    [SerializeField] int I_damage = 40;

    [SerializeField] AudioClip AudClp_shock, AudClp_absorb;
    AudioSource audioSource;
    //Destroy bullets that hit the shield, but damage the player who hits it.
    private void OnTriggerEnter(Collider other)
    {
        CheckPlasmaBullet(other);
        CheckPlayer(other);
        CheckPlayerGun(other);
    }

    private void Start() {
        GetVariables();
    }

    void CheckPlasmaBullet(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            Destroy(other.gameObject);
            other.GetComponent<PlasmaBullet>().WallParticles();
            PlaySound(AudClp_absorb);
        }
    }

    void CheckPlayer(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PlaySound(AudClp_shock);
            ShockPlayer(other.GetComponent<PlayerController>());
        }
    }

    void ShockPlayer(PlayerController _playa)
    {
        _playa.TempDisableController(0.3f);
        _playa.DamageHealth(I_damage);
        _playa.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
    }


    void CheckPlayerGun(Collider other)
    {
        if (other.GetComponent<Gun>())
        {
            ShockPlayer(other.GetComponentInParent<PlayerController>());
        }
    }

    void GetVariables()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }


    public void PlaySound(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
