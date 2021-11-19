using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    public TMP_Text data;
    public GameObject manager;

    private void Start()
    {
        manager = GameObject.Find("Canvas");
    }
    public bool isEmpty()
    {
        if (data.text == "")
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void playerMove()
    {
        if (isEmpty() && manager.GetComponent<gameManager>().checkWinner() == null)
        {
            data.text = "x";
            manager.GetComponent<gameManager>().checkWinner();
            manager.GetComponent<gameManager>().AI();
        }
    }
}
