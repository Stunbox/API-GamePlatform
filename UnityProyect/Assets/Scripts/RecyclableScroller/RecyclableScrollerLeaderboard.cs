using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;


public class RecyclableScrollerLeaderboard : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;
    
    // Start is called before the first frame update
    private List<User> _usersList = new List<User>();
    private void Awake()
    {
        _recyclableScrollRect.DataSource = this;
    }
    public int GetItemCount()
    {
        return _usersList.Count;
    }
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as LeaderboardCell;
        item.ConfigureCell(_usersList[index], index);
    }
    public void SetUserInfo(List<User> userInfo)
    {
        _usersList = userInfo;

        

        _recyclableScrollRect.Initialize(this);
    }
}
