using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    public struct Key
    {
        public Key(string _id, Color _colour)
        {
            m_keyId = _id;
            m_keyColour = _colour;
        }

        public string m_keyId;
        public Color m_keyColour;
    }

    private List<Key> m_keys = new List<Key>();

    public List<Key> GetKeys()
    {
        return m_keys;
    }

    public bool HasKey(string _id)
    {
        return (m_keys.Where(x => x.m_keyId == _id).Count() > 0);
    }

    public void RemoveKey(string _id)
    {
        m_keys = m_keys.Where(x => x.m_keyId != _id).ToList();
    }


    public void RegisterKey(string _id, Color _colour)
    {
        m_keys.Add(new Key( _id,  _colour));
    }
}
