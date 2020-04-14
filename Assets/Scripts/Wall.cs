using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public AudioClip chopSound1;
    public AudioClip chopSound2;

    [SerializeField]private Sprite damageSprite;
    [SerializeField]private int hp = 4;

    


    private SpriteRenderer spriteRenderer;





    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        spriteRenderer.sprite = damageSprite;
        hp -= loss;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
            
       
    }
}
