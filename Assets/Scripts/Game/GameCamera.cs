using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Transform trans_target;
    public Vector3 v3_distanceFromPlayer, v3_viewingAngle;
    [SerializeField] float F_camSize;
    [SerializeField] float F_camShakeTime, F_camShakeMagnitude;
    Vector3 v3_camTarget;
    public bool B_PuzzleMode;
    // Use this for initialization
    void Start()
    {
        trans_target = GameObject.FindObjectOfType<PlayerController>().transform;
        F_camSize = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!B_PuzzleMode)
        {
            FollowTarget();
        }

    }

    void FollowTarget()
    {
        v3_camTarget = trans_target.position + v3_distanceFromPlayer;//Set cam target separately from player target.
        transform.position = Vector3.Lerp(transform.position, v3_camTarget, Time.deltaTime * 8);//Lerp smoothly to cam target.

        transform.eulerAngles = v3_viewingAngle;//Look at player from this specific angle.
    }

    public void EnterPuzzleMode(Transform _newCamPos, float _camSize)
    {
        B_PuzzleMode = true;
        transform.SetParent(_newCamPos);
        StartCoroutine(LerpCamToPos(transform.localPosition, new Vector3(0, 0, 0), 3));
        StartCoroutine(LerpCamSize(F_camSize, _camSize, 3));
    }

    public void ExitPuzzleMode(float _oldCamSize)
    {
        B_PuzzleMode = false;
        transform.SetParent(null);
        StartCoroutine(LerpCamSize(_oldCamSize, F_camSize, 3));
    }

    IEnumerator LerpCamToPos(Vector3 _sourcePos, Vector3 _newPos, float _lerpTime)
    {
        float _startTime = Time.time;
        while (Time.time < _startTime + _lerpTime)
        {
            transform.localPosition = Vector3.Lerp(_sourcePos, _newPos, (Time.time - _startTime) / _lerpTime);
            yield return null;
        }

        transform.localPosition = _newPos;
    }

    IEnumerator LerpCamSize(float _originSize, float _targetSize, float _lerpTime)
    {
        float _startTime = Time.time;
        while (Time.time < _startTime + _lerpTime)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(_originSize, _targetSize, (Time.time - _startTime) / _lerpTime);
            yield return null;
        }

        GetComponent<Camera>().orthographicSize = _targetSize;
    }

    IEnumerator CameraShake(float _duration, float _magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float _timeElapsed = 0f;

        while (_timeElapsed < _duration)
        {
            float x = Random.Range(-1, 1.5f) * _magnitude;
            float y = Random.Range(-1, 1.5f) * _magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);

            _timeElapsed += Time.deltaTime;

            yield return null;
        }
    }

    public void CamShake()
    {
        StartCoroutine(CameraShake(F_camShakeTime, F_camShakeMagnitude));
    }
}
