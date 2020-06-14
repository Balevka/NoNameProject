using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputcheck : MonoBehaviour
{
    [SerializeField]
    private Player player;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (SceneManager.GetActiveScene().name == "Hub")
                Application.LoadLevel("MainMenu");
            if (SceneManager.GetActiveScene().name == "Demonstration")
                Application.LoadLevel("Hub");
            if (SceneManager.GetActiveScene().name == "TestGrid")
                Application.LoadLevel("Hub");
            if (SceneManager.GetActiveScene().name == "TestGrid")
                Application.LoadLevel("Hub");
            if (SceneManager.GetActiveScene().name == "Dungeon")
                Application.LoadLevel("Hub");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Application.LoadLevel("TestGrid");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Application.LoadLevel("TestScene");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            player.Hp = player.Hp + 10;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Application.LoadLevel("Dungeon");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Application.LoadLevel("Dungeon");
        }
    }
}
