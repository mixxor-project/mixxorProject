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
          //FadeOut(); // 어두워지는
    }

    public void FadeOut()
    {
        Debug.Log("FadeOut() 실행");
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
        Debug.Log("FadeIn() 실행");
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
    /* 코루틴 함수를 실행하는 함수; 이 함수를 호출하면 된다.*/

    public void SelectButtonBackFadeInOut(float alpha, int fadeInOut)
    {
        if (fadeInOut == 1) // 구분 1 일 때 fade in 실행
        {
            StartCoroutine(SelectButtonBackFadeInCor(alpha));
        } 
        else if  (fadeInOut == 0) // 구분 0 일 때 fade out 실행
        {
            StartCoroutine(SelectButtonBackFadeOutCor(alpha));
        }
    }

    /* 버튼이 생성될 때 뒷 배경을 어둡게 하기 위한 코루틴 */
    public IEnumerator SelectButtonBackFadeOutCor(float alpha)
    {
        Debug.Log("SelectButtonBackFadeOutCor() 실행");
        FadeIntroImage.gameObject.SetActive(true); // fadeout 이미지 활성화
        Color color = FadeIntroImage.color;
        float currentTime = 0.0f;

        while (color.a < alpha) // 알파값(투명도) 보다 작을 때까지 반복
        {
            currentTime += Time.deltaTime / Fades; // deltaTime을 Fades(1f)로 나눈 값을 저장
            color.a = Mathf.Lerp(0, 1, currentTime); // Mathf.Lerp(시작점, 종료점, 거리비율) ; 0~1 사이의 currentTime(%)에 해당하는 값을 반환
            FadeIntroImage.color = color; // 이미지에 적용 
            yield return null;
        }
        yield return null;
    }
    /* 버튼이 생성될 때 뒷 배경을 밝게 하기 위한 코루틴 */
    public IEnumerator SelectButtonBackFadeInCor(float alpha)
    {
        Debug.Log("SelectButtonBackFadeInCor() 실행");
        Color color = FadeIntroImage.color;
        float currentTime = 0.0f;

        while (color.a > alpha) // 알파값(투명도) 보다 클 때까지 
        {
            currentTime += Time.deltaTime / Fades; // deltaTime을 Fades(1f)로 나눈 값을 저장
            color.a = Mathf.Lerp(1, 0, currentTime); // Mathf.Lerp(시작점, 종료점, 거리비율) ; 1~0 사이의 currentTime(%)에 해당하는 값을 반환
            FadeIntroImage.color = color; // 이미지에 적용 
            yield return null;
        }
        FadeIntroImage.gameObject.SetActive(false);
        yield return null;
    }
}
