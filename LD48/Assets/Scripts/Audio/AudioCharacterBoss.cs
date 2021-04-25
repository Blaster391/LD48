using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacterBoss : AudioCharacter
{
    [SerializeField]
    private List<AudioClip> m_pounceCharging;
    [SerializeField]
    private List<AudioClip> m_pouncing;
    [SerializeField]
    private List<AudioClip> m_spinning;
    [SerializeField]
    private List<AudioClip> m_shotgun;
    [SerializeField]
    private List<AudioClip> m_roar;
    [SerializeField]
    private List<AudioClip> m_damaged;

    [SerializeField]
    private AudioClip m_deathRoar;

    public void TriggerPounceCharge()
    {
        PlayFromList(m_pounceCharging, 2);
    }

    public void TriggerPounce()
    {
        PlayFromList(m_pouncing, 3);
    }

    public void TriggerDamaged()
    {
        PlayFromList(m_damaged, 1);
    }

    public void TriggerShotgun()
    {
        PlayFromList(m_shotgun, 2);
    }

    public void TriggerSpin()
    {
        PlayFromList(m_spinning, 2);
    }

    public void TriggerRoar()
    {
        PlayFromList(m_roar, 10);
    }

    public void TriggerDeathRoar()
    {
        Play(m_deathRoar, 999);
    }
}
