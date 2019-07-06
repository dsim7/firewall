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

    int passivePointIncrement = 3;
    float passivePointsTimeInterval = 0.1f;
    float passivePointsTimer = 0;

    void Start()
    {
        playing.Value = false;
        score.Value = 0;
        highScore.Value = 0;

        OnStart.Invoke();
    }

    private void Update()
    {
        if (playing.Value)
        {
            passivePointsTimer += Time.deltaTime;
            while (passivePointsTimer > passivePointsTimeInterval)
            {
                score.Value += passivePointIncrement;
                passivePointsTimer -= passivePointsTimeInterval;
            }
        }
    }

    public void Play()
    {
        lives.Value = 1;
        score.Value = 0;
        bufferManager.ResetAll();
        bufferManager.FreezeAll(false);
        playing.Value = true;

        passivePointsTimer = 0;
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
