using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] internal GameObject reactGameObject;
    private Collider2D itemCollider;


    void Start()
    {
        itemCollider = GetComponent<Collider2D>();

        if (!itemCollider.isTrigger)
            itemCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
        
    }

}
