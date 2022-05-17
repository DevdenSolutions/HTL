using System.Collections.Generic;
using UnityEngine;

public class BaseRepeat : MonoBehaviour, iRepeat
{
    public GameObject[] _gameObjectsToReset;
    [HideInInspector]
    public List<GameObject> _gameObjectCopied = new List<GameObject>();

    private void Start()
    {
        _gameObjectCopied = RepeatManager.Instance.InstantiateGameobjectsAndCopyTransform(_gameObjectsToReset);
    }
    public void ResetFunctions()
    {
        print("Reset Function: " + name);
    }

    public void ResetPositions()
    {
        RepeatManager.Instance.ResetPosition(_gameObjectsToReset, _gameObjectCopied);
    }
}
