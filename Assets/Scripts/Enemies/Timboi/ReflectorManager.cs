using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorManager : MonoBehaviour
{
    List<StaticReflector> BossReflectors = new List<StaticReflector>();
    [SerializeField] float _switchTime;
    private void Start()
    {
        GetBossReflectors();
        ToggleReflectors(false);
    }

    void GetBossReflectors()
    {
        foreach (StaticReflector _refl in FindObjectsOfType<StaticReflector>())
        {
            if (_refl.B_isBossBattle)
                BossReflectors.Add(_refl);
        }
    }
    void ToggleReflectors(bool _On)
    {
        foreach (StaticReflector _statRef in BossReflectors)
        {
            foreach(BoxCollider boxCollider in _statRef.GetComponentInChildren<Animator>().GetComponents<BoxCollider>())
            boxCollider.enabled = _On;
        }
    }

    public void SwitchReflectorMaterials()
    {
        foreach (StaticReflector _statRefl in BossReflectors)
        {
            _statRefl.SwitchMaterials(_switchTime);
        }
        ToggleReflectors(true);
    }

}
