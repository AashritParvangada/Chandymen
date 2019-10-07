using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoor : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] GameObject GO_door; Renderer rend_door; BoxCollider boxCol_Door;
    [SerializeField] float F_lerpTime;

    private void Start()
    {
        GetVariables();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rend_door.material.SetFloat("Vector1_B15186D7", 1f);
        }
    }

    void GetVariables()
    {
        audioSource = GetComponent<AudioSource>();
        rend_door = GO_door.GetComponent<Renderer>();
        boxCol_Door = GO_door.GetComponent<BoxCollider>();
    }

    public void SwitchDoor(bool _toActivate)
    {
        StartCoroutine(IEnum_LerpClippingThresehold(_toActivate, F_lerpTime));
    }

    IEnumerator IEnum_LerpClippingThresehold(bool _toActivate, float _time)
    {
        float _newClipSrength = 0;
        if (!_toActivate) _newClipSrength = 1;
        float elapsedTime = 0;
        float startingClippingSrength = rend_door.material.GetFloat("Vector1_B15186D7");

        while (elapsedTime < _time)
        {
            float _currentClipStrength = Mathf.Lerp(startingClippingSrength, _newClipSrength, elapsedTime / _time);
            rend_door.material.SetFloat("Vector1_B15186D7", _currentClipStrength);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }

}
