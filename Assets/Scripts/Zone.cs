using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] int I_ZoneNumber;
    public List<Transform> Trans_List_NavDestinations = new List<Transform>();

    private void Start()
    {
        foreach (Transform _transform in GetComponentsInChildren<Transform>())
        {
            Trans_List_NavDestinations.Add(_transform);
        }

    }


}
