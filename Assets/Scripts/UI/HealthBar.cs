using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject GO_purpleBar, GO_yellowBar, GO_rightCap, GO_leftCap;
    [SerializeField] float F_duration;
    public void ScalePurpleBar(float _scale)
    {
        Vector3 _currentScale = GO_purpleBar.transform.localScale;
        GO_purpleBar.transform.localScale = new Vector3(_scale, _currentScale.y, _currentScale.z);
    }

    public void ScaleYellowBar(float _endScale)
    {
        StopAllCoroutines();
        StartCoroutine(LerpYellowBar(_endScale, 2));
    }

    public void SwitchRightCap(bool _on)
    {
        GO_rightCap.SetActive(_on);
    }

    public void SwitchLeftCap(bool _on)
    {
        GO_leftCap.SetActive(_on);
    }

    IEnumerator LerpYellowBar(float _endScale, float _time)
    {
        Vector3 _currentScale = GO_yellowBar.transform.localScale;
        Vector3 _targetScale = new Vector3(_endScale, _currentScale.y, _currentScale.z);
        float _originalTime = _time;

        while (_time > 0)
        {
            _time -= Time.deltaTime;

            GO_yellowBar.transform.localScale = Vector3.Lerp(_targetScale, _currentScale, _time / _originalTime);
            yield return null;
        }

    }

}
