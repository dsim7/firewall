using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public BlackMask redMask;

    public UnityEvent OnGameOver;

    void Start()
    {
        gameOver.Disappear();
        scoreLabel.Disappear();
        scoreText.Disappear();
        continueButton.Disappear();
        redMask.FadeIn();

        lives.RegisterPostchangeEvent(() => { if (lives.Value == 0) OnGameOver.Invoke(); });
    }

    public void Appear()
    {
        Time.timeScale = 0.5f;
        
        gameOver.Appear();
        scoreLabel.Appear();
        scoreText.Appear();
        continueButton.Appear();
        redMask.InstantFadeTo(1);
        redMask.FadeIn();
        scoreText.GetComponent<Text>().text = score.Value.ToString();
    }

    public void Disappear()
    {
        Time.timeScale = 1f;
        
        gameOver.Disappear();
        scoreLabel.Disappear();
        scoreText.Disappear();
        continueButton.Disappear();
    }
}
