using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharacterImg
{
    public string name;
    public List<CharaFace> faceList;

    #region
    [System.Serializable]
    public struct CharaFace
    {
        public string name;
        public Sprite faceImg;
        public Vector2 faceScale;
        public Vector2 facePos;
        public bool faceFlipX;
    }
    #endregion
}

public class UICanvas : Singleton<UICanvas>
{
    public static UICanvas instance;


    [Header("캐릭터 이미지 리스트")]
    public List<CharacterImg> charaImgList;


    private void Awake()
    {
        instance = this;
    }

    public GameObject CastRay()
    {
        GameObject target = null;
        Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(pos, Mathf.Infinity);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        } 
        return target;
    }
}
