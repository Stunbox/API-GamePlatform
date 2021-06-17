using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDownloader : MonoBehaviour
{

    public string urlLeaderboard = "http://localhost:3050/users";
    public string urlPostUser = "http://localhost:3050/users/add";

    public string urlPutUser = "http://localhost:3050/users/";

    public List<User> usersList;
    void Start()
    {
        StartCoroutine(GetJsonInfo(urlLeaderboard, true));
    }
    public void GetJsonLeaderboard()
    {
        StartCoroutine(GetJsonInfo(urlLeaderboard, false));
    }

    public void setInformationDataSetByJson(string jsonInfo, bool OnlyUserList)
    {
        usersList = new List<User>();
        User[] currentUsers = JsonHelper.FromJson<User>(jsonInfo);
        User[] element = currentUsers;
        usersList.AddRange(element);
        usersList.Sort((x, y) => y.coins.CompareTo(x.coins));

        if (!OnlyUserList)
        {
            RecyclableScrollerLeaderboard scrollerLeaderboard = GameObject.Find("UIController").GetComponent<RecyclableScrollerLeaderboard>();
            scrollerLeaderboard.SetUserInfo(usersList);
        }

    }
    public bool CheckIfUserRepeat(string name)
    {
        foreach (User user in usersList)
        {
            if (user.name == name)
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator GetJsonInfo(string uri, bool OnlyUserList)
    {
        //https://homeinteriors.s3-us-west-2.amazonaws.com/showroom-360-home-interiors/test.json
        //https://firebasestorage.googleapis.com/v0/b/fusevictoriatest.appspot.com/o/escenas_hi_2021-04-16.json?alt=media&token=914bbad7-bc7a-480f-9f80-627d22d12c60
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {

                var jsonInfo = webRequest.downloadHandler.text;
                Debug.Log(pages[page] + ":\nReceived: " + jsonInfo);
                setInformationDataSetByJson(jsonInfo, OnlyUserList);
            }
        }

    }
    public void UploadUser(User user)
    {
        StartCoroutine(Upload(user));
    }
    IEnumerator Upload(User user)
    {
        /*WWWForm form = new WWWForm();
        form.AddField("name", userName);
        form.AddField("lvl", 0);*/
        var jsonData = JsonUtility.ToJson(user);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(urlPostUser, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    public void UpdateCoins(int coins)
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        int id = -1;
        foreach (User player in usersList)
        {
            if (playerName == player.name)
            {
                id = player.id;
            }
        }
        if (id != -1)
        {
            string urlPutUserCustom = urlPutUser + id.ToString();

            User user = new User();
            user.id = id;
            user.name = playerName;
            user.coins = coins;
            StartCoroutine(UpdateUser(user, urlPutUserCustom));
        }else{
            Debug.Log("No player founded");
        }


    }
    IEnumerator UpdateUser(User user, string urlCustom)
    {
        var jsonData = JsonUtility.ToJson(user);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Put(urlCustom, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("User updated!");
            }
        }
    }
}
