using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddCoin();
                Destroy(gameObject);
            }
        }
    }
}
