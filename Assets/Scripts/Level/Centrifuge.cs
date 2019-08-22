using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] int I_chargesNeeded;
    [SerializeField] GameObject GO_door;
    AudioSource audioSource;
    public AudioClip AudClp_FugeFail;
    public AudioClip AudClp_FugeOn;

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
                Debug.Log("lkajsdhalkjs");
                audioSource.clip = AudClp_FugeFail;
                audioSource.Play();
                Destroy(thisBullet.gameObject);
            }
        }
    }

    void ActivateFuge(PlasmaBullet _plsbull)
    {
        audioSource.clip = AudClp_FugeOn;
        audioSource.Play();
        if (GO_door) GO_door.SetActive(false);
        Destroy(_plsbull.gameObject);

        if (B_countFugeInEvent)
        {
            FindObjectOfType<EventManager>().FugeHitEvent();
        }
    }

    void GetVariables()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
