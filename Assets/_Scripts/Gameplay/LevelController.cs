using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public BoolVariableSO playing;
    public IntVariableSO score;
    public IntVariableSO highScore;
    public IntVariableSO lives;
    public FirewallBufferManager bufferManager;
    public SaveLoad saveLoad;

    public UnityEvent OnStart;

    void Start()
    {
        playing.Value = false;
        score.Value = 0;
        highScore.Value = 0;

        OnStart.Invoke();
    }

    public void Play()
    {
        lives.Value = 1;
        score.Value = 0;
        bufferManager.ResetAll();
        bufferManager.FreezeAll(false);
        playing.Value = true;
    }

    public void StoppedPlaying()
    {
        playing.Value = false;
    }

    public void GameOver()
    {
        playing.Value = false;
        bufferManager.FreezeAll(true);
        if (score.Value > highScore.Value)
        {
            highScore.Value = score.Value;
        }
        saveLoad.Save();
    }
}
