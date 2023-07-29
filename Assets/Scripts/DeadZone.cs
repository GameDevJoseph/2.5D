using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] GameObject _respawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
                player.Damage();

            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
                cc.enabled = false;

            other.transform.position = _respawnPosition.transform.position;

            StartCoroutine(CCEnableRoutine(cc));
        }
    }


    IEnumerator CCEnableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
