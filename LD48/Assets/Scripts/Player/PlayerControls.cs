using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool AttackPressed()
    {
        return Input.GetKey(KeyCode.Space);
    }
    public bool UpPressed()
    {
        return Input.GetKey(KeyCode.W);
    }

    public bool DownPressed()
    {
        return Input.GetKey(KeyCode.S);
    }

    public bool LeftPressed()
    {
        return Input.GetKey(KeyCode.A);
    }

    public bool RightPressed()
    {
        return Input.GetKey(KeyCode.D);
    }
}
