using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] MeshRenderer _elevatorLight;
    [SerializeField] int _requiredCoins = 8;
    [SerializeField] Elevator _elevator;
    bool _elevatorCalled = false;

    private void Start()
    {
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();

        if(_elevator == null)
            Debug.LogError("Elevator is null");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && player.CoinsCollected() >= _requiredCoins)
                {
                    if(_elevatorCalled == true)
                        _elevatorLight.material.color = Color.red;
                    else
                    {
                        _elevatorLight.material.color = Color.green;
                        _elevatorCalled = true;
                    }
                    _elevator.CallElevator();
                }
            }
        }
    }
}
