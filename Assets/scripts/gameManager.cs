using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public Button cell;
    public Transform board;
    public TMP_Text info, info2;
    public List<Button> grid = new List<Button>();

    public Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        Vector3 boardPos = board.transform.position;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                float xOffset = j * 75;
                float yOffset = i * 75;

                Button node;
                node = Instantiate(cell, new Vector3(boardPos.x + xOffset, boardPos.y + yOffset, boardPos.z), Quaternion.identity, board);
                grid.Add(node);
            }
        }
    }


    public void AI()
    {
        int random = Random.Range(0, grid.Count);
       
        if (grid[random].GetComponent<Cell>().isEmpty() && checkWinner() == null)
        {
            grid[random].GetComponent<Cell>().data.text = "o";
            checkWinner();
        }
        else
        {
           if (!checkLastMove() && checkWinner() == null) AI();
        }
    }

     bool checkLastMove()
    {
        int emptySpots = 0;
        for (int i = 0; i < grid.Count; i++)
        {
            if (grid[i].GetComponent<Cell>().isEmpty()) emptySpots++;
        }
        if (emptySpots > 0) return false;
        else return true;
    }

    int index(int i, int j)
    {
        return j + i*3;
    }

    bool equals3(string a, string b, string c)
    {
        return a == b && b == c && !string.IsNullOrEmpty(a);
    }

    string getGrid(int i, int j)
    {
        return grid[index(i, j)].GetComponent<Cell>().data.text;
    }

   public string checkWinner()
    {
        string winner = null;

        // horizontal
        
        for (int i = 0; i < 3; i++)
        {
            if (equals3(getGrid(i, 0), getGrid(i, 1), getGrid(i, 2)))
            {
                winner = getGrid(i, 0);
            }
        }
        
        // vertical 

        for (int i = 0; i < 3; i++)
        {
            if (equals3(getGrid(0, i), getGrid(1, i), getGrid(2, i)))
            {
                winner = getGrid(0, i);
            }
        }

        // diagonal

        if (equals3(getGrid(0, 0), getGrid(1, 1), getGrid(2, 2)))
        {
            winner = getGrid(0, 0);
        }

        if (equals3(getGrid(2, 0), getGrid(1, 1), getGrid(0, 2)))
        {
            winner = getGrid(2, 0);
        }

       if (winner == "x") info.text = "You Win!";
       if (winner == "o") info.text = "You Lose!";

       if (winner == null && checkLastMove()) info.text = "Tie!";

        if (winner != null) info2.text = "Press 'R' to replay";

        return winner;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(scene.name);
    }
}
