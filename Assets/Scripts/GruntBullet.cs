using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBullet : MonoBehaviour
{
[SerializeField] int I_DamageCaused= 30;

    private void Start() {
        Destroy(gameObject, 3);
    }


    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().DamageHealth(I_DamageCaused);
            Destroy(gameObject);
        }
    }

}
