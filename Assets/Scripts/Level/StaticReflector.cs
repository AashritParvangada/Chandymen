using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticReflector : MonoBehaviour
{
    [SerializeField] Material Mat_frame, Mat_plasma, Mat_forceField;
    public bool B_isBossBattle = false;
    [SerializeField] SkinnedMeshRenderer SknMsh_plasma;
    [SerializeField] MeshRenderer MshRn_frame;
    [SerializeField] GameObject Go_light;
    // Start is called before the first frame update
    void Start()
    {
        if (B_isBossBattle) SetForceFieldMaterial();
    }

    void SetNormalMaterials()
    {
        Material[] materials = SknMsh_plasma.materials;
        materials[1] = Mat_plasma;
        SknMsh_plasma.materials = materials;

        MshRn_frame.material = Mat_frame;
        Go_light.SetActive(true);
    }

    void SetForceFieldMaterial()
    {
        Material[] materials = SknMsh_plasma.materials;
        materials[1] = Mat_forceField;
        SknMsh_plasma.materials = materials;

        MshRn_frame.material = Mat_forceField;
        Go_light.SetActive(false);
    }

    public void SwitchMaterials(float _time)
    {
        StartCoroutine(IEnum_LerpClippingThresehold(_time));
    }

    IEnumerator IEnum_LerpClippingThresehold(float _time)
    {
        SetForceFieldMaterial();

        float _newClipSrength = 0;
        float elapsedTime = 0;

        Material[] materials = SknMsh_plasma.materials;
        materials[1].SetFloat("Vector1_B15186D7", 1);
        SknMsh_plasma.materials = materials;

        MshRn_frame.materials[0].SetFloat("Vector1_B15186D7", 1);

        float startingClippingSrength = 1;

        while (elapsedTime < _time)
        {
            float _currentClipStrength = Mathf.Lerp(startingClippingSrength, _newClipSrength, elapsedTime / _time);

            materials[1].SetFloat("Vector1_B15186D7", _currentClipStrength);
            SknMsh_plasma.materials = materials;

            MshRn_frame.materials[0].SetFloat("Vector1_B15186D7", _currentClipStrength);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SetNormalMaterials();
    }

}
