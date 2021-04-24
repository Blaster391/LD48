using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamageReceiver : MonoBehaviour
{
    public abstract void ReceiveDamage(int _damage);
}
