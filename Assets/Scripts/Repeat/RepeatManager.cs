using System.Collections.Generic;
using UnityEngine;
public class RepeatManager : MonoBehaviour
{

    #region singleton

    private static RepeatManager _instance;

    public static RepeatManager Instance { get { return _instance; } }


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

    [SerializeField] GameObject _cloneParent;

    public void RunRepeat(iRepeat iRepeat)
    {
        iRepeat.MainRepeatFunction();
    }

    public List<GameObject> InstantiateGameobjectsAndCopyTransform(GameObject[] gameObjectsToReset)
    {
        List<GameObject> gameObjectsToCopy = new List<GameObject>();
        foreach (var x in gameObjectsToReset)
        {
            GameObject GO = new GameObject(x.name + "_TempClones");
            GO.transform.SetParent(_cloneParent.transform);
            GO.transform.localPosition = x.transform.localPosition;
            GO.transform.localRotation = x.transform.localRotation;
            gameObjectsToCopy.Add(GO);
        }

        return gameObjectsToCopy;
    }

    public void ResetPosition(GameObject[] _gameObjectsToReset, List<GameObject> _gameObjectCopied)
    {
        int i = 0;
        if (_gameObjectsToReset.Length != 0)
        {
            foreach (var x in _gameObjectsToReset)
            {
                x.transform.localPosition = _gameObjectCopied[i].transform.localPosition;
                x.transform.localRotation = _gameObjectCopied[i].transform.localRotation;
                i++;

                UnityEngine.Debug.Log("Called Repeat Function");
            }
        }

    }
}
