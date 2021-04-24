using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    static GameMaster m_master = null;
    GameObject m_player = null;
    PlayerCamera m_playerCamera = null;
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

    public static void RegisterPlayerCamerea(PlayerCamera _camera)
    {
        GetGameMaster().m_playerCamera = _camera;
    }

    public static PlayerCamera GetPlayerCamera()
    {
        return GetGameMaster().m_playerCamera;
    }
}

public static class MathsHelper
{
    public static float RandomWithNegative()
    {
        return Random.Range(-1.0f, 1.0f);
    }

    public static Vector2 RandomWithNegativeVector2()
    {
        return new Vector2(MathsHelper.RandomWithNegative(), MathsHelper.RandomWithNegative());
    }
}
