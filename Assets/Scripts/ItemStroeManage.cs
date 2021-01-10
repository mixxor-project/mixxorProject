using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundImgs
{
    public string name;
    public Sprite img;
}

public class ItemStroeManage : Singleton<ItemStroeManage>
{
    public static ItemStroeManage instance;

    public List<BackgroundImgs> bgImgList;

    private void Awake()
    {
        instance = this;
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
}
