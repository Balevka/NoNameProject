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
    public Player player;
    public Canvas Paused;
    public Canvas Menu;
    public InputField input;
    public Text warningSave;
    public Text warningDelete;
    private bool paused = false;
    private bool seedAcceptable = false;
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
        PlayerPrefs.SetInt("inGameHP", 10);
        PlayerPrefs.SetInt("inGameRofl", 0);
        SceneManager.LoadScene("Hub");
    }
    public void SaveGame()
    {
        PlayerPrefs.SetInt("savedSeed", grid.GetComponent<Generation>().Seed);
    }
    public void LoadGame()
    {
        int seed;
        int hp = 10;
        int rofl = 0;
        if (PlayerPrefs.GetInt("loadedSeed") > 0)
        {
            seed = PlayerPrefs.GetInt("loadedSeed");
            PlayerPrefs.SetInt("inGameHP", hp);
            PlayerPrefs.SetInt("inGameRofl", rofl);
            PlayerPrefs.SetInt("seed", seed);
            SceneManager.LoadScene("Dungeon");
        }
        else
        {
            if (PlayerPrefs.GetInt("savedSeed") > 0)
            {
                seed = PlayerPrefs.GetInt("savedSeed");
                hp = PlayerPrefs.GetInt("savedHP");
                rofl = PlayerPrefs.GetInt("savedRofl");
                PlayerPrefs.SetInt("inGameRofl", rofl);
                PlayerPrefs.SetInt("inGameHP", hp);
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
    }
    public void LoadBySeed()
    {

        if (seedAcceptable)
        {
            PlayerPrefs.SetInt("loadedSeed", Int32.Parse(input.text));
            LoadGame();
        }
    }
    public void InputChecker()
    {
        if (Int32.Parse(input.text) > 999999999 || Int32.Parse(input.text) < 10000)
        {
            warningSave.enabled = true;
            seedAcceptable = false;
        }
        else
        {
            warningSave.enabled = false;
            seedAcceptable = true;
        }
    }
    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        Paused.enabled = true;
        GUI.enabled = false;
    }
    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        GUI.enabled = true;
        Loss.enabled = false;
        Paused.enabled = false;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GUI.enabled = true;
        Loss.enabled = false;
        Paused.enabled = false;
        SceneManager.LoadScene(Application.loadedLevel);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
