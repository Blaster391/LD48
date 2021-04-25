using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacterPlayer : AudioCharacter
{
    [SerializeField]
    private List<AudioClip> m_playerAttackClips;
    [SerializeField]
    private List<AudioClip> m_playerDamaged;

    public void TriggerPlayerDamaged()
    {
        PlayFromList(m_playerDamaged, 2);
    }

    public void TriggerPlayerAttack()
    {
        PlayFromList(m_playerAttackClips, 1);
    }
}
