using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataConvertions : MonoBehaviour {

    Text m_Elder,m_Newer;
    void Awake()
    {
        m_Elder = transform.Find("Elder").GetComponent<Text>();
        m_Newer = transform.Find("Newer").GetComponent<Text>();

        STest temp = new STest(0,new List<STest1>() {new STest1(10.1f, enum_Test.Invalid),new STest1(10.2f, enum_Test.EnumData1) },new STest2(9,"Test",new STest1(10.3f, enum_Test.EnumData1)));

        string convertString = TDataConvert.Convert(temp);
        m_Elder.text = convertString+temp.GetSomeInnerDatas();
        Debug.Log(convertString);

        convertString =convertString.Replace('9','2').Replace("Test", "New").Replace("EnumData1","EnumData2");

        string newText = convertString + TDataConvert.Convert<STest>(convertString).GetSomeInnerDatas(); 
        m_Newer.text = newText;
        Debug.Log(newText);
    }

    enum enum_Test
    {
        Invalid=-1,
        EnumData1,
        EnumData2,
    }

    struct STest:IDataConvert
    {
        public int m_Index { get; private set; }
        public List<STest1> m_Item1s { get; private set; }
        public STest2 m_Item2 { get; private set; }
        public STest(int index, List<STest1> item1s, STest2 item2)
        {
            m_Index = index;
            m_Item1s = item1s;
            m_Item2 = item2;
        }

        public string GetSomeInnerDatas() => "," + m_Item2.m_Value1 + "," + m_Item2.m_Value2 + "," + m_Item2.m_Item.m_Value2;
    }

    struct STest1 : IDataConvert
    {
        public float m_Value1 { get; private set; }
        public enum_Test m_Value2 { get; private set; }
        public STest1(float value1,enum_Test value2)
        {
            m_Value1 = value1;
            m_Value2 = value2;
        }
    }
    struct STest2:IDataConvert
    {
        public int m_Value1 { get; private set; }
        public string m_Value2 { get; private set; }
        public STest1 m_Item { get; private set; }
        public STest2(int v1, string v2,STest1 item)
        {
            m_Value1 = v1;
            m_Value2 = v2;
            m_Item = item;
        }
    }

}
