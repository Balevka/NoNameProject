using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Grid grid;
    public Canvas GUI;
    public Canvas Loss;
    public Canvas Paused;
    public Canvas Menu;
    public InputField input;
    public Text warningSave;
    public Text warningDelete;
    private bool paused = false;
    private bool seedAcceptable = false;
    // Start is called before the first frame update
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            GUI.enabled = true;
            Loss.enabled = false;
            Paused.enabled = false;
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("seed", 0);
        PlayerPrefs.SetInt("savedSeed", 0);
        SceneManager.LoadScene("Hub");
    }
    public void SaveGame()
    {
        PlayerPrefs.SetInt("savedSeed", grid.GetComponent<Generation>().Seed);
        Debug.Log(PlayerPrefs.GetInt("savedSeed"));
    }
    public void LoadGame()
    {
        int seed;
        if (PlayerPrefs.GetInt("loadedSeed") > 0)
        {
            seed = PlayerPrefs.GetInt("loadedSeed");
            PlayerPrefs.SetInt("seed", seed);
        }
        else
        {
            if (PlayerPrefs.GetInt("savedSeed") > 0)
            {
                Debug.Log(PlayerPrefs.GetInt("savedSeed"));
                seed = PlayerPrefs.GetInt("savedSeed");
                PlayerPrefs.SetInt("seed", seed);
                SceneManager.LoadScene("Dungeon");
            }
            if (PlayerPrefs.GetInt("savedSeed") == 0)
            {
                warningDelete.enabled = true;
            }
        }
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void DeleteSave()
    {
        PlayerPrefs.SetInt("savedSeed", 0);
        Debug.Log(PlayerPrefs.GetInt("savedSeed"));
    }
    public void LoadBySeed()
    {

        if (seedAcceptable)
        {
            PlayerPrefs.SetInt("loadedSeed", Int32.Parse(input.text));
            Debug.Log(PlayerPrefs.GetInt("loadedSeed"));
            LoadGame();
        }
    }
    public void InputChecker()
    {
        if (Int32.Parse(input.text) > 999999999 || Int32.Parse(input.text) < 10000)
        {
            warningSave.enabled = true;
            seedAcceptable = false;
            Debug.Log("wrong seed");
        }
        else
        {
            warningSave.enabled = false;
            seedAcceptable = true;
            Debug.Log("nice seed");
        }
    }
    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        Paused.enabled = true;
        GUI.enabled = false;
        Debug.Log("Paused");
    }
    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        GUI.enabled = true;
        Loss.enabled = false;
        Paused.enabled = false;
        Debug.Log("Resumed");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GUI.enabled = true;
        Loss.enabled = false;
        Paused.enabled = false;
        SceneManager.LoadScene(Application.loadedLevel);
        Debug.Log("Restarted    ");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Ебать ты чо делаешт");
    }
}
