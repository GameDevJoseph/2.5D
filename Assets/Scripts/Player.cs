using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _gravity = 1.0f;
    [SerializeField] float _jumpHeight = 15f;
    [SerializeField] float _doubleJumpHeight = 30f;
    [SerializeField] float _pushPower = 2.0f;
    CharacterController _controller;
    float _yVelocity;
    bool _canDoubleJump = false;
    bool _canWallJump = false;

    [SerializeField] int _coinAmount;
    [SerializeField] int _lives;
    Vector3 _direction, _velocity;
    Vector3 _wallSurfaceNormal;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        UIManager.Instance.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        if(_controller.isGrounded)
        {
            _canWallJump = false;
            _direction = new Vector3(horizontal, 0, 0);
            _velocity = _direction * _speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }else
        {
            if(Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
            {
                if(_canDoubleJump)
                {
                    _yVelocity += _doubleJumpHeight;
                    _canDoubleJump = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
            {
                _yVelocity = _doubleJumpHeight;
                _velocity = _wallSurfaceNormal * _speed;
            }
            _yVelocity -= _gravity;
        }
        
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void AddCoin()
    {
        _coinAmount += 1;
        UIManager.Instance.UpdateCoinDisplay(_coinAmount);
    }

    public void Damage()
    {
        _lives--;

        UIManager.Instance.UpdateLivesDisplay(_lives);

        if(_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int CoinsCollected()
    {
        return _coinAmount;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(_controller.isGrounded == false && hit.transform.CompareTag("Wall"))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }


        //check if grounded and for moving box
        if(_controller.isGrounded == true && hit.transform.CompareTag("Box"))
        {
            //get hit's rigidbody
            Rigidbody box = hit.collider.attachedRigidbody;

            //check if body is null or kinematic if so return
            if (box == null || box.isKinematic)
                return;

            //check if hit move direction y value is less than .3f if so return
            if (hit.moveDirection.y < -0.3f)
                return;

            //assign push direction to new vector move direction
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);

            //assign body velocity to push direction times push power
            box.velocity = pushDir * _pushPower;
        }
    }
}
