using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    

    [SerializeField] private float health = 10;
    [SerializeField] private GameObject healthBar;

    private Rigidbody2D enemyRb;
    private float healthPart;
    
    void Start()
    {
        healthPart = 2 / health;

        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 14)
        {
            
            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - healthPart, 0.2f, 1);
            Debug.Log(healthBar.transform.localScale);
            health--;
            Debug.Log(health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }


        
    }

    



}
