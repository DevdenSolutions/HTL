using System.Collections.Generic;
using UnityEngine;

public interface iRepeat
{
    public void ResetPositions();
    public void ResetFunctions();

    public void MainRepeatFunction()
    {
        this.ResetFunctions();
        this.ResetPositions();
    }
}
