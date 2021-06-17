using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<GameObject> obtacles;
    public GameObject coin;
    public Transform spawnPos,spawnPlayer;
    [SerializeField]private GameObject player;

    // Start is called before the first frame update
    public void StartSpawning(){
        Instantiate(player,spawnPlayer.position,Quaternion.identity);
        InvokeRepeating("SpawnObstacle",3,3);
        InvokeRepeating("SpawnCoin",5,10);

    }
    public void SpawnObstacle(){
        Instantiate(obtacles[0],spawnPos.position,Quaternion.identity);
    }
    public void SpawnCoin(){
        Instantiate(coin,spawnPos.position,Quaternion.identity);
    }
    public void CancelSpawns(){
        CancelInvoke();
    }
}
