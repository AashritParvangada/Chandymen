using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    ParticleSystem[] Prtcl_Arr_toPlayOnStart;
    [SerializeField] AudioClip AudClp_toPlayOnStart;
    AudioSource source;

    private void Start()
    {
        GetVariables();
    }

    public void Activate()
    {
        transform.SetParent(null);
        PlayParticleAndAudio();
        Destroy(gameObject, f_checkDurationOfParticleAndClip());
    }

    public void GetVariables()
    {
        Prtcl_Arr_toPlayOnStart = GetComponentsInChildren<ParticleSystem>();
        source = GetComponent<AudioSource>();
    }
    void PlayParticleAndAudio()
    {
        PlayAllParticles();
        source.clip = AudClp_toPlayOnStart;
        source.Play();
    }

    float f_checkDurationOfParticleAndClip()
    {
        float previousLongestDuration = 0;
        foreach (ParticleSystem _prtcl in Prtcl_Arr_toPlayOnStart)
        {
            if (_prtcl.main.duration / _prtcl.main.simulationSpeed > previousLongestDuration)
            {
                previousLongestDuration = _prtcl.main.duration / _prtcl.main.simulationSpeed;
            }
        }

        previousLongestDuration = AudClp_toPlayOnStart.length > previousLongestDuration ? AudClp_toPlayOnStart.length : previousLongestDuration;

        return previousLongestDuration;
    }

    void PlayAllParticles()
    {
        foreach (ParticleSystem _prtcl in Prtcl_Arr_toPlayOnStart)
        {
            _prtcl.Play();
        }
    }

}
