using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;
    private Animator _anim;
    private bool _isJumping = false;
    private bool _isWalking = false;
    private bool _isIdle = true;
    private SpriteRenderer _playerSprite;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _anim = GetComponent<Animator>();
        _playerSprite = GetComponent<SpriteRenderer>();

        if(_uiManager == null)
        {
            Debug.LogError("The UI manager is null");
        }
    }

    void Update()
    {
            //physics calculations first then update!
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0 , 0);
        Vector3 velocity = direction * _speed;
        velocity.y = _yVelocity;
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _anim.SetTrigger("Walk");
        } else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _anim.ResetTrigger("Walk");
            Idle();
        }
        if(_controller.isGrounded == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
                StartCoroutine(JumpTrigger());
                Idle();
            }
                
        }
        else if(Input.GetKeyDown(KeyCode.Space) && _canDoubleJump == true)
        {
            _yVelocity += _jumpHeight;
            _canDoubleJump = false;
            StartCoroutine(JumpTrigger());
            Idle();
                
        }     
        _yVelocity -= _gravity;
        _controller.Move(velocity * Time.deltaTime);
        SetDirection();
    }

    public void AddCoins()
    {
        _coins ++;
        _uiManager.UpdateCoinDisplay(_coins);
    }
    
    public void LoseLife()
    {
        _lives--;
        _uiManager.UpdateLives(_lives);

        if(_lives == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator JumpTrigger()
    {
        _anim.SetTrigger("Jump");
        yield return new WaitForSeconds(1f);
        _anim.ResetTrigger("Jump");
    }
    private void Idle()
    {
        _isIdle = true;
        _anim.SetTrigger("Idle");
    }
    private void SetDirection()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            _playerSprite.flipX = true;
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
           _playerSprite.flipX = false; 
        }
    }
}
