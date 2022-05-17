using Lean.Gui;

public class BasicSubstate : SubState
{
    public LeanToggle leanToggle;
    public int subStateID;
    public override void Ended()
    {

    }
    public override void InProgress()
    {

    }

    public override void ResetEverything()
    {
        int ID = gameObject.GetComponentInParent<States>().StateID;
        StateManager.Instance.SwitchMainState(ID);
        StateManager.Instance.currentIndex = ID;
        leanToggle.TurnOn();
    }
}
