using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool AttackPressed()
    {
        return AttackUpPressed() || AttackDownPressed() || AttackLeftPressed() || AttackRightPressed();
    }

    public bool AttackUpPressed()
    {
        return Input.GetKey(KeyCode.UpArrow);
    }

    public bool AttackDownPressed()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }

    public bool AttackLeftPressed()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }

    public bool AttackRightPressed()
    {
        return Input.GetKey(KeyCode.RightArrow);
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
