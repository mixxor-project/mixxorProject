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


}
