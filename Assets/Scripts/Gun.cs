using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 10;
    public GameObject Projectile;
    PlayerController PlaCont_Playa;
    // Use this for initialization
    void Start()
    {
        PlaCont_Playa = GetComponentInParent<PlayerController>();
    }

    public void DamageParent(int _Damage)
    {
        PlaCont_Playa.DamageHealth(_Damage);
    }

    public void ShootProjectile(Transform _origin)
    {
        GameObject newBullet = Instantiate(Projectile, _origin.position, _origin.rotation, null);
        newBullet.GetComponent<Rigidbody>().velocity = transform.up * bulletSpeed;
        newBullet.transform.forward = transform.up;

    }
}
