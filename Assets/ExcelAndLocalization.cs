using System.Collections;
using System.Collections.Generic;
using TExcel;
using UnityEngine;
using UnityEngine.UI;

public class ExcelAndLocalization : MonoBehaviour {
    UIT_TextExtend m_Normal,m_Format;
    void Awake()
    {
        m_Normal = transform.Find("Normal").GetComponent<UIT_TextExtend>();
        m_Format = transform.Find("Format").GetComponent<UIT_TextExtend>();
        transform.Find("ChangeLanguage").GetComponent<Button>().onClick.AddListener(OnChangeLanguageClick);

        TLocalization.OnLocaleChanged += OnKeyLocalized;
        TLocalization.SetRegion(enum_Option_LanguageRegion.CN);
        m_Normal.autoLocalizeKey = "GAME_TEST_NORMAL";


        Properties<STestProperties>.Init();
        for (int i = 0; i < Properties<STestProperties>.PropertiesList.Count; i++)
            Debug.Log(TDataConvert.Convert(Properties<STestProperties>.PropertiesList[i]));

        SheetProperties<STestSheetProperties>.Init();
        for (int i = 0; i < SheetProperties<STestSheetProperties>.GetPropertiesList(1).Count; i++)
            Debug.Log(TDataConvert.Convert( SheetProperties<STestSheetProperties>.GetPropertiesList(1)[i]));
    }
    void OnDestroy()
    {
        TLocalization.OnLocaleChanged -= OnKeyLocalized;
    }
    void OnChangeLanguageClick()
    {
        TLocalization.SetRegion(TLocalization.e_CurLocation== enum_Option_LanguageRegion.CN? enum_Option_LanguageRegion.EN: enum_Option_LanguageRegion.CN);
    }
    void OnKeyLocalized()
    {
        m_Format.formatText("GAME_TEST_FORMAT",10,TLocalization.GetKeyLocalized("GAME_TEST_ITEM"));
    }

#pragma warning disable 0649
    struct STestProperties :ISExcel,IDataConvert
    {
         int index;
         int value1;
         int value2;
         int value3;
         int value4;
        public void InitAfterSet()
        {
        }
    }
    struct STestSheetProperties : ISExcel, IDataConvert
    {
        int index;
        int value1;
        public void InitAfterSet()
        {
        }
    }
}
