using System.Collections;
using System.Collections.Generic;
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
        controller = GameObject.FindGameObjectWithTag("GameController");
        if(attack)
        {
            GameObject cp = controller.GetComponent<Chess>().GetPosition(matrixX, matrixY);

            Destroy(cp);
        }
        controller.GetComponent<Chess>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Chess>().SetPosition(reference);

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
