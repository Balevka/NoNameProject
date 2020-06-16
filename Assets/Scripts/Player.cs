using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] internal Animator playerAnimator;
    private Rigidbody2D rb;
    private int hp;
    private int maxHP = 10;
    [SerializeField] private Text roflaniumCounter;
    private int roflanium = 0;
    public Canvas GUI;
    public Canvas Loss;
    public Canvas Pause;
    public Image hpBar;
   
    private Vector2 movement;
    private Vector2 mousePos;
    [SerializeField] private Camera cam;

    public int Hp { get => hp; set => hp = value; }
    public int Roflanium { get => roflanium; set => roflanium = value; }

    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("inGameHP"));
        hp = PlayerPrefs.GetInt("inGameHP");
        roflanium = PlayerPrefs.GetInt("inGameRofl");
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
        if(hp > maxHP)
        {
            hp = maxHP;
        }
        Updater();
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
        {
            PlayerPrefs.SetInt("inGameHP", hp);
            PlayerPrefs.SetInt("inGameRofl", roflanium);
            Application.LoadLevel(Application.loadedLevel);
        }

        if (collision.gameObject.layer == 17)
        {
            hpChanger(-1);
            Updater();
        }
    }
    public void Updater() {
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            roflaniumCounter.text = roflanium.ToString();
            hpBar.fillAmount = hp / 10f;
        }
    }
    public void roflChanger(int count) {
        roflanium = roflanium + count;
    }
    public void hpChanger(int count) {
        hp = hp + count;
    }
}
