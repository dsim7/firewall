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
    public FadingDialog optionsButton;
    public BufferOrbManager orbManager;
    public BlackMask blackMask;
    public BoolVariableSO tunnelCleared;

    void Start()
    {
        blackMask.InstantFadeTo(1);
        highScoreLabel.Disappear();
        highScoreText.Disappear();
        playButton.Disappear();
        optionsButton.Disappear();
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
        optionsButton.Appear();
    }

    IEnumerator AppearCoroutine()
    {
        blackMask.FadeIn();
        title.Appear();
        highScoreLabel.Appear();
        highScoreText.Appear();
        optionsButton.Appear();
        orbManager.Disappear();

        // wait for tunnel to be clear before allowing a new game to start
        yield return new WaitUntil(() => tunnelCleared.Value);
        playButton.Appear();
    }

    public void Disappear()
    {
        title.Disappear();
        playButton.Disappear();
        highScoreLabel.Disappear();
        highScoreText.Disappear();
        optionsButton.Disappear();
    }
}
