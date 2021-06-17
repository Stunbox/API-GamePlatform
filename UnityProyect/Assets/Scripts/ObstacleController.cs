using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rigidBody;
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Destroy(this.gameObject, 5);
    }
    void Update()
    {
        _rigidBody.velocity = new Vector2(Speed * -1, 0);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collision = other.gameObject;
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");
            PlayerController player = collision.GetComponent<PlayerController>();
            player.killPlayer();
            Destroy(this.gameObject);
        }
    }
}
