using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTest : MonoBehaviour
{
    public static SelectTest instance;
    private void Awake()
    {
        instance = this;
    }

    public RectTransform Parent;
    public GameObject buttonPre;
    public int selectNumber;
    List<string> selectList = new List<string> (); //선택지 텍스트 삽입용
    public bool isDestroy = false;
    public GameObject selectButton;


    public void ButtonGenerate(string[] btnTxts, int y = 100) // ↑위 과정을 한 번에 해주는 같은 이름의 함수.
    {
        isDestroy = false;
        ButtonText(btnTxts);
        ButtonGenerate(btnTxts.Length, y);
    }

    void ButtonGenerate(int buttonCount, int y)
    {
        GameObject.Find("FadeIntroImage").GetComponent<FadeInOut>().SelectButtonBackFadeInOut(0.5f,0); // 버튼 생성될 때 fade out 하는 함수
        for (int i = 0; i < buttonCount; i++)
        {
            y -= 20;
            selectButton = (GameObject)Instantiate(buttonPre); //프리팹인스턴스
            selectButton.transform.SetParent(Parent, false);//지정한 패널(캔버스) 위로 올라감.
            selectButton.transform.localPosition = new Vector3(0, y, 0); //버튼 위치 설정.
            selectButton.GetComponentInChildren<Text>().text = selectList[i];
            y -= 100; //버튼 위치로 y값 변경
            Button exButton = selectButton.GetComponent<Button>();

            int buttonIndex = i; // 버튼 인덱스 값 저장.
            exButton.onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }

    void ButtonClicked(int buttonIndex)
    {
        // Debug.Log(buttonIndex);
        selectNumber = buttonIndex;
        isDestroy = true;
        if (isDestroy == true)
        {
            foreach(Transform child in Parent)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject.Find("FadeIntroImage").GetComponent<FadeInOut>().SelectButtonBackFadeInOut(0.5f,1); // 버튼 제거될 때 fade in 하는 함수
        selectList.Clear();
        // 후에 스크립트 재사용을 위해서 selectList를 비워줘야 합니다. selectList.Clear(); 였나 아마 맞을거예요(?)
    }

    void ButtonText(string[] answers) // string[] answers <= 같은 식으로 받아주시면 더 좋을거 같습니다
    {
        for (int i = 0; i < answers.Length; i++)
        {
            selectList.Add(answers[i]);
        }
    }

}
