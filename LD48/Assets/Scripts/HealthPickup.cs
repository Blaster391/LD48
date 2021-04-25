using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> m_pickupFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            PlayerHealth health = GameMaster.GetPlayer().GetComponent<PlayerHealth>();
            if(health.GetMaxHealth() != health.GetCurrentHealth())
            {
                health.IncrementCurrentHealth();
                GameMaster.GetAudioManager().CreateAndPlayAudioObject(m_pickupFX[Random.Range(0, m_pickupFX.Count)]);
                Destroy(gameObject);
            }
        }
    }

}
