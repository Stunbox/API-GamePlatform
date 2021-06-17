using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;

public class LeaderboardCell : MonoBehaviour, ICell
{
    public TMP_Text name;
    public TMP_Text coins;

    private User _userInfo;
     private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }
    public void ConfigureCell(User userInfo,int cellIndex){
        _userInfo = userInfo;

        name.text = userInfo.name;
        coins.text = userInfo.coins.ToString();
    }
    private void ButtonListener()
    {
        Debug.Log("Button pressed");
    }
}
