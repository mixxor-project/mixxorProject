using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemStroeManage : Singleton<ItemStroeManage>
{
    List<string> itemList = new List<string>();
    public static ItemStroeManage instance;

    private void Awake()
    {
        instance = this;
        PlayerPrefs.DeleteAll();
    }

    public bool ReadTextData(int _chapterNum, int _sceneNum)
    {
        instance = this;

        string chapterStr = _chapterNum < 10 ? "0" + _chapterNum.ToString() : _chapterNum.ToString();
        string sceneStr = _sceneNum < 10 ? "0" + _sceneNum.ToString() : _sceneNum.ToString();

        //Debug.Log("Data/Chapter" + chapterStr + "/chapter" + chapterStr + "-scene" + sceneStr);
        List<Dictionary<string, object>> data = CSVReader.Read("Data/ItemStore"  + "/chapter" + chapterStr + "-scene" + sceneStr); //파일이름.
        if (data.Count <= 0) return false;
        List<StoreText> texts = new List<StoreText>();
        //data[i]["EXP"] <이런식으로 하면 i번째 exp를 헤더로 가진 녀석이 리턴됨
        for (int i = 0; i < data.Count; i++)
        {
            string postxt = data[i]["Position"].ToString();
            int pos = 0;
            if (postxt != "") pos = int.Parse(postxt);
            StoreText tx = new StoreText(data[i]["Text"].ToString(), data[i]["Character"].ToString(),
                data[i]["Face"].ToString(), pos, data[i]["ItemContent"].ToString(), data[i]["ItemAnswer"].ToString());
            texts.Add(tx);
        }
        StroeTextPrint.instance.TextStart(texts.ToArray());
        return true;
    }

    void ItemClick() // 선택한 아이템의 정보를 뿌려주는 역할
    {

    }

    void SelectItem() // 아이템 구입 버튼 클릭 이벤트
    {

    }

    public void ExampleFun()
    {
        // Button 눌렸을 때 button > itemName > text 가져오는 ...
        var itemName = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().gameObject.transform.Find("ItemName").GetComponent<Text>().text;

        if (overlapChk(itemName)) // 이미 저장되었을 때
        {
            Debug.Log("이미 저장되어있는 아이템입니다.");
        }
        else // 저장되어있지 않을 때 
        {
            string strArr = ""; // 문자열 생성

            foreach(var item in itemList)// 배열과 ','를 번갈아가며 tempStr에 저장
            {
                if (item.Length > 0)
                {
                    strArr = strArr + item;
                    strArr = strArr + ",";
                }
            }

            strArr = strArr + itemName;

            print("아이템이 성공적으로 저장되었습니다.\n저장된 아이템들 : " + strArr); // 저장된 items 보기

            PlayerPrefs.SetString("Data", strArr); // PlyerPrefs에 문자열 형태로 저장
        }
    }

    public bool overlapChk(string itemName)
    {
        if (getItems()) // 값이 있으면
        {
            return itemList.Contains(itemName);
        }
        return false;
    }

    public Boolean getItems()
    {
        itemList.Clear();
        var itemListTmp = PlayerPrefs.GetString("Data").Split(',');

        if (itemListTmp.Length > -1)
        {
            int[] number2 = new int[itemListTmp.Length]; // 문자열 배열의 크기만큼 정수형 배열 생성

            for (int i = 0; i < itemListTmp.Length; i++)
            {
                itemList.Add(itemListTmp[i]);
            }

            return true;
        }
        return false;
    }

    public void saveItem() // 아이템 저장하는 함수
    {
        
    }
}
