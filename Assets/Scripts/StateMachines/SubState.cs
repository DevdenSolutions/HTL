using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubState : MonoBehaviour
{
    public abstract void ResetEverything();
    public abstract void InProgress();
    public abstract void Ended();
}
