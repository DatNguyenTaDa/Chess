using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
    [SerializeField] private GameObject chessPiece;


    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] playerBlack = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
    }

    // Update is called once per frame
    
}
