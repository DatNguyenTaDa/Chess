using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;

    [SerializeField] private Sprite white_king, white_queen, white_bishop, white_rook, white_pawn;
    [SerializeField] private Sprite black_king, black_queen, black_bishop, black_rook, black_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        SetCoords();

        switch (this.name)
        {
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; break;

            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; break;
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }
}
