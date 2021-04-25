using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{

    [SerializeField]
    GameObject m_object;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            m_object.SetActive(true);
        }
    }

}
