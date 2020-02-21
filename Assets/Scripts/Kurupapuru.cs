using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurupapuru : MonoBehaviour
{
    [SerializeField]
    private GameObject kurupapuru;
    private System.Random r = new System.Random();

    private int kurupapuruCount = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CreateKurupapuru(new Vector2(r.Next(-10, 10), 10f));
        
    }

    private void CreateKurupapuru(Vector2 vector)
    {
        Instantiate(kurupapuru, vector, transform.rotation);
        kurupapuruCount++;
    }

    

    private void OnMouseDown()
    {
        
    }

}

