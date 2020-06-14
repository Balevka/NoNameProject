using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
    [SerializeField] private int hpAddScale = 2;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Sostikovka");
            player.GetComponent<Player>().hpChanger(hpAddScale);
            DestroyObject();
        }
    }
    private void DestroyObject()
    {

        Destroy(gameObject);
        Debug.Log("destroyed");
    }
}
