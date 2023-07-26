using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _gravity = 1.0f;
    [SerializeField] float _jumpHeight = 15f;
    CharacterController _controller;
    float _yVelocity;
    bool _canDoubleJump = false;

    [SerializeField] int _coinAmount;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal,0,0);
        Vector3 velocity = direction * _speed;

        if(_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(_canDoubleJump)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }
            _yVelocity -= _gravity;
        }
        
        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoin()
    {
        _coinAmount += 1;
        UIManager.Instance.UpdateCoinDisplay(_coinAmount);
    }

}
