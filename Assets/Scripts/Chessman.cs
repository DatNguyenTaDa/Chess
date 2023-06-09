﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;

    //private bool isPromotion = false;

    [SerializeField] private Sprite white_king, white_queen, white_bishop, white_rook, white_knight, white_pawn;
    [SerializeField] private Sprite black_king, black_queen, black_bishop, black_rook, black_knight, black_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        SetCoords();

        switch (this.name)
        {
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;

            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
        }
        
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 1.1f;
        y *= 1.1f;

        x += -3.85f;
        y += -3.85f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }
    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }
    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }
    private void OnMouseUp()
    {
        if (Chess.isMove)
        {
            if (!controller.GetComponent<Chess>().IsGameOver() && controller.GetComponent<Chess>().GetCurrentPlayer() == player)
            {
                DestroyMovePlate();

                InitiateMovePlate();
            }
        }
    }

    private void Update()
    {
        switch (this.name)
        {
            case "black_pawn":
                if (yBoard == 0)
                {

                    GameObject cp = controller.GetComponent<Chess>().GetPosition(xBoard, yBoard);
                    cp.name = "black_queen";
                    this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black";

                }
                break;

            case "white_pawn":
                if (yBoard == 7)
                {
                    GameObject cp = controller.GetComponent<Chess>().GetPosition(xBoard, yBoard);
                    cp.name = "white_queen";
                    this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white";

                }
                break;
        }
    }

    public void DestroyMovePlate()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlate()
    {
        switch(this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "white_knight":
            case "black_knight":
                LMovePlate();
                break;
            case "white_bishop":
            case "black_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "white_king":
                SurroundMovePlate();
                if(CheckWhiteCastling() == 1 && Chess.whiteCastlingTimes == 0)
                {
                    MovePlateSpawn(7,0);
                }
                if (CheckWhiteCastling() == 2 && Chess.whiteCastlingTimes == 0)
                {
                    MovePlateSpawn(0, 0);
                }
                break;
            case "black_king":
                SurroundMovePlate();
                //if(CheckBlackCatsling() && GetComponent<Chess>().GetBlackCastlingTimes() == 0)
                //{
                //    MovePlateSpawn(0, 7);
                //}
                break;
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);

                if (CheckWhiteCastling() == 1 && Chess.whiteCastlingTimes == 0)
                {
                    MovePlateSpawn(4, 0);
                }
                if (CheckWhiteCastling() == 2 && Chess.whiteCastlingTimes == 0)
                {
                    MovePlateSpawn(4, 0);
                }
                break;
            case "black_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);

                if (CheckBlackCatsling() == 1 && Chess.blackCastlingTimes == 0)
                {
                    MovePlateSpawn(4, 7);
                }
                if (CheckBlackCatsling() == 2 && Chess.blackCastlingTimes == 0)
                {
                    MovePlateSpawn(4, 7);
                }

                break;
            case "black_pawn":
                
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }


    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Chess sc = controller.GetComponent<Chess>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }
    public void PointMovePlate(int x, int y)
    {
        Chess sc = controller.GetComponent<Chess>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if(cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if(cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }
    public int CheckWhiteCastling()
    {
        Chess sc = controller.GetComponent<Chess>();
        if (sc.GetPosition(5, 0) == null && sc.GetPosition(6, 0) == null &&
            sc.GetPosition(7, 0).name == "white_rook" && sc.GetPosition(4, 0).name == "white_king")
        {
            return 1;
        }
        if (sc.GetPosition(1, 0) == null && sc.GetPosition(2, 0) == null && sc.GetPosition(3, 0) == null &&
            sc.GetPosition(0, 0).name == "white_rook" && sc.GetPosition(4, 0).name == "white_king")
        {
            return 2;
        }
        return 0;
    }
    public int CheckBlackCatsling()
    {
        Chess sc = controller.GetComponent<Chess>();
        if (sc.GetPosition(5, 7) == null && sc.GetPosition(6, 7) == null &&
            sc.GetPosition(7, 7).name == "black_rook" && sc.GetPosition(4, 7).name == "black_king")
        {
            return 1;
        }
        if (sc.GetPosition(1, 7) == null && sc.GetPosition(2, 7) == null && sc.GetPosition(3, 7) == null &&
            sc.GetPosition(0, 7).name == "black_rook" && sc.GetPosition(4, 7).name == "black_king")
        {
            return 2;
        }
        return 0;
    }
    public void PawnMovePlate(int x, int y)
    {
        Chess sc = controller.GetComponent<Chess>();
        if(sc.PositionOnBoard(x,y))
        {


            if (sc.GetPosition(x, y) == null)
            {
                if (yBoard == 1 && sc.GetPosition(x, y+1) == null)
                {
                    MovePlateSpawn(x, y);
                    MovePlateSpawn(x, y+1);
                }
                else if(yBoard == 6 && sc.GetPosition(x, y-1) == null)
                {
                    MovePlateSpawn(x, y);
                    MovePlateSpawn(x, y-1);
                }else
                    MovePlateSpawn(x, y);
            }
            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                sc.GetPosition(x+1,y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }

        }
    }
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.1f;
        y *= 1.1f;

        x += -3.85f;
        y += -3.85f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.1f;
        y *= 1.1f;

        x += -3.85f;
        y += -3.85f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    //public bool IsKingInDanger()
    //{
    //    int x = GetXBoard();
    //    int y = GetYBoard();
    //    GameObject[] chessmen = GameObject.FindGameObjectsWithTag("Chessman");
    //    foreach (GameObject chessman in chessmen)
    //    {
    //        Chessman chessmanScript = chessman.GetComponent<Chessman>();
    //        if (chessmanScript != null && chessmanScript.player != player)
    //        {
    //            int x1 = chessmanScript.GetXBoard();
    //            int y1 = chessmanScript.GetYBoard();
    //            if (chessmanScript.ValidMove(x, y) && chessmanScript.name != "white_king" && chessmanScript.name != "black_king")
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    //public bool IsChecked(string color)
    //{
    //    GameObject king = GameObject.Find(color + "_king");
    //    foreach (GameObject chessman in GameObject.FindGameObjectsWithTag("Chessman"))
    //    {
    //        Chessman chessmanScript = chessman.GetComponent<Chessman>();
    //        if (chessmanScript.GetPlayer() != color)
    //        {
    //            if (chessmanScript.ValidMove(king.GetComponent<Chessman>().GetXBoard(), king.GetComponent<Chessman>().GetYBoard()))
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    //public void CheckKingInDanger()
    //{
    //    string opponent = (player == "white") ? "black" : "white"; // tìm người chơi đối thủ

    //    // tìm vị trí của vua của người chơi hiện tại
    //    int kingX = -1, kingY = -1;
    //    GameObject[,] positions = controller.GetComponent<Chess>().GetPosition();
    //    for (int x = 0; x < 8; x++)
    //    {
    //        for (int y = 0; y < 8; y++)
    //        {
    //            if (positions[x, y] != null && positions[x, y].name == player + "_king")
    //            {
    //                kingX = x;
    //                kingY = y;
    //                break;
    //            }
    //        }
    //        if (kingX != -1 && kingY != -1) break;
    //    }

    //    // kiểm tra xem vua có bị tấn công không
    //    for (int x = 0; x < 8; x++)
    //    {
    //        for (int y = 0; y < 8; y++)
    //        {
    //            if (positions[x, y] != null && positions[x, y].name.StartsWith(opponent))
    //            {
    //                Chessman chessman = positions[x, y].GetComponent<Chessman>();
    //                if (chessman != null && chessman.IsMovePossible(kingX, kingY))
    //                {
    //                    Debug.Log(player + "'s king is in danger!");
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}

}
