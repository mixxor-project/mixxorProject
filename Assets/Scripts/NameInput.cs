using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public InputField name;

    public void PrintName()
    {
        var _name = name.text;
        Debug.Log(_name);
    }
}
