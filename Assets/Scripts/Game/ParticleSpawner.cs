using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    ParticleSystem Prtcl_toPlayOnStart;
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
        Prtcl_toPlayOnStart = GetComponentInChildren<ParticleSystem>();
        source = GetComponent<AudioSource>();
    }
    void PlayParticleAndAudio()
    {
        Prtcl_toPlayOnStart.Play();
        source.clip = AudClp_toPlayOnStart;
        source.Play();
    }

    float f_checkDurationOfParticleAndClip()
    {
        float toReturn = AudClp_toPlayOnStart.length > Prtcl_toPlayOnStart.main.duration / Prtcl_toPlayOnStart.main.simulationSpeed ?
         AudClp_toPlayOnStart.length : Prtcl_toPlayOnStart.main.duration / Prtcl_toPlayOnStart.main.simulationSpeed;

        return toReturn;
    }
}
