using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Game stats")]
    public int actualCoins = 0;
    [Header("Physics")]
    public int JumpForce;
    private Rigidbody2D _rigidbody;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private Animator _animator;
    private GameManager _gameManager;
    public bool isGrounded;
    
    public float checkRadius;
    // Start is called before the first frame update
    void Awake(){
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    public void Jump(){
        _rigidbody.AddForce(Vector2.up * JumpForce,ForceMode2D.Impulse);
    }
    void Update(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,groundLayer);
    }
    void LateUpdate()
    {
        _animator.SetFloat("verticalVelocity", _rigidbody.velocity.y);
        _animator.SetBool("isGrounded", isGrounded);
    }
    public void AddCoin(){
        actualCoins++;
        _gameManager.AddCoin();
    }
    public void killPlayer(){
        _gameManager.Losed(actualCoins);
        Destroy(this.gameObject);
    }
}
