using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    public IntVariableSO lives;
    public IntVariableSO score;

    public FadingDialog backButton;
    public FadingDialog scoreLabel;
    public FadingDialog scoreText;
    public BufferOrbManager orbManager;
    public Runner runner;
    public BoolVariableSO building;
    public FirewallBufferManager bufferManager;

    Text _scoreTextCache;

    void Start()
    {
        _scoreTextCache = scoreText.GetComponent<Text>();

        backButton.Disappear();
        scoreLabel.Disappear();
        scoreText.Disappear();
        building.Value = false;

        score.RegisterPostchangeEvent(() => _scoreTextCache.text = score.Value.ToString() );
        lives.RegisterPostchangeEvent(() => { if (lives.Value == 0) { Disappear(); } });
    }

    public void Appear()
    {
        backButton.Appear();
        scoreLabel.Appear();
        scoreText.Appear();
        orbManager.Appear();
        runner.Appear();
        building.Value = true;

        bufferManager.ResetAll();
        bufferManager.FreezeAll(false);
        orbManager.UpdateInstant();
    }

    public void Disappear()
    {
        backButton.Disappear();
        scoreLabel.Disappear();
        scoreText.Disappear();

        runner.Disappear();
        building.Value = false;
    }
}
