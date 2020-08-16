using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class CharaFacePos
{
    public List<facePos> posList;

    [System.Serializable]
    public class facePos
    {
        public string name_notUsed;
        public int pos;
        public RectTransform getPos;
    }

    #region 더이상 사용하지 않는 부분
    //public enum positions
    //{
    //    center,
    //    left,
    //    right,
    //    NumberOfTypes // enum의 개수를 받아오기 위한 값.
    //}

    //public int GetPosEnum(string _posName) //position enum값을 리턴함.
    //{
    //    var result = positions.center;
    //    int posCount = (int)positions.NumberOfTypes;

    //    for (int i = 0; i < posCount; i++)
    //    {
    //        var val = (positions)i;
    //        if (_posName == val.ToString())
    //        {
    //            result = val;
    //            break;
    //        }
    //    }
    //    return result;
    //}
    #endregion

    public Vector3 GetPosition(int _pos)
    {
        Vector3 result = new Vector3(999, 999);
        #region
        for (int i = 0; i < posList.Count; i++)
        {
            if (posList[i].pos == _pos)
            {
                var target = posList[i];
                result = target.getPos.anchoredPosition;
                break;
            }
        }
        #endregion
        return result;
    }
}

[System.Serializable]
public class LeftText
{
    public string text;
    public string charaName;
    public string charaFaceName;
    public int charaPos;
    public string bgName = "";

    public LeftText(string _text, string _charaName = "", string _charaFace = "", int _charaPos = 999, string _bgName =  "")
    {
        text = _text;
        charaName = _charaName;
        charaFaceName = _charaFace;
        charaPos = _charaPos;
        bgName = _bgName;
    }

    public LeftText(bool _bg, string _text, string _bgName)
    {
        text = _text;
        charaName = "";
        charaFaceName = "";
        charaPos = 999;
        bgName = _bgName;
    }
}

public class TextPrint : MonoBehaviour
{
    public static TextPrint instance;

    //public List<Sprite> bgImgList;

    //캐릭터 이미지 설정
    [Header("캐릭터 이미지 리스트")]
    public CharaFacePos charaImgPos;

    //반드시 설정해야하는 부분
    [Header("텍스트 출력")]
    public GameObject textObj;
    public GameObject textCharaNameObj;
    public GameObject textTextObj;
    public GameObject textEndObj;
    [Range(0.00f, 2.00f)]
    public float textSpeed;

    //현재 실행중인 부분
    [ReadOnly]
    public bool nowTextActive = false;
    public List<LeftText> leftTextList = new List<LeftText>();

    //텍스트 부가기능
    [Header("이미지 설정")]
    public GameObject bgObj;
    public GameObject charaParent;
    public TextPrint_Chara origChara;
    public List<TextPrint_Chara> charaList;

    //기능 내에서만 사용되는 부가적인 부분
    string nextText = "";
    Text nowName;
    Text nowText;
    bool now_TextListCoroutine_active = false;
    bool now_TextWriteInvoke_active = false;
    bool now_TextSelect_OK = false;
    int nowWrite_idx = 0;
    bool GetButtonDown;

    private void Awake()
    {
        instance = this;
        if(textTextObj == null)
            textTextObj = textObj.transform.Find("TextUI-text").gameObject;
        if (textEndObj == null)
            textEndObj = textObj.transform.Find("TextUI-end").gameObject;
        nowText = textTextObj.GetComponent<Text>();
        nowName = textCharaNameObj.GetComponent<Text>();
    }

