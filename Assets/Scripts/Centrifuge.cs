using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] int I_chargesNeeded;
    [SerializeField] GameObject GO_door;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            Debug.Log("Entered Trigger");
            PlasmaBullet thisBullet = other.GetComponent<PlasmaBullet>();
            if (thisBullet.I_BulletCharge == I_chargesNeeded)
            {
                //Should put animation here.
                Destroy(GO_door);
                Destroy(thisBullet);
            }
        }
    }


}
