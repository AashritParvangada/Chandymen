using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] int I_chargesNeeded;
    [SerializeField] ElectricDoor ElecDoor_door;
    AudioSource audioSource;
    public AudioClip AudClp_FugeFail;
    public AudioClip AudClp_FugeOn;
    Animator animator;
    [SerializeField] bool B_countFugeInEvent = false;

    private void Start()
    {
        GetVariables();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            PlasmaBullet thisBullet = other.GetComponent<PlasmaBullet>();
            if (thisBullet.I_BulletCharge >= I_chargesNeeded)
            {
                ActivateFuge(thisBullet);
            }

            else
            {
                FailFuge(thisBullet);
            }
        }
    }

    void ActivateFuge(PlasmaBullet _plsbull)
    {
        audioSource.clip = AudClp_FugeOn;
        audioSource.Play();
        if (ElecDoor_door) ElecDoor_door.SwitchDoor(false);
        Destroy(_plsbull.gameObject);

        if (B_countFugeInEvent)
        {
            FindObjectOfType<EventManager>().FugeHitEvent();
        }
    }

    void FailFuge(PlasmaBullet _plsBull)
    {
        audioSource.clip = AudClp_FugeFail;
        audioSource.Play();
        animator.SetTrigger("Fail");
        Destroy(_plsBull.gameObject);
    }

    void GetVariables()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
}
