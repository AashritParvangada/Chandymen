using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusTimboi : MonoBehaviour
{
    Timboi tim_timboi;
    // Start is called before the first frame update
    void Start()
    {
        tim_timboi = GetComponentInParent<Timboi>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            Debug.Log("Triggering through raduis");
            tim_timboi.SlashBullet();
        }
    }
}
