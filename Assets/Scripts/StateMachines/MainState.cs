using UnityEngine;

public abstract class MainState : MonoBehaviour
{
    public abstract void ResetEverything();
    public abstract void InProgress();
    public abstract void Ended();

    public abstract void Repeat();

    public abstract void Skip();

    public abstract void SavePosition();

    public abstract void LoadSkipPosition();
}
