using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterFrame : MonoBehaviour
{
    protected int chapterNum;

    protected void ReadData(int sceneNum)
    {
        ChapterManager.instance.ReadTextData(chapterNum, sceneNum);
    }

    protected void PlaceMove()
    {
        StartCoroutine(PlaceMoveCoroutine(chapterNum));
    }

    IEnumerator PlaceMoveCoroutine(int nowChapterNum)
    {
        while (chapterNum == nowChapterNum)
        {
            yield return new WaitForSeconds(0.01f); // 아주잠시 대기해줌
            yield return new WaitUntil(() => !TextPrint.instance.nowTextActive);
            ReadData(3); // 이동 선택지
            yield return new WaitUntil(() => SelectTest.instance.isDestroy);
            switch (SelectTest.instance.selectNumber)
            {
                case 0: // 대장간
                    Debug.LogWarning("대장간으로 이동합니다.");
                    break;
                case 1: // 상점
                    Debug.LogWarning("상점으로 이동합니다.");
                    break;
                case 2: // 여관
                    Debug.LogWarning("여관으로 이동합니다.");
                    break;
                case 3: // 포탈 스테이션
                    Debug.LogWarning("포탈 스테이션으로 이동합니다.");
                    break;
            }
        }
    }
}
