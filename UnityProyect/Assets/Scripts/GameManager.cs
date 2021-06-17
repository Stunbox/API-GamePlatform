using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerCoins = 0;
    [SerializeField]private AnimationController animationController;
    [SerializeField]private SpawnController spawnController;
    [SerializeField]private UIController uIController;
    [SerializeField]private NetworkDownloader networkDownloader;
    public void StartGame(){
        animationController.MoveCameraToPlayer();
        spawnController.StartSpawning();
        Debug.Log("Starting Game...");
    }
    public void SetCoins(int value){
        playerCoins = value;
        uIController.UpdateUICoins(playerCoins);
    }
    public void AddCoin(){
        playerCoins++;
        uIController.UpdateUICoins(playerCoins);
    }
    public void Losed(int coins){
        uIController.ShowLoseUI();
    }
    public void BackToHome(){
        SaveCoinsInPrefs();
        networkDownloader.UpdateCoins(playerCoins);
        animationController.MoveCameraToHome();
        spawnController.CancelSpawns();
        uIController.ShowHomeUI();
    }
    public void SaveCoinsInPrefs(){
        PlayerPrefs.SetInt("PlayerCoins",playerCoins);
    }
}
