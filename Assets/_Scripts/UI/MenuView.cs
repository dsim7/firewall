using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public static bool seenIntro = false;

    public IntVariableSO highScore;
    public FadingDialog title;
    public FadingDialog highScoreLabel;
    public FadingDialog highScoreText;
    public FadingDialog playButton;
    public BlackMask blackMask;

    void Start()
    {
        blackMask.InstantFadeTo(1);
        highScoreLabel.Disappear();
        highScoreText.Disappear();
        playButton.Disappear();
    }

    public void Appear()
    {
        highScoreText.GetComponent<Text>().text = highScore.Value.ToString();

        if (!seenIntro)
        {
            seenIntro = true;
            StartCoroutine(IntroCoroutine());
        }
        else
        {
            StartCoroutine(AppearCoroutine());
        }
    }

    IEnumerator IntroCoroutine()
    {
        title.Appear();
        yield return new WaitForSeconds(2f);
        blackMask.FadeIn();
        yield return new WaitForSeconds(blackMask.fadeRate);
        playButton.Appear();
        highScoreLabel.Appear();
        highScoreText.Appear();
    }

    IEnumerator AppearCoroutine()
    {
        blackMask.FadeIn();
        title.Appear();
        highScoreLabel.Appear();
        highScoreText.Appear();

        yield return new WaitForSeconds(3f);
        playButton.Appear();
    }

    public void Disappear()
    {
        title.Disappear();
        playButton.Disappear();
        highScoreLabel.Disappear();
        highScoreText.Disappear();
    }
}
