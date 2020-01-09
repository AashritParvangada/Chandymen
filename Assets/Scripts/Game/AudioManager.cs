using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] bool B_isMainMenu = false;
    [SerializeField] float F_totalFadeTime, F_sourceMaxVolume;
    [SerializeField] AudioClip AudClp_mainMenu;
    [SerializeField] AudioClip AudClp_combat;
    [SerializeField] AudioClip AudClp_puzzle;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetPiecePuzzle(bool isPuzzle)
    {
        StartCoroutine(IEnumFadeAudioToAnotherPiece(isPuzzle, F_totalFadeTime));
    }

    IEnumerator IEnumFadeAudioToAnotherPiece(bool _isPuzzle, float _totalTime)
    {
        float _currentTime = 0;

        while (source.volume > 0)
        {
            _currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(F_sourceMaxVolume, 0, _currentTime / (_totalTime / 2));
            yield return null;
        }

        if (_isPuzzle) source.clip = AudClp_puzzle;
        else source.clip = AudClp_combat;

        source.Play();

        while (source.volume < F_sourceMaxVolume)
        {
            _currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(0, F_sourceMaxVolume, _currentTime / (_totalTime / 2));
            yield return null;
        }
    }
}
