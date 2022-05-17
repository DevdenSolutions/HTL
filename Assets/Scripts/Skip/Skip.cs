using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    public int _stateID;
    [SerializeField] List<GameObject> _skipGOList = new List<GameObject>();
    public SkipData skipData = new SkipData();

    public void SavePositions()
    {
        skipData.SkipID = _stateID;

        if (skipData._copiedGoList != null)
        {
            if (skipData._copiedGoList.Count > 0)
            {
                skipData._copiedGoList.Clear();
            }
        }

        foreach (var x in _skipGOList)
        {
            SkipObject SO = new SkipObject();
            SO.Position.x = x.transform.localPosition.x;
            SO.Position.y = x.transform.localPosition.y;
            SO.Position.z = x.transform.localPosition.z;

            SO.Rotation.x = x.transform.localRotation.eulerAngles.x;
            SO.Rotation.y = x.transform.localRotation.eulerAngles.y;
            SO.Rotation.z = x.transform.localRotation.eulerAngles.z;
            skipData._copiedGoList.Add(SO);
        }
        if (!SkipManager.Instance._dataToSave._skipList.Contains(skipData))
        {
            SkipManager.Instance._dataToSave._skipList.Add(skipData);
        }
    }

    public void LoadPositions()
    {
        if (SkipManager.Instance._RetrievedData != null)
        {
            foreach (var x in SkipManager.Instance._RetrievedData._skipList)
            {
                if (_stateID == x.SkipID)
                {
                    int i = 0;
                    foreach (var y in x._copiedGoList)
                    {
                        _skipGOList[i].transform.localPosition = new Vector3(y.Position.x, y.Position.y, y.Position.z);
                        _skipGOList[i].transform.localRotation = Quaternion.Euler(y.Rotation.x, y.Rotation.y, y.Rotation.z);
                        i++;
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class SkipData
{
    public int SkipID;
    public List<SkipObject> _copiedGoList;
}

[System.Serializable]
public class SkipObject
{

    public Vector3Ser Position = new Vector3Ser();
    public Vector3Ser Rotation = new Vector3Ser();
}

[System.Serializable]
public class Vector3Ser
{
    public float x;
    public float y;
    public float z;
}

