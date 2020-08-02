using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPrint_Chara : MonoBehaviour
{
    public bool active;
    public bool firstUse = false;
        //firstUse가 false이고, 위치값이 설정되지 않았을 경우(999일 경우), 위치값을 Vec3.zero로 함.
    public int nowPos;
    public string charaName;
    public Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        active = false;
    }

    public void ChangeImg(bool _enabled)
    {
        img.enabled = _enabled;
        active = _enabled;
    }

    public void ChangeImg(Sprite _spr, bool _enabled = true)
    {
        if (_spr == null) return;
        img.sprite = _spr;
        ChangeImg(_enabled);
    }

    //중요도에 따라 이미지의 어둡기, 위치를 다르게 만들어주는 함수.
    public void ImgEffect(int _imp = 0)
        //중요도를 받아옵니다. 0이 제일 높고, 수가 커질수록 뒤로 갑니다.
    {
        int important = Mathf.Clamp(_imp, 0, 9);
        var target = img;
        float col = 1f;
        switch (important)
        {
            case 0:
                break;
            default:
                col = 0.2f;
                break;
        }
        target.color = new Color(col, col, col);

        RectTransform tr = GetComponent<RectTransform>();
        Vector3 trans = tr.localPosition;
        GetComponent<RectTransform>().localPosition = new Vector3(trans.x, trans.y, (10 - _imp));
    }

    public void ChangeTransform(int _posInt, Vector3 _pos)
    {
        GetComponent<RectTransform>().anchoredPosition = _pos;
        nowPos = _posInt;
    }
}
