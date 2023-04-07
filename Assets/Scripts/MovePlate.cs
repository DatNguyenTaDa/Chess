using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    private GameObject controller;


    GameObject reference = null;

    //vi tri cua Board
    int matrixX;
    int matrixY;

    //trang thai quan co, true neu dang tan cong
    public bool attack =  false;

    private void Start()
    {
        if(attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    private void OnMouseUp()
    {
        Debug.Log("di chuyen");
        if(!attack && DoButton.sound == 1)
        {
            GameObject.FindGameObjectWithTag("MoveAudio").GetComponent<AudioSource>().Play();
        }
        controller = GameObject.FindGameObjectWithTag("GameController");
        if(attack)
        {
            GameObject cp = controller.GetComponent<Chess>().GetPosition(matrixX, matrixY);

            if(cp.name == "white_king")
            {
                controller.GetComponent<Chess>().Winner("Black");
                Chess.isMove = false;
            }
            if(cp.name == "black_king")
            {
                controller.GetComponent<Chess>().Winner("White");
                Chess.isMove = false;
            }

            Destroy(cp);
            if(DoButton.sound == 1)
                GameObject.FindGameObjectWithTag("AttackAudio").GetComponent<AudioSource>().Play();
        }
        controller.GetComponent<Chess>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Chess>().SetPosition(reference);

        controller.GetComponent<Chess>().NextTurn();

        reference.GetComponent<Chessman>().DestroyMovePlate();

    }

    public void SetCoords(int x, int y)
    {
        matrixX= x; matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference= obj;
    }
    public GameObject GetReference() { return reference; }
}
