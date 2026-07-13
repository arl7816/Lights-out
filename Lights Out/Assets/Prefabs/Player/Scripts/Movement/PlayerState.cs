using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerState : MonoBehaviour
{
    public bool facingRight = true;
    public bool isDashing = false;

    public event Action jumpStarted;
    public void notifyStartJump(){jumpStarted?.Invoke();}

    public event Action playerFlipped;
    public void notifyPlayerFlipped() { playerFlipped?.Invoke(); }
}
