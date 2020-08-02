using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundImg
{
    public string name;
    public Sprite img;
}

public class ChapterManager : Singleton<ChapterManager>
{
    public static ChapterManager instance;

    public List<BackgroundImg> bgImgList;

    private void Awake()
    {
        instance = this;
    }

    public bool ReadTextData(int _chapterNum, int _sceneNum)
    {
        string chapterStr = _chapterNum < 10 ? "0" + _chapterNum.ToString() : _chapterNum.ToString();
        string sceneStr = _sceneNum < 10 ? "0" + _sceneNum.ToString() : _sceneNum.ToString();
        //Debug.Log("Data/Chapter" + chapterStr + "/chapter" + chapterStr + "-scene" + sceneStr);
        List<Dictionary<string, object>> data = CSVReader.Read("Data/Chapter"+ chapterStr + "/chapter" + chapterStr + "-scene" + sceneStr); //파일이름.
        if (data.Count <= 0) return false;

        List<LeftText> texts = new List<LeftText>();
        //data[i]["EXP"] <이런식으로 하면 i번째 exp를 헤더로 가진 녀석이 리턴됨
        for (int i = 0; i< data.Count; i++)
        {
            string postxt = data[i]["Position"].ToString();
            int pos = 0;
            if (postxt != "") pos = int.Parse(postxt); 
            LeftText tx = new LeftText(data[i]["Text"].ToString(), data[i]["Character"].ToString(),
                data[i]["Face"].ToString(), pos, data[i]["Background"].ToString());
            texts.Add(tx);
        }
        TextPrint.instance.TextStart(texts.ToArray());

        return true;
    }

    
}
