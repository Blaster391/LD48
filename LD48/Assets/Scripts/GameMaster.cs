using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    static GameMaster m_master = null;
    GameObject m_player = null;
    PlayerCamera m_playerCamera = null;
    AudioManager m_audioManager = null;

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

    public static void RegisterAudioManager(AudioManager _manager)
    {
        GetGameMaster().m_audioManager = _manager;
    }

    public static AudioManager GetAudioManager()
    {
        return GetGameMaster().m_audioManager;
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

    public static Vector2 PerpendicularClockwise(this Vector2 vector2)
    {
        return new Vector2(vector2.y, -vector2.x);
    }
}
