using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettings : MonoBehaviour
{
    public void ShowSettingMenu(GameObject settings)
    {
        settings.SetActive(true);
    }
    public void HideSettingMenu(GameObject settings)
    {
        settings.SetActive(false);
    }
}
