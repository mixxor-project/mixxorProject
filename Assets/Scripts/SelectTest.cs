using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTest : MonoBehaviour
{
    public RectTransform Parent;
    public GameObject buttonPre;
    public int selectNumber;
    List<string> selectList = new List<string> ();//선택지 텍스트 삽입용
    public bool isDestroy = false;
    public GameObject selectButton;

    void Start()
    {
        int buttonCount = 3;
        int y = 100; //버튼 위치 조정
        

        //텍스트 변경에 아이디어가 떠오르지 않아 우선 switch문으로 값을 변경해줌...
        switch (selectNumber)
        {
            case 1:
                ButtonText("선택1", "선택2", "선택3");
                break;
            case 2:
                ButtonText("답1", "답2", "답3");
                break;
            default:
                ButtonText("선택지 삽입X", "선택지 삽입X", "선택지 삽입X");
                break;
        }
        
        for (int i = 0; i < buttonCount; i++)
        {
            selectButton = (GameObject)Instantiate(buttonPre); //프리팹인스턴스
            selectButton.transform.SetParent(Parent, false);//지정한 패널(캔버스) 위로 올라감.
            selectButton.transform.localScale = new Vector3(2, 2, 1); //버튼 크기 설정.
            selectButton.transform.localPosition = new Vector3(0, y, 0); //버튼 위치 설정.
            selectButton.GetComponentInChildren<Text>().text = selectList[i]; 
            y -= 100; //버튼 위치로 y값 변경
            Button exButton = selectButton.GetComponent<Button>();
            
            int buttonIndex = i; // 버튼 인덱스 값 저장.
            exButton.onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }//start


    void ButtonClicked(int buttonIndex)
    {
        Debug.Log(buttonIndex);
        isDestroy = true;
        if (isDestroy == true)
        {
            foreach(Transform child in Parent)
            {
                Destroy(child.gameObject);
            }
        }
    }


    void ButtonText(string answer1,string answer2,string answer3)
    {
        selectList.Add(answer1);
        selectList.Add(answer2);
        selectList.Add(answer3);
    }

}
