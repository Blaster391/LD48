using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float m_baseAttackSpeed = 1.0f;
    [SerializeField]
    private int m_baseAttackDamage = 1;

    [SerializeField]
    private float m_currentAttackSpeed = 0.0f;
    [SerializeField]
    private int m_currentAttackDamage = 0;

    [SerializeField]
    private GameObject m_bulletPrefab = null;
    private PlayerControls m_controls;


    void Start()
    {
        m_controls = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
