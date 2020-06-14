using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject projectTile;
    [SerializeField] private float projectTileForce = 20f;
    private SpriteRenderer weaponRenderer;
    private Vector2 mousePos;
    private Rigidbody2D rb;
    private Transform firePoint;
    
    

    void Start()
    {
        firePoint = GetComponentInChildren<Transform>();
        weaponRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        weaponRenderer.sprite = weaponSprite;
        gameObject.transform.localScale = new Vector2(0.3f, 0.3f);
    }

    
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        this.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }

    }


    private void Shooting()
    {
        GameObject pt =  Instantiate(projectTile, firePoint.position, firePoint.rotation);
        pt.GetComponent<Rigidbody2D>().AddForce(firePoint.right * projectTileForce, ForceMode2D.Impulse);
    }

    
}
