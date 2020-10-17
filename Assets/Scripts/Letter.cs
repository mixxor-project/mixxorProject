using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    private Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GameObject.FindGameObjectWithTag("PeopleText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myText.text == "(황실에서 온 급한 편지다. 얼른 읽어보자)")
        {
            GameObject.Find("Image").transform.Find("Letter").gameObject.SetActive(true);
        }    

        if(myText.text == "~튜토리얼~")
        {
            GameObject.Find("Image").transform.Find("Letter").gameObject.SetActive(false);
        }
    }
}
