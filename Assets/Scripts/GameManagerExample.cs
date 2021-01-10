using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerExample : MonoBehaviour
{
    public GameObject myItemUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void gameReset() // 새로하기
    {

    }

    void gameSave() // 게임 끄고 나갈 때 정보 저장하는 함수
    {

    }

    void gameStart() // 불어오기
    {

    }

    public void myItemsOpenBtn() // 자신의 아이템 확인하는 
    {
        myItemUI.SetActive(true);

    }
    public void myItemsExitBtn() // 자신의 아이템 확인하는 
    {
        myItemUI.SetActive(false);

    }
}
