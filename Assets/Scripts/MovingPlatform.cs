using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    [SerializeField] float _speed = 1.0f;

    bool _switching = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_switching == false)
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
        else if(_switching == true)
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);

        if(transform.position == _pointB.position)
            _switching = true;
        else if(transform.position == _pointA.position)
            _switching = false;
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
