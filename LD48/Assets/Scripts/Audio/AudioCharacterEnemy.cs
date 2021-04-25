using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacterEnemy : AudioCharacter
{
    [SerializeField]
    private List<AudioClip> m_enemyAttackClips;
    [SerializeField]
    private List<AudioClip> m_enemyDamaged;

    public void TriggerEnemyDamaged()
    {
        PlayFromList(m_enemyDamaged, 2);
    }

    public void TriggerEnemyAttack()
    {
        PlayFromList(m_enemyAttackClips, 1);
    }
}
