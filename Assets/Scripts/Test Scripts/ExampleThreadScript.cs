using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ExampleThreadScript : MonoBehaviour
{
    bool isRunning = false;


    void Start()
    {
        Debug.Log("Start");

        Thread myThread = new Thread(SlowJob);
        myThread.Start();
        Debug.Log("End");
        
    }

    
    

    
    void Update()
    {
        
        if (isRunning)
            Debug.Log(("Slow Job is running"));


        transform.position += new Vector3(.1f, 0, 0);
    }


    private void FixedUpdate()
    {
        
    }


    private void SlowJob()
    {
        isRunning = true;

        Debug.Log("ExampleThreadScript::SlowJob() -- Doing 1000 things, each taking 2ms.");


        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();


        sw.Start();

        // Пока не пройдет 1000 итераций работа будет продолжаться
        for(int i = 0; i<1000; i++)
        {

            Thread.Sleep(1000);
            
        }

        sw.Stop();


        Debug.Log("Done");

        isRunning = false;

        
    }
}
