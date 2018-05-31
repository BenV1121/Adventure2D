using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    PlayerController player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject.GetComponent<PlayerController>();

            player.health += 5;
            player.GetComponent<PlayerController>().SetHealthAmount();

            Destroy(gameObject);
        }
    }
}
