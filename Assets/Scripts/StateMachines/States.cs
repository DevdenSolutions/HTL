using Lean.Gui;
using UnityEngine;

public class States : MainState
{
    public int StateID;
    public LeanToggle leanToggle;
    public iRepeat _Repeat;
    public Skip _skip;
    public override void Ended()
    {

    }

    public override void InProgress()
    {
        print(gameObject.name);
    }

    public override void Repeat()
    {
        print("Repeating: " + name);
        if (_Repeat != null)
        {
            _Repeat.MainRepeatFunction();
        }
        else
        {
            Debug.LogError("No Repeat included!!");
        }
    }

    public override void ResetEverything()
    {
        leanToggle.TurnOn();
    }

    public override void Skip()
    {

    }

    public override void SavePosition()
    {
        if (_skip != null)
        {
            _skip._stateID = StateID;
            _skip.SavePositions();
        }

        else
        {
            Debug.LogError("Cant Save!! Skip Not Defined");
        }
    }

    public override void LoadSkipPosition()
    {
        if (_skip != null)
        {
            _skip.LoadPositions();
        }

        else
        {
            Debug.LogError("Cant Load!! Skip not defined for this State");

        }
    }
}
