using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Chess : MonoBehaviour
{
    [SerializeField] private GameObject chessPiece;
    [SerializeField] private RectTransform panel;
    [SerializeField] private Image whiteKing;
    [SerializeField] private Image blackKing;
    public static bool isMove;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] playerBlack = new GameObject[16];

    private string currentPlayer = "white";
    private string winnerPlayer = "White";

    private bool gameOver;

    private float blackTime;
    private float whiteTime;
    private bool isBlackCD;
    private bool isWhiteCD;

    [SerializeField] private TMP_Text blackTurn;
    [SerializeField] private TMP_Text whiteTurn;

    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(false);

        gameOver = false;
        isMove= true;

        whiteTime = 300;
        blackTime = 300;
        isWhiteCD = true; isBlackCD = false;

        whiteKing.gameObject.SetActive(false);
        blackKing.gameObject.SetActive(false);

        blackTurn.gameObject.SetActive(false);
        whiteTurn.gameObject.SetActive(true);

        playerWhite = new GameObject[]
        {
            Create("white_rook",0,0),
            Create("white_knight",1,0),
            Create("white_bishop",2,0),
            Create("white_queen",3,0),
            Create("white_king",4,0),
            Create("white_bishop",5,0),
            Create("white_knight",6,0),
            Create("white_rook",7,0),

            Create("white_pawn",0,1),
            Create("white_pawn",1,1),
            Create("white_pawn",2,1),
            Create("white_pawn",3,1),
            Create("white_pawn",4,1),
            Create("white_pawn",5,1),
            Create("white_pawn",6,1),
            Create("white_pawn",7,1),
        };
        playerBlack = new GameObject[]
        {
            Create("black_rook",0,7),
            Create("black_knight",1,7),
            Create("black_bishop",2,7),
            Create("black_queen",3,7),
            Create("black_king",4,7),
            Create("black_bishop",5,7),
            Create("black_knight",6,7),
            Create("black_rook",7,7),

            Create("black_pawn",0,6),
            Create("black_pawn",1,6),
            Create("black_pawn",2,6),
            Create("black_pawn",3,6),
            Create("black_pawn",4,6),
            Create("black_pawn",5,6),
            Create("black_pawn",6,6),
            Create("black_pawn",7,6),
        };

        for(int i = 0; i<playerBlack.Length;i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    // Update is called once per frame
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0,0,-1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }
    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }
    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if(currentPlayer == "white")
        {
            isWhiteCD = false;
            isBlackCD = true;

            blackTurn.gameObject.SetActive(true);
            whiteTurn.gameObject.SetActive(false);

            currentPlayer = "black";
        }
        else
        {
            isWhiteCD = true;
            isBlackCD = false;

            blackTurn.gameObject.SetActive(false);
            whiteTurn.gameObject.SetActive(true);

            currentPlayer = "white";
        }
    }
    private void Update()
    { 
        if (Mathf.Round(whiteTime) > 0 && isWhiteCD)
        {
            whiteTime -= Time.deltaTime;
        }
        if (Mathf.Round(blackTime) > 0 && isBlackCD)
        {
            blackTime -= Time.deltaTime;
        }
        GameObject.FindGameObjectWithTag("WhiteTimer").GetComponent<TMP_Text>().text = "Time remaining: " + Mathf.Round(whiteTime).ToString();
        GameObject.FindGameObjectWithTag("BlackTimer").GetComponent<TMP_Text>().text = "Time remaining: " + Mathf.Round(blackTime).ToString();
        //for (int x = 0; x < 8; x++)
        //{
        //    if (GetPosition(x, 7) != null && (GetPosition(x, 7).GetComponent<Chessman>().name == "white_pawn"))
        //    {
        //        GameObject pawn = GetPosition(x, 7);
        //        SetPositionEmpty(x, 7);
        //        Destroy(pawn);
        //        GameObject queen = Create("white_queen", x, 7);
        //        SetPosition(queen);
        //    }
        //    if (GetPosition(x, 0) != null && (GetPosition(x, 0).GetComponent<Chessman>().name == "black_pawn"))
        //    {
        //        GameObject pawn = GetPosition(x, 0);
        //        SetPositionEmpty(x, 0);
        //        Destroy(pawn);
        //        GameObject queen = Create("black_queen", x, 0);
        //        SetPosition(queen);
        //    }
        //}
        if(Mathf.Round(whiteTime) == 0)
            Winner("Black");
        if(Mathf.Round(blackTime) == 0)
            Winner("White");
        if(gameOver == true)
        {
            panel.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TMP_Text>().text = winnerPlayer + " is Winner";
            if(winnerPlayer == "White")
            {
                whiteKing.gameObject.SetActive(true);
            }
            if(winnerPlayer == "Black")
            {
                blackKing.gameObject.SetActive(true);
            }
        }
    }
    public void Winner(string winner)
    {
        gameOver = true;

        winnerPlayer = winner;
    }
    
}