    private void Start() //테스트를 합니다.
    {
        //string[] testTexts = new string[] { "안녕하세요."};
        //TextStart(testTexts);

        //List<LeftText> texts = new List<LeftText>();
        //texts.Add(new LeftText(true, "반갑<굵게>습니다.</굵게>", "구글에서 긁어온 배경"));
        //texts.Add(new LeftText("스푼이 다가옵니다...", "스푼", "", 0));
        //texts.Add(new LeftText("안녕하세요...", "스푼"));
        //texts.Add(new LeftText("내 이름은 스푼입니다...", "스푼", "웃음"));
        //texts.Add(new LeftText("스푼은 늘 행복합니다...", "스푼", "웃음", -1));
        //texts.Add(new LeftText("내 이름은 <강조>포크</강조>입니다...", "포크", "웃음"));
        //texts.Add(new LeftText("포크는 늘 행복합니다...", "포크", "웃음", -1));
        //texts.Add(new LeftText("나이프가 포크 앞에 끼어듭니다...", "나이프", "기본", -1));
        //texts.Add(new LeftText("하지만 당신이 무시하면... 스푼은 화냅니다...", "스푼", "분노", 1));
        //texts.Add(new LeftText("스푼이 화를 냅니다...", "스푼", "분노"));
        //texts.Add(new LeftText("#모두 퇴장"));
        //texts.Add(new LeftText(true, "스푼은 갔습니다...", "구글에서 긁어온 바닷가"));
        //texts.Add(new LeftText("내 이름은 포크입니다...", "포크", "웃음"));
        //texts.Add(new LeftText("나이프가 포크 앞에 끼어듭니다...", "나이프", "기본", -1));
        //texts.Add(new LeftText("#나이프/포크 퇴장"));
        //texts.Add(new LeftText("나이프가 저리 갑니다...", "나이프", "기본", 1));
        //texts.Add(new LeftText("#나이프 퇴장"));
        //TextStart(texts.ToArray());

        //testTexts = new string[] { "값을 한 번 더 넣어봅니다. 으아아아 아아아아 아 아 아 아 아 아 아" };
        //TextStart(testTexts);
    }

    private void Update()
    {
        GetButtonDown = false; //텍스트 출력 이벤트를 중지시킬 때 사용

        if (!nowTextActive) return;

        //버튼 다운 부분
        if (Input.GetMouseButtonDown(0))
        {
            GameObject target = CastRay();
            if(target != null)
            if (target.name == textObj.name)
                GetButtonDown = true;
        }
        else if( Input.GetButtonDown("Jump") ) // 스페이스키
        {
            GetButtonDown = true;
        }

        //텍스트 출력중이 아니면
        if (!now_TextListCoroutine_active) return; 

        if (GetButtonDown)
            TextWriteInvoke_End();
    }

    GameObject CastRay() // 유닛 히트처리 부분. 레이를 쏴서 처리합니다. 
    {
        GameObject target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        { 
            target = hit.collider.gameObject;
        }
        return target;
    }

    public void TextStart(LeftText[] _texts) //LeftText[]을 받고 텍스트 이벤트 자체를 실행시켜주는 함수
    {
        nowTextActive = true;
        for (int i = 0; i < _texts.Length; i++)
        {
            leftTextList.Add(_texts[i]);
        }

        if (!now_TextListCoroutine_active && leftTextList.Count > 0)
        {
            StartCoroutine(TextListCoroutine());
        }
    }

    public void TextStart(string[] _texts) //string[]을 받고 텍스트 이벤트 자체를 실행시켜주는 함수
    {
        nowTextActive = true;
        for(int i= 0; i<_texts.Length; i++)
        {
            var leftT = new LeftText(_texts[i]);
            leftTextList.Add(leftT);
        }

        if(!now_TextListCoroutine_active && leftTextList.Count > 0)
        {
            StartCoroutine(TextListCoroutine());
        }
    }

    IEnumerator TextListCoroutine() //받은 텍스트 리스트를 비워주는 함수
    {
        now_TextListCoroutine_active = true;
        textObj.SetActive(true);
        textEndObj.SetActive(false);

        while (true)
        {
            LeftText nowLeftText = leftTextList[0];

            if(nowLeftText.text.Length >= 1)
            if(nowLeftText.text[0] == '#') //시작 글자가 #일 경우 커맨드 입력으로.
            {
                TextCommandActive(nowLeftText.text);
                leftTextList.RemoveAt(0);
                if (leftTextList.Count <= 0) break;
                continue;
            }

            //배경을 설정합니다.
            BackgroundSet(nowLeftText.bgName);

            //컷씬을 표시합니다.
            var character = CharaOK(nowLeftText.charaName);
            if (character != null)
                FaceImgSet(character, nowLeftText.charaFaceName, nowLeftText.charaPos);

            //텍스트를 출력함.
            if(nowLeftText.charaName != "효과")
                if(nowLeftText.text[0] != '[')
                {
                    nextText = TextCodeCommand(nowLeftText.text);
                    nowName.text = nowLeftText.charaName;
                    nowText.text = "";
                    TextWriteInvoke();
                    yield return new WaitUntil(() => !now_TextWriteInvoke_active);
                    yield return new WaitUntil(() => textEndObj.activeSelf);
                    yield return new WaitUntil(() => GetButtonDown);
                    textEndObj.SetActive(false);
                }
                else //선택지 출력
                {
                    now_TextSelect_OK = false;
                    TextSelectCall(nowLeftText.text);
                    yield return new WaitUntil(() => now_TextSelect_OK);
                }

            leftTextList.RemoveAt(0);
            if (leftTextList.Count <= 0) break;
        }

        now_TextListCoroutine_active = false;
        TextEnd();
    }

