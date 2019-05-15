using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuge : MonoBehaviour
{
    [SerializeField] int i_chargesNeeded;
    [SerializeField] GameObject go_door;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            Debug.Log("Entered Trigger");
            PlasmaBullet thisBullet = other.GetComponent<PlasmaBullet>();
            if(thisBullet.I_BulletCharge==i_chargesNeeded)
            {
                //Should put animation here.
                Destroy(go_door);
                Destroy(thisBullet);
            }
        }
    }


}
