using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    static GameMaster m_master = null;
    GameObject m_player = null;

    static public GameMaster GetGameMaster()
    {
        if(m_master == null)
        {
            m_master = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        }

        return m_master;
    }

    public static void RegisterPlayer(GameObject _player)
    {
        GetGameMaster().m_player = _player;
    }

    public static GameObject GetPlayer()
    {
        return GetGameMaster().m_player;
    }
}