    void TextSelectCall(string _text)
    {
        _text = _text.Substring(1, _text.Length - 1);
        char splt = '[';
        string[] texts = _text.Split(splt);
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i] = texts[i].Replace(" ] ", "");
            texts[i] = texts[i].Replace("] ", "");
            texts[i] = texts[i].Replace("]", "");
        }

        Debug.LogWarning("선택지를 출력합니다.");
        for(int i = 0; i < texts.Length; i++)
        {
            Debug.Log(i + "번째 선택지 : \"" + texts[i] + "\"");
        }

        now_TextSelect_OK = true;
    }

    void TextCommandActive(string _command)
    {
        _command = _command.Substring(1, _command.Length - 1);
        char splt = ' ';
        string[] comnd = _command.Split(splt);
        char splt2 = '/';
        string[] target = comnd[0].Split(splt2);

        switch (comnd[1])
        {
            case "퇴장":
                if (target[0] == "모두")
                    AllFaceImgSet();
                else
                    AllFaceImgSet(target);
                break;
        }

        Debug.LogWarning(_command +" 커맨드가 실행되었습니다.");
    }

    void BackgroundSet(string _bgName)
    {
        if (_bgName == "") return;

        for(int i = 0; i < ChapterManager.instance.bgImgList.Count; i++)
        {
            if(ChapterManager.instance.bgImgList[i].name == _bgName)
            {
                bgObj.GetComponent<SpriteRenderer>().sprite = ChapterManager.instance.bgImgList[i].img;
                Debug.Log("["  +_bgName + "]로 배경이 바뀌었습니다.");
                return;
            }
        }
    }

    CharacterImg CharaOK(string _charaName)
    {
        if (_charaName == "") return null;
        CharacterImg result = null;
        for (int i = 0; i < UICanvas.instance.charaImgList.Count; i++)
        {
            if(UICanvas.instance.charaImgList[i].name == _charaName)
            {
                result = UICanvas.instance.charaImgList[i];
                break;
            }
        }
        if (result == null) Debug.Log(_charaName + " 라는 이름의 캐릭터가 없습니다.");
        return result;
    }

    // 모든 캐릭터의 이미지를 비활성화 시킵니다.
    void AllFaceImgSet()
    {
        for (int i = 0; i < charaList.Count; i++)
        {
            charaList[i].ChangeImg(false);
        }
    }

    // 특정 캐릭터의 이미지를 비활성화 시킵니다.
    void AllFaceImgSet(string[] _activeNameList)
    {
        for (int i = 0; i < charaList.Count; i++)
        {
            for(int j = 0; j < _activeNameList.Length; j++)
            {
                if(charaList[i].charaName == _activeNameList[j])
                {
                    charaList[i].ChangeImg(false);
                }
            }
        }
    }

    void FaceImgSet(CharacterImg _character, string _faceName, int _pos)
    {
        Sprite faceSprite = _character.faceList[0].faceImg;
            //아무런 스프라이트가 없을 경우, 기본적으로 0번에 있는 face를 리턴.
        for(int i = 0; i< _character.faceList.Count; i++)
        {
            var target = _character.faceList[i];
            if (target.name == _faceName)
            {
                faceSprite = target.faceImg;
                break;
            }
        }

        if (faceSprite == null)
        {
            Debug.LogWarning(_character.name + "라는 이름의 캐릭터도, "
                + _faceName +"라는 페이스 이미지도 없습니다.");
            return;
        }

        TextPrint_Chara targetChara = FindChara(_character.name);
        if (targetChara == null) return; //버그방지용
        targetChara.ChangeImg(faceSprite);
        if (!targetChara.firstUse && _pos == 999)  _pos = 0;
        NoDupliLocation(_pos, targetChara.charaName);
        Vector3 nextPos = charaImgPos.GetPosition(_pos);
        if(nextPos != new Vector3(999, 999)) //Vector3 999, 999는 위치값이 존재하지 않다는 뜻
            targetChara.ChangeTransform(_pos, nextPos);

        for(int i = 0; i < charaList.Count; i++)
        {
            if (charaList[i] == targetChara)
                charaList[i].ImgEffect();
            else
                charaList[i].ImgEffect(Mathf.Abs(charaList[i].nowPos) + 1);
        }
    }

    TextPrint_Chara FindChara(string _charaName)
    {
        for(int i = 0; i < charaList.Count; i++)
        {
            if(charaList[i].charaName == _charaName)
            {
                if(!charaList[i].firstUse)
                    charaList[i].firstUse = true;
                return charaList[i];
            }
        }

        Debug.Log(_charaName + " (이)라는 이름을 가진 캐릭터 객체가 없어 따로 생성합니다...");

        GameObject newObj = Instantiate(origChara.gameObject, Vector3.zero, Quaternion.identity);
        newObj.name = "[" + _charaName + "]";
        newObj.transform.SetParent(charaParent.transform);
        TextPrint_Chara newChara = newObj.GetComponent<TextPrint_Chara>();
        newChara.charaName = _charaName;
        charaList.Add(newChara);

        return newChara;
    }

    void NoDupliLocation(int _pos, string _charaName)
    {
        //페이스 이미지가 둘 이상 겹치는 걸 방지하는 함수.
        //겹치는 이미지가 없을 때까지 무한 반복함.
        //겹치는 이미지가 3장 이상을 경우 charaList상 제일 위에 있는 거부터 남길...듯?
        for (int i = 0; i < charaList.Count; i++)
        {
            if (charaList[i].charaName == _charaName ||
                charaList[i].nowPos != _pos)
                continue;

            if (_pos == 0)
            {
                charaList[i].ChangeImg(false);
            }
            else //0만 아니라면
            {
                int add = _pos < 0 ? -1 : 1;
                Vector3 tf = charaImgPos.GetPosition(_pos + add);
                if (tf == Vector3.zero)
                    charaList[i].ChangeImg(false);
                else
                {
                    NoDupliLocation(_pos + add, charaList[i].charaName);
                    charaList[i].ChangeTransform(_pos + add, tf);
                }
            }
        }
    }

    string TextCodeCommand(string _origText)
    {
        string result = _origText;

        string commend = "";
        bool swc = false;

        for( int i = 0; i < result.Length; i++)
        {
            switch (result[i])
            {
                case '<':
                    swc = true;
                    commend = "";
                    break;
            }

            if(swc)
            {
                commend += result[i];
            }

            switch (result[i])
            {
                case '>':
                    swc = false;
                    result = result.Replace(commend, TextCode(commend));
                    break;
            }
        }

        return result;
    }

    string TextCode(string _command) //실제 코드 작동
    {
        string result = "";
        bool block = false;
        bool shut = false;
        if(_command[0] == '<' && _command[_command.Length - 1] == '>')
        {
            block = true;
            _command = _command.Substring(1, _command.Length - 2);
        }
        if(_command[0] == '/')
        {
            shut = true;
            _command = _command.Substring(1, _command.Length - 1);
        }

        switch (_command)
        {
            case "굵게":
                result = "b";
                break;
            case "강조":
                result = shut ? "color" : "color=#ffff00";
                break;
        }

        if (result == "") result = _command;
        if (shut) result = '/' + result;
        if (block) result = '<' + result + '>';
        return result;
    }

    void TextWriteInvoke() //텍스트를 정말로 Text컴포넌트에 넣어주는 부분
    {
        now_TextWriteInvoke_active = true;
        nowText.text += AddText();
        bool isItAllWrite = nowWrite_idx == nextText.Length;

        if ( !isItAllWrite ) //모두 다 작성되지 않았다면
        {
            Invoke("TextWriteInvoke", textSpeed); //Invoke를 사용해 무한반복
        }
        else //모두 다 작성됐다면
        {
            Invoke("TextWriteInvoke_End", textSpeed * 1.5f); //잠시 후 End 함수를 출력
        }
    }

    string AddText()
    {
        string next = "";
        char add;
        bool loop = false;

        do
        {
            if (nowWrite_idx == nextText.Length)
                break;

            add = nextText[nowWrite_idx];
            next += add;
            nowWrite_idx++;

            if (add == '<')
            {
                loop = true;
            }
            else if (add == '>')
            {
                loop = false;
                next += AddText(); //무한반복 주의
            }

        } while (loop);


        return next;
    }

    void TextWriteInvoke_End()
    {
        CancelInvoke("TextWriteInvoke");
        nowText.text = nextText;
        nowWrite_idx = 0;
        textEndObj.SetActive(true);
        now_TextWriteInvoke_active = false;
    }

    void TextEnd()
    {
        textObj.SetActive(false);
        nowTextActive = false;
    }



}
