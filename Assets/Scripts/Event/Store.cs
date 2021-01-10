using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : ItemStoreFrameExample
{
    // Start is called before the first frame update
    void Start()
    {
        chapterNum = 2;

        Debug.Log("hi");
        ReadData(2);
        PlaceMove(); // 상점, 대장간 등으로 이동
    }
}
