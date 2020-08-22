using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    Image FadeIntroImage;

    float FadeTime = 0f;
    float Fades = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        FadeIntroImage = GameObject.Find("FadeIntroImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
         // FadeIn(); // 밝아지는
          FadeOut(); // 어두워지는
    }

    public void FadeOut()
    {
        FadeTime += Time.deltaTime;
        if (Fades <= 255.0f && FadeTime >= 0.0f)
        {
            Fades += 0.1f;
            FadeIntroImage.color = new Color(0, 0, 0, Fades);
            Debug.Log(Fades);
        }
        else if (Fades <= 0.0f)
        {
            FadeIntroImage.gameObject.SetActive(false);
        }
    }

    public void FadeIn()
    {
        FadeTime += Time.deltaTime;

        if (Fades > 0.0f && FadeTime >= 0.1f)
        {
            Fades -= 0.1f;
            FadeIntroImage.color = new Color(0, 0, 0, Fades);
            FadeTime = 0;
        }
        else if (Fades <= 0.0f)
        {
            FadeIntroImage.gameObject.SetActive(false);
        }
    }

}
