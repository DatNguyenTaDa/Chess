using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoButton : MonoBehaviour
{
    public static int sound = 1;
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
    public void TurnOffSound()
    {
        sound= 0;
    }
    public void TurnOnSound()
    {
        sound= 1;
    }
}
