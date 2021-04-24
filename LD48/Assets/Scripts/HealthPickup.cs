using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            PlayerHealth health = GameMaster.GetPlayer().GetComponent<PlayerHealth>();
            if(health.GetMaxHealth() != health.GetCurrentHealth())
            {
                health.IncrementCurrentHealth();
                Destroy(gameObject);
            }
        }
    }

}
