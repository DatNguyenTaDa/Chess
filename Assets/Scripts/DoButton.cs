using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Surrender()
    {
        if(Chess.instance.GetCurrentPlayer() == "white")
            Chess.instance.Winner("Black");
        if(Chess.instance.GetCurrentPlayer() == "black")
            Chess.instance.Winner("White");
    }
}
