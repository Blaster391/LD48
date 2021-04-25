using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{

    private AudioSource m_audioSource;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(AudioClip _clip)
    {
        m_audioSource.PlayOneShot(_clip);
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
