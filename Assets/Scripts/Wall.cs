using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public AudioClip chopSound1;
    public AudioClip chopSound2;

    [SerializeField] private int hp = Random.Range(2, 5);

    private SpriteRenderer spriteRenderer;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 14)
        {

            hp--;
            Debug.Log(hp);
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }



    }
}
