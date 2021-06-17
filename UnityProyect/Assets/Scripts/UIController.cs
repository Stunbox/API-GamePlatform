using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class UIController : MonoBehaviour
{

    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Text IntroText;
    [SerializeField] private TMP_Text UserText;
    [SerializeField] private TMP_Text CoinText;
    private NetworkDownloader networkDownloader;
    private GameManager gameManager;
    public GameObject contentToClear;
    public GameObject form;
    public GameObject playing;
    public GameObject lose;
    [SerializeField] private TMP_Text CoinLoseText;

    // Start is called before the first frame update
    void Start()
    {
        networkDownloader = GameObject.Find("NetworkDownloader").GetComponent<NetworkDownloader>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        CheckUserData();
        
        
    }
    
    public void CheckUserData(){
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            inputName.gameObject.SetActive(false);
            UserText.gameObject.SetActive(true);
            UserText.text = PlayerPrefs.GetString("PlayerName");

            int playerCoins = PlayerPrefs.GetInt("PlayerCoins");
            gameManager.SetCoins(playerCoins);
            CoinText.text = playerCoins.ToString();
            IntroText.text = "Welcome back!";
        }
    }
    public void PlayGame()
    {
        if (inputName.text != "" || PlayerPrefs.HasKey("PlayerName"))
        {
            if (!PlayerPrefs.HasKey("PlayerName"))
            { //
                SubmitForm();
            }
            form.SetActive(false);
            playing.SetActive(true);
            gameManager.StartGame();
        }
        else
        {
            Debug.Log("Fill the input");
        }

    }
    public void UpdateUICoins(int value)
    {
        CoinText.text = value.ToString();
    }
    public void SubmitForm()
    {
        User userToPost = new User();
        userToPost.name = inputName.text;
        userToPost.coins = 0;
        if (!networkDownloader.CheckIfUserRepeat(userToPost.name))
        {
            networkDownloader.UploadUser(userToPost);
            SaveNewPlayerInPlayerPrefs(userToPost.name, userToPost.coins);

        }
        else
        {
            Debug.Log("User Repeated!");
        }
    }
    public void SaveNewPlayerInPlayerPrefs(string name, int coins)
    {
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.SetInt("PlayerCoins", coins);
    }
    public void ClearLeaderboardContent()
    {
        foreach (Transform child in contentToClear.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void ShowLoseUI()
    {
        PlayerController player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();

        CoinLoseText.text = player.actualCoins.ToString();
        playing.SetActive(false);
        lose.SetActive(true);
    }
    public void ShowHomeUI()
    {
        CheckUserData();
        lose.SetActive(false);
        form.SetActive(true);
    }
    public void Jump()
    {
        PlayerController player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        player.Jump();
    }
}
