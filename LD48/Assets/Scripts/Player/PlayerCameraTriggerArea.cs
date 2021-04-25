using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTriggerArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            GameMaster.GetPlayerCamera().RegisterTriggerArea(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            if(! GameMaster.GetPlayer().GetComponent<PlayerHealth>().IsDead())
            {
                GameMaster.GetPlayerCamera().ClearTriggerArea();
            }

        }
    }
}
