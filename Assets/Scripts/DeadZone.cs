using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Die(other.gameObject);
        }
    }


    public void Die(GameObject player)
    {
        //Alive = false;
        //GameController.gameController.ShowMenu(Alive);
        Destroy(player);
    }
}
