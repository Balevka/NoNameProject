using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private int hp = 5;
    [SerializeField]
    private bool breakable = false;
    private Vector2 obstaclePosition;
    [SerializeField]
    public Grid grid;

    private void Start()
    {
        obstaclePosition = new Vector2(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
        if (hp <= 0 && breakable)
        {
            Destroy(gameObject);
            grid.GetComponent<Generation>().obstacleList.Remove(obstaclePosition);
        }

    }
    public void HpLoss()
    {
        if (breakable)
        {
            hp--;
            Debug.Log(hp);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 14:
                HpLoss(); 
                break;
        }
    }
}
