using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject GO_PurpleBar, GO_YellowBar;

    public void ScalePurpleBar(float _scale)
    {
        Vector3 _currentScale = GO_PurpleBar.transform.localScale;
        GO_PurpleBar.transform.localScale = new Vector3(_scale, _currentScale.y, _currentScale.z);
    }

}
