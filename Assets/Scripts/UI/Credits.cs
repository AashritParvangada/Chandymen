using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] Animator Anim_flare, Anim_aashrit, Anim_vrushank, Anim_davin, Anim_logo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayCredits());
    }

    void RunFlare()
    {
        Anim_flare.SetTrigger("Flare_Sweep");
    }

    void LogoBang()
    {
        Anim_logo.SetTrigger("Logo_Bang");
    }

    void RunName(Animator _name)
    {
        _name.SetTrigger("Name_Sweep");
    }

    IEnumerator PlayCredits()
    {
        LogoBang();
        yield return new WaitForSeconds(7);
        RunFlare();
        RunName(Anim_aashrit);
        yield return new WaitForSeconds(5);
        RunFlare();
        RunName(Anim_vrushank);
        yield return new WaitForSeconds(5);
        RunFlare();
        RunName(Anim_davin);
        yield return new WaitForSeconds(5);
        FindObjectOfType<Scene_Manager>().SceneChangeInt(0);

    }

}
