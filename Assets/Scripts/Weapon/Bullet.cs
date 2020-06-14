using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private GameObject hitFX;




    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject ef = Instantiate(hitFX, transform.position, Quaternion.identity);
        Destroy(ef, 0.1f);
        Destroy(gameObject);
    }
}
