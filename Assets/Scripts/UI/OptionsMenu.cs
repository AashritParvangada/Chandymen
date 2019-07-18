using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer AudMix_Mixer;
    public void SetVolume(float _volume)
    {
        AudMix_Mixer.SetFloat("Volume", _volume);
    }

    public void SetQuality(int _qualityIndex)
    {
        Debug.Log("Should set Quality to: " + _qualityIndex); Debug.Log("Quality was: " + QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(_qualityIndex);

        Debug.Log("Quality is now: " + QualitySettings.GetQualityLevel());
    }
}
