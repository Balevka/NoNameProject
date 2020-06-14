using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] internal Animator playerAnimator;
    private Rigidbody2D rb;
    private int maxHp = 20;
    private int hp = 10;
    public Canvas GUI;
    public Canvas Loss;
    public Canvas Pause;
    public Image hpBar;
   
    private Vector2 movement;
    private Vector2 mousePos;
    [SerializeField] private Camera cam;

    public int Hp { get => hp; set => hp = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        rb.velocity = Movement();
        if(hp == 0)
        {
            PlayerDead();
        }
    }
    public void PlayerDead()
    {

        Time.timeScale = 0f;
        GUI.enabled = false;
        Loss.enabled = true;
        Pause.enabled = false;
    }

    private Vector2 Movement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
                
        if(movement.x < 0)
        {
            
        }
        return new Vector2(movement.x * speed, movement.y * speed);
        
    }


    private float Rotation()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        return angle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exit")) 
            Application.LoadLevel(Application.loadedLevel);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            hpLoss();
            hpBar.fillAmount = hp / (float)maxHp;
        }
    }

    private void hpLoss()
    {
        hp--;
        Debug.Log(hp);
        //yield return new WaitForSeconds(2);
    }
}
