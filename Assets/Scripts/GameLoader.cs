using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void NewGame()
    {
        Application.LoadLevel("Hub");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Ебать ты чо делаешт");
    }
}
