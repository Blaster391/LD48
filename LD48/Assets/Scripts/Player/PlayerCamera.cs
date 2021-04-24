using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float m_cameraHeight = 10.0f;

    GameObject m_player;

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
        Vector3 cameraPosition = m_player.transform.position;
        cameraPosition = new Vector3(cameraPosition.x, cameraPosition.y, m_cameraHeight);
        return cameraPosition;
    }
}
