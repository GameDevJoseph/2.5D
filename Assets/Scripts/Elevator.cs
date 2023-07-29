using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    bool _goingDown = false;

    [SerializeField] Transform _origin, _target;
    [SerializeField] float _speed = 1.0f;

    public void CallElevator()
    {
        _goingDown = !_goingDown;
    }


    void FixedUpdate()
    {
        if(_goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }else if(_goingDown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _origin.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
