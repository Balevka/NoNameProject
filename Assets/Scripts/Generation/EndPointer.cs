﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointer : MonoBehaviour
{
    public Transform target;
    public Transform player;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        Vector3 lookDir = target.position - player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180;
        rb.rotation = angle;
    }
}
