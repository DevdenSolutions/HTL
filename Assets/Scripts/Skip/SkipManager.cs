using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SkipManager : MonoBehaviour
{
    #region singleton

    private static SkipManager _instance;

    public static SkipManager Instance { get { return _instance; } }


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



    public DataToSave _dataToSave = new DataToSave();
    public DataToSave _RetrievedData;
    public GameObject _skipCloneParent;
    public Transform _skipParent;

    private readonly string DataPath = "E:\\RaeX\\";
    string FileName = "Positions.raex";

    private void Start()
    {
        RetrieveData();
    }
    public void GetSkipData()
    {
        foreach( Transform x in _skipParent)
        {
            _dataToSave._skipList.Add(x.GetComponent<Skip>().skipData);
        }

        Invoke(nameof(SaveDataInFile), 1f);
    }

    public void SaveDataInFile()
    {

        FileStream fs = new FileStream(DataPath + FileName, FileMode.Create);
        BinaryFormatter binary = new BinaryFormatter();
        binary.Serialize(fs, _dataToSave);
        fs.Close();
    }

    public void RetrieveData()
    {
        if (File.Exists(DataPath + FileName))
        {
            FileStream fs = new FileStream(DataPath + FileName, FileMode.Open, FileAccess.Read);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            _RetrievedData = binaryFormatter.Deserialize(fs) as DataToSave;
            fs.Close();
        }
        else
        {
            Debug.LogError("Position File doesnt Exist");
        }
    }

}

[System.Serializable]
public class DataToSave
{
    public List<SkipData> _skipList;
}