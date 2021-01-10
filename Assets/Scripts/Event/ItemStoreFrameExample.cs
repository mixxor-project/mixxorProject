using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemStoreFrameExample : ItemStroeManage
{
    protected int chapterNum;

    protected void ReadData(int sceneNum)
    {
        ReadTextData(chapterNum, 1);
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
            yield return new WaitUntil(() => !StroeTextPrint.instance.nowTextActive);
            yield return new WaitUntil(() => SelectItemButton.instance.isDestroy);
            switch (SelectItemButton.instance.selectNumber)
            {
                case 0: // 대장간
                    Debug.LogWarning("대장간으로 이동합니다.");
                    break;
                case 1: // 상점
                    Debug.LogWarning("상점으로 이동합니다.");
                    SceneManager.LoadScene("ItemStore");
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
