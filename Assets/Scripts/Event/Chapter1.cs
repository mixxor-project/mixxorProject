using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1 : MonoBehaviour
{
    int chapterNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        ChapterManager.instance.ReadTextData(2, 1);
        ChapterManager.instance.ReadTextData(2, 2);
        /*
        ChapterManager.instance.ReadTextData(chapterNum, 1);
        ChapterManager.instance.ReadTextData(chapterNum, 2);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
