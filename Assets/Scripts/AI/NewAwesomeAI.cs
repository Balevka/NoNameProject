using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewAwesomeAI : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 5f;

    [SerializeField]
    private float lookingAngle = 45f;


    public Transform target;

    private float angle;
    private Vector2 distance;
    private Rigidbody2D enemyRb;


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        distance = target.position - transform.position;


        Debug.DrawRay(transform.position, transform.right, Color.green);



        if (lookingAngle> Vector2.Angle(transform.right, distance) && distance.magnitude <= maxDistance)
        {
            Debug.Log("Follow");
            Debug.Log(Vector2.Angle(transform.right, distance));
            Debug.DrawRay(transform.position, transform.right, Color.red);

        }


        

    }


    /*private float Module(Vector3 vector)
    {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }


    private float VectorAngle(Vector3 vector1, Vector3 vector2)
    {
        float scalarPow = vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;

        return scalarPow / (Module(vector1) * Module(vector2)) * 100;
    }*/
}
