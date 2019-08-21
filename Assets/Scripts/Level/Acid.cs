using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    bool b_playerisinAcid, b_playSound = true;
    bool b_damageCoolingdown = false;
    [SerializeField] float F_timeInterval = .1f;
    [SerializeField] int I_damageCaused = 2;
    public AudioClip[] AudClp_Arr_AcidSFX;
    public AudioClip AudClp_AcidAMB;
    PlayerController plCont_playa;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            plCont_playa = other.GetComponent<PlayerController>();
            b_playerisinAcid = true;
        }
    }

    //When the player is in acid, start the damage function.
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !plCont_playa.B_AboveAcid)
        {
            PlayAcidSound();
            TriggerPlayerAcidDamage(plCont_playa);
        }
    }

    //To stop the loop of the player being damaged.
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            b_playerisinAcid = false;
    }

    void PlayAcidSound()
    {
        if (b_playSound)
        {
            int _numberOfClips = 0;
            foreach (AudioClip _audClp in AudClp_Arr_AcidSFX)
            {
                _numberOfClips++;
            }

            int _soundToPlay = Random.Range(0, _numberOfClips);
            GetComponent<AudioSource>().clip = AudClp_Arr_AcidSFX[_soundToPlay];
            GetComponent<AudioSource>().Play();
            b_playSound = false;

            StartCoroutine(WaitForSound(AudClp_Arr_AcidSFX[_soundToPlay]));
        }
    }

    IEnumerator WaitForSound(AudioClip _clip)
    {
        yield return new WaitForSeconds(_clip.length);
        b_playSound = true;
    }


    //Start damaging the player and set a loop.
    void TriggerPlayerAcidDamage(PlayerController playa)
    {
        if (!b_damageCoolingdown)
        {
            if (b_playerisinAcid)
            {
                playa.AcidDamageHealth(I_damageCaused);
                StartCoroutine(AcidCooldown());

            }
        }
    }

    //To damage the player gradually over time.
    IEnumerator AcidCooldown()
    {
        b_damageCoolingdown = true;
        yield return new WaitForSeconds(F_timeInterval);
        b_damageCoolingdown = false;
    }
}