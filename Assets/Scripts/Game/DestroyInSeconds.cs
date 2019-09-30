using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] float F_secondsToDestroy = 3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, F_secondsToDestroy);
    }

}
