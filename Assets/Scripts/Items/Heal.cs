using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
    [SerializeField] private int hpAddScale = 2;
    private Player playerScript;
    
    private void Start()
    {
        
        playerScript = reactGameObject.GetComponent<Player>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript.hp =+ hpAddScale;
        //Логика увелечения здоровья

        Destroy(gameObject);
    }
}
