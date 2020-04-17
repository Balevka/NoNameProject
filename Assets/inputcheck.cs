using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputcheck : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (SceneManager.GetActiveScene().name == "Hub")
                Application.LoadLevel("MainMenu");
            if (SceneManager.GetActiveScene().name == "Random")
                Application.LoadLevel("Hub");
            if (SceneManager.GetActiveScene().name == "TestScene")
                Application.LoadLevel("Hub");
        }
    }
}
