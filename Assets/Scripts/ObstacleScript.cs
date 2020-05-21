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
            grid.GetComponent<Generation>().obstacleList.Remove(obstaclePosition);

            Destroy(this);
        }
    }
    public void HpLoss()
    {
        hp--;
    }
}
