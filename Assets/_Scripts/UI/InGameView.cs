using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    public IntVariableSO score;
    [Space]
    public FadingDialog scoreLabel;
    public FadingDialog scoreText;
    public Runner runner;
    public BufferOrbManager orbManager;

    Text _scoreTextCache;
    
    void Start()
    {
        _scoreTextCache = scoreText.GetComponent<Text>();
        
        scoreLabel.Disappear();
        scoreText.Disappear();

        score.RegisterPostchangeEvent(() => _scoreTextCache.text = score.Value.ToString() );
    }

    public void Appear()
    {
        scoreLabel.Appear();
        scoreText.Appear();
        orbManager.Appear();
        runner.Appear();
    }

    public void Disappear()
    {
        scoreLabel.Disappear();
        scoreText.Disappear();
        runner.Disappear();
    }
}
