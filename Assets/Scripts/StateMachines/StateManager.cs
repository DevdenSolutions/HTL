using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region Singleton
    private static StateManager _instance;

    public static StateManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private MainState currentState;

    [SerializeField]
    List<MainState> mainStates;

    [SerializeField]
    List<SubState> subStates;
    [SerializeField] GameObject _RepeatParent;

    [SerializeField] 
    public int currentIndex = 0;
    int currentSubIndex = 0;

    public string CurrentMainStateName;
    public string CurrentSubStateName;

    public MainState _currentMainState;
    public SubState _currentSubState;
    public MainState CurrentState
    {
        get => currentState;


        set
        {
            if (currentState != null)
            {
                currentState.Ended();
            }

            currentState = value;
            _currentMainState = currentState;
            currentState.ResetEverything();
        }
    }

    private SubState currentSubState;
    public SubState CurrentSubState
    {
        get => currentSubState;

        set
        {
            if (currentSubState != null)
            {
                currentSubState.Ended();
            }

            currentSubState = value;
            _currentSubState = currentSubState;

            currentSubState.ResetEverything();
        }
    }

    [System.Obsolete]
    private void Start()
    {
        StateEvent(0);
        PopulateSubStateList();
        SubStateEvent(0);
        PopulateRepeatFunction();
        PopulateSkipFunctions();
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.InProgress();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GoPreviousSub();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GoNextSub();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GoPrevious();
            SwitchSubState(_currentMainState.transform.GetChild(0).GetComponent<BasicSubstate>().subStateID);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GoNext();
            SwitchSubState(_currentMainState.transform.GetChild(0).GetComponent<BasicSubstate>().subStateID);
        }

        CurrentMainStateName = CurrentState.name;
        CurrentSubStateName = CurrentSubState.name;

    }

    [System.Obsolete]
    void PopulateSubStateList()
    {
        int index = 0;
        foreach (var x in mainStates)
        {
            int childcount = 0;
            childcount = x.transform.childCount;

            for (int i = 0; i < childcount; i++)
            {
                if (x.transform.GetChild(i).GetComponent<SubState>() != null)
                {
                    subStates.Add(x.transform.GetChild(i).GetComponent<SubState>());
                    x.transform.GetChild(i).GetComponent<BasicSubstate>().subStateID = index;
                    index++;
                }
            }
        }
    }

    void PopulateRepeatFunction()
    {
        int i = 0;
        foreach(Transform x in _RepeatParent.transform)
        {
            mainStates[i].GetComponent<States>()._Repeat = x.GetComponent<iRepeat>();
            i++;
        }
    }

    void PopulateSkipFunctions()
    {
        int i = 0;
        foreach( Transform x in SkipManager.Instance._skipParent.transform)
        {
            mainStates[i].GetComponent<States>()._skip = x.GetComponent<Skip>();
            i++;
        }
    }
    void StateEvent(int ID)
    {
        CurrentState = mainStates[ID];
    }

    void SubStateEvent(int ID)
    {
        CurrentSubState = subStates[ID];
        currentSubIndex = ID;
    }

    #region PublicFunctions

    public void GoNext()
    {
        if (currentIndex < mainStates.Count - 1)
        {
            currentIndex++;
            StateEvent(currentIndex);
        }

        _currentMainState.LoadSkipPosition();
    }

    public void GoNextSub()
    {
        if (currentSubIndex < subStates.Count - 1)
        {
            currentSubIndex++;
            SubStateEvent(currentSubIndex);
        }

    }

    public void GoPrevious()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            StateEvent(currentIndex);
        }
    }

    public void GoPreviousSub()
    {
        if (currentSubIndex > 0)
        {
            currentSubIndex--;
            SubStateEvent(currentSubIndex);
        }
    }

    public void SwitchMainState(int ID)
    {
        if (currentIndex != ID)
        {
            StateEvent(ID);
        }
    }

    public void SwitchSubState(int ID)
    {
        if (currentSubIndex != ID)
        {
            SubStateEvent(ID);
            
        }
    }

    public void RepeatMainState()
    {
        _currentMainState.Repeat();
    }

    public void SaveSkipPosition()
    {
        _currentMainState.SavePosition();
    }

    public void LoadSkipPosition()
    {
        _currentMainState.LoadSkipPosition();
    }
    #endregion
}
