using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBullet : MonoBehaviour
{
[SerializeField] int I_damageCaused= 30;

    private void Start() {
        Destroy(gameObject, 3);//Bullet only lasts for 3 seconds.
    }


    private void OnTriggerEnter(Collider other)//If a player enters its radius, damage the player. Else if the bullet hits a wall, destroy this. 
    {
        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().DamageHealth(I_damageCaused);
            Destroy(gameObject);
        }

        else if(other.gameObject.layer==9)
        {
            Destroy(gameObject);
        }
    }

}
