using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float levelStartDelay = 2f;
    private bool doingSetup;
    public static GameManager instance = null;
    private BoardManager boardScript;
    [SerializeField] private int level = 1;
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
        boardScript.SetupScene(level);
    }
}
