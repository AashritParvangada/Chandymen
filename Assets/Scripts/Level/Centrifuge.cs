using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] int I_chargesNeeded;
    [SerializeField] GameObject GO_door;

    [SerializeField] bool B_isLastinLevel = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            PlasmaBullet thisBullet = other.GetComponent<PlasmaBullet>();
            if (thisBullet.I_BulletCharge >= I_chargesNeeded)
            {
                //Should put animation here.
                ActivateFuge(thisBullet);
            }
        }
    }

    void ActivateFuge(PlasmaBullet _plsbull)
    {
        if (GO_door) GO_door.SetActive(false);
        Destroy(_plsbull.gameObject);

        if (B_isLastinLevel)
        {
            FindObjectOfType<EventManager>().LastFugeHitEvent();
        }
    }


}
