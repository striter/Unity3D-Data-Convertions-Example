using System.Collections;
using System.Collections.Generic;
using TGameSave;
using UnityEngine;
using UnityEngine.UI;

public class DataLocalSave : MonoBehaviour {
    Text m_CurrentData;
    InputField m_StringField;
    void Awake()
    {
        m_StringField = transform.Find("StringField").GetComponent<InputField>();
        m_StringField.onValueChanged.AddListener(ChangeStringValue);
        m_CurrentData = transform.Find("CurrentData").GetComponent<Text>();
        transform.Find("InsertInteger").GetComponent<Button>().onClick.AddListener(InsertInteger);
        transform.Find("Reset").GetComponent<Button>().onClick.AddListener(OnReset);
        Debug.Log("File Path:" + TGameData<SGameSaveTest>.s_FilePath);
        TGameData<SGameSaveTest>.Init();
        OnValueChange();
    }

    void ChangeStringValue(string value)
    {
        TGameData<SGameSaveTest>.Data.m_Value = value;
        TGameData<SGameSaveTest>.Save();
        OnValueChange();
    }
    void InsertInteger()
    {
        TGameData<SGameSaveTest>.Data.m_Integerss.Add(new SGameTestItem(Random.Range(-5,5)));
        TGameData<SGameSaveTest>.Save();
        OnValueChange();
    }

    void OnReset()
    {
        TGameData<SGameSaveTest>.Reset();
        TGameData<SGameSaveTest>.Save();
        OnValueChange();
    }

    void OnValueChange() => m_CurrentData.text = "CurrentData:"+ TGameData<SGameSaveTest>.Data.GetItemDatas();


    class SGameSaveTest : ISave
    {
        public string m_Value;
        public List<SGameTestItem> m_Integerss;

        public SGameSaveTest()
        {
            m_Value = "Default";
            m_Integerss = new List<SGameTestItem>() { new SGameTestItem(0),new SGameTestItem(1),new SGameTestItem(2)};
        }

        public string GetItemDatas() => m_Value + "," + TDataConvert.Convert(m_Integerss);
        public void DataRecorrect()
        {
        }
    }

    struct SGameTestItem:IDataConvert
    {
        public int m_Value { get; private set; }
        public SGameTestItem(int value)
        {
            m_Value = value;
        }
    }
}
