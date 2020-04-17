using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{

    private float levelStartDelay = 2f;
    private Text levelText;
    private GameObject levelImage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Test"))
        {
            Application.LoadLevel("TestScene");
        }
        if (collision.CompareTag("kurapapuru"))
        {
            Application.LoadLevel("KurupapuruRain");
        }
        if (collision.CompareTag("back"))
        {
            Application.LoadLevel("MainMenu");
        }
        if (collision.CompareTag("Random"))
        {
            Application.LoadLevel("Random");
        }
    }
    private void LoadImage(string level)
    {
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        levelText.text = level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
    }
}

