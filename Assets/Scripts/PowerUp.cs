using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PlayerController playerController;
    public float duration = 4f;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        playerController.fireRate = 0.1f;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;


        yield return new WaitForSeconds(duration);

        playerController.fireRate = 0.25f;

        Destroy(gameObject);
    }
}
