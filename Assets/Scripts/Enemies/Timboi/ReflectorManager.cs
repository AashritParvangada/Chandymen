using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorManager : MonoBehaviour
{
    [SerializeField] GameObject[] GO_reflectors;
    Dictionary<int, Material> dic_reflectorMaterials = new Dictionary<int, Material>();
    // Start is called before the first frame update
    void Start()
    {

    }

    void GetReflectorPositions()
    {
        foreach (GameObject _reflector in GO_reflectors)
        {

        }
    }

}
