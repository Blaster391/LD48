using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject m_bulletPrefab = null;

    public void Shoot(Vector2 _velocity, int _damage)
    {
        GameObject bullet = Instantiate<GameObject>(m_bulletPrefab, gameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetupBullet(_velocity, gameObject, _damage);
    }
}
