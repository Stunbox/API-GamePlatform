using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float Speed;
    void Update()
    {
        this.transform.Translate(Vector3.left * Time.deltaTime * Speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collision = other.gameObject;
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");
            PlayerController player = collision.GetComponent<PlayerController>();
            player.AddCoin();
            Destroy(this.gameObject);
        }
    }
}
