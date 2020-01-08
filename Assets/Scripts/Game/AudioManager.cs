using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] bool B_isMainMenu = false;

    [SerializeField] AudioClip AudClp_mainMenu;
    [SerializeField] AudioClip AudClp_combat;
    [SerializeField] AudioClip AudClp_puzzle;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source=GetComponent<AudioSource>();
    }

    public void SetPiecePuzzle(bool isPuzzle)
    {
        if(isPuzzle) source.clip=AudClp_puzzle;
        else source.clip=AudClp_combat;
        source.Play();
    }

}
