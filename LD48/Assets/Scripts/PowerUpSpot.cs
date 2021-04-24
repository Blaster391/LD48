using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpTypes
{
    Health,
    Speed,
    AttackDamage,
    AttackSpeed,
    AttackCount,
    Accuracy,

    // Also negatives
    HealthDown,
    SpeedDown,
    AttackDamageDown,
    AttackSpeedDown,
    AccuracyDown
}

public class PowerUpSpot : MonoBehaviour
{
    [SerializeField]
    private List<PowerUpTypes> m_goodPowerups = new List<PowerUpTypes>();
    [SerializeField]
    private List<PowerUpTypes> m_badPowerups = new List<PowerUpTypes>();
    [SerializeField]
    private float m_successChance = 1.0f;


    [SerializeField]
    private Sprite m_successSprite = null;
    [SerializeField]
    private Sprite m_failSprite = null;

    [SerializeField]
    private GameObject m_otherSpot = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            TriggerPowerup();
        }
    }

    private void TriggerPowerup()
    {
        PlayerPowerups playerPowerups = GameMaster.GetPlayer().GetComponent<PlayerPowerups>();

        if (m_successChance >= Random.value)
        {
            if(m_goodPowerups.Count > 0)
            {
                playerPowerups.HandlePowerup(m_goodPowerups[Random.Range(0, m_goodPowerups.Count)]);
            }


            GetComponent<SpriteRenderer>().sprite = m_successSprite;
        }
        else
        {
            if (m_badPowerups.Count > 0)
            {
                playerPowerups.HandlePowerup(m_badPowerups[Random.Range(0, m_badPowerups.Count)]);
            }

            GetComponent<SpriteRenderer>().sprite = m_failSprite;
        }

        if(m_otherSpot != null)
        {
            m_otherSpot.GetComponent<Fader>().StartFade();
        }
    }
}
