using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1 : ChapterFrame
{
    // Start is called before the first frame update
    void Start()
    {
        chapterNum = 2;

        //ReadData(1);
        ReadData(2);

        PlaceMove(); // 상점, 대장간 등으로 이동
    }

}
