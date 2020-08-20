﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("The UI manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
            //physics calculations first then update!
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0 , 0);
        Vector3 velocity = direction * _speed;
        

        if(_controller.isGrounded == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
            
        } 
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) && _canDoubleJump == true)
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }
            
            _yVelocity -= _gravity;
        }
        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
    public void AddCoins()
    {
        _coins ++;
        _uiManager.UpdateCoinDisplay(_coins);
    }
    // public void LoseLife()
    // {
    //     if()
    //     {
    //         lives--;
    //         _uiManager.UpdateLives(lives);  
    //     }
    // }
}