using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 10;
    [SerializeField] private GameObject rayCaster;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 14)
        {
            
            health--;
            Debug.Log(health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }


        
    }

    
}
