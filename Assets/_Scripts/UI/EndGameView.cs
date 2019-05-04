using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{
    public IntVariableSO lives;
    public IntVariableSO score;
    public IntVariableSO highScore;

    public FadingDialog gameOver;
    public FadingDialog scoreLabel;
    public FadingDialog scoreText;
    public FadingDialog continueButton;
    public FirewallBufferManager bufferManager;
    public BufferOrbManager orbManager;
    public BlackMask redMask;

    void Start()
    {
        lives.RegisterPostchangeEvent(() => { if (lives.Value == 0) Appear(); });

        gameOver.Disappear();
        scoreLabel.Disappear();
        scoreText.Disappear();
        continueButton.Disappear();
        redMask.FadeIn();
    }

    public void Appear()
    {
        Time.timeScale = 0.5f;

        bufferManager.FreezeAll(true);

        scoreText.GetComponent<Text>().text = score.Value.ToString();
        if (score.Value > highScore.Value)
        {
            highScore.Value = score.Value;
        }

        gameOver.Appear();
        scoreLabel.Appear();
        scoreText.Appear();
        continueButton.Appear();

        redMask.InstantFadeTo(1);
        redMask.FadeIn();
    }

    public void Disappear()
    {
        Time.timeScale = 1f;

        orbManager.Disappear();

        gameOver.Disappear();
        scoreLabel.Disappear();
        scoreText.Disappear();
        continueButton.Disappear();

        lives.Value = 1;
        score.Value = 0;
    }
}
