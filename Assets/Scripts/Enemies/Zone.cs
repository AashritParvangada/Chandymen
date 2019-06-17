using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    //This script is actually the Zone Manager. Grunts use this.
    public List<Transform> Trans_List_NavDestinations = new List<Transform>();

    private void Start()
    {
        foreach (Transform _transform in GetComponentsInChildren<Transform>())
        {
            Trans_List_NavDestinations.Add(_transform);
        }

    }


}
