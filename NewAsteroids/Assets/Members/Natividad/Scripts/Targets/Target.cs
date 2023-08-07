using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    public AudioClip targetSound;

    public abstract void DoTargetBehavior();
}
