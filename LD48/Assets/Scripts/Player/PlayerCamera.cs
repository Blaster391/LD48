using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float m_cameraHeight = 10.0f;

    GameObject m_player;
    PlayerCameraTriggerArea m_triggerArea = null;

    private void Awake()
    {
        GameMaster.RegisterPlayerCamerea(this);
    }

    void Start()
    {
        m_player = GameMaster.GetPlayer();

        transform.position = GetDesiredCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetDesiredCameraPosition();
    }

    Vector3 GetDesiredCameraPosition()
    {
        Vector3 cameraPosition = transform.position;
        if (m_triggerArea != null)
        {
            cameraPosition = m_triggerArea.transform.position;
        }
        else if(m_player != null)
        {
            cameraPosition = m_player.transform.position;
        }

        cameraPosition = new Vector3(cameraPosition.x, cameraPosition.y, m_cameraHeight);
        return cameraPosition;
    }


    public void RegisterTriggerArea(PlayerCameraTriggerArea _triggerArea)
    {
        m_triggerArea = _triggerArea;
    }

    public void ClearTriggerArea()
    {
        m_triggerArea = null;
    }

}
