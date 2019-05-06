using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public IntVariableSO highScore;
    public BoolVariableSO seenTutorial;

    public void Save()
    {
        PlayerPrefs.SetInt("High Score", highScore.Value);
        PlayerPrefs.SetInt("Seen Tutorial", seenTutorial.Value ? 1 : 0);
    }

    public void Load()
    {
        highScore.Value = PlayerPrefs.GetInt("High Score");
        seenTutorial.Value = PlayerPrefs.GetInt("Seen Tutorial") == 1;
    }
}
