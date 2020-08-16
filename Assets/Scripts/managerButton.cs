using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class managerButton : MonoBehaviour
{
    Image btnImage;
    //object answer = "button2";
    public void GetBtn()
    {
        GameObject tempBtn = EventSystem.current.currentSelectedGameObject;
        //GameObject test1 = GameObject.Find("button1");
        Debug.Log((object)tempBtn);
        if(tempBtn.CompareTag("Finish"))
        {
            Debug.Log("정답");
        }
        else
        {
            Debug.Log("오답");
        }
        
    }

}
