using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roflanium : Item
{
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Sostikovka");            
            player.GetComponent<Player>().roflChanger(2);
            DestroyObject();

        }
    }
    private void DestroyObject()
    {

        Destroy(gameObject);
        Debug.Log("destroyed");
    }
}