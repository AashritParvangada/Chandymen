using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 10;
    public GameObject Projectile;
    // Use this for initialization
    void Start()
    {

    }

    public void ShootProjectile(Transform _origin)
    {
        GameObject newBullet = Instantiate(Projectile, _origin.position, _origin.rotation, null);
        newBullet.GetComponent<Rigidbody>().velocity = transform.up * bulletSpeed;
        newBullet.transform.forward = transform.up;

    }
}
