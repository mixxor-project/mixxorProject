using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTest : MonoBehaviour
{
    //button type의 리스트 생성함.
    //selectButtons로 접근 : 사용자가 클릭한 버튼
    //Inspector 창에서 사이즈와 그 사이즈에 맞는 버튼 개수 할당 필요함
    [SerializeField] List<Button> selectButtons = new List<Button>();
    
    void Start()
    {
        for (int i = 0; i < selectButtons.Count; i++)
        {
            int index = i;
            //검색해보니 람다식으로도 사용가능함. 편한걸로 사용하면 될 듯. (아직은 람다가 익숙하지 않아서 + unity에 미숙해서 delegate 사용.)
            //익명함수(람다식) 사용 
            // selectButtons[i].onClick.AddListener(() => OnKeyPressed(index));


            //onClick함수 안에 파라미터로 clickButton 함수(만듦...) 넣음
            selectButtons[i].onClick.AddListener(delegate
            {
                clickButton(index);
            });
        }
    }

    public void clickButton(int index)
    {
        var buttonIndex = index; //클릭된 button의 index값이 저장되는 변수
        // Debug.Log(buttonIndex);
    }
}
