using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : PowerUpManager
{

    private new void OnTriggerEnter(Collider other)
    {
        // extends all the functionalities of the original code
        base.OnTriggerEnter(other);
    }
}
