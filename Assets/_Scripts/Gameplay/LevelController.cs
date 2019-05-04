using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public BoolVariableSO building;
    public IntVariableSO score;
    public IntVariableSO lives;
    public MenuView menu;

    void Start()
    {
        building.Value = false;
        ResetGame();

        menu.Appear();
    }

    public void ShowMenu()
    {
        menu.Appear();
    }

    public void ResetGame()
    {
        lives.Value = 1;
        score.Value = 0;
    }
}
