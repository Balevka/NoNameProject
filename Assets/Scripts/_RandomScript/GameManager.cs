using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private int level = 1;
    private float levelStartDelay = 2f;
    private bool doingSetup;
    private BoardManager boardScript;
    private Text levelText;
    private GameObject levelImage;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    private void OnLevelWasLoaded(int level)
    {
        this.level++;

        InitGame();
    }

    private void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        levelText.text = "Уровень " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        boardScript.SetupScene(level);
    }
    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }
    
}
