using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreModel : IInitializable
{
    private UIHandler _uiCanvas;
    private GameInstaller _gameInstaller;
    public ScoreModel(UIHandler uiCanvas, GameInstaller gameInstaller)
    {
        this._uiCanvas = uiCanvas;
        this._gameInstaller = gameInstaller;
    }

    private int score;
    public int Score
    {
        get { return score; }
        set {
            score = value;
            _uiCanvas.UpdateScore(score);
        }
    }

    private int failures;
    public int Failures
    {
        get { return failures; }
        set
        {
            failures = value;
            _uiCanvas.UpdateFailures();
            if(failures > 3)
            {
                _uiCanvas.GameOver(score);
                _gameInstaller.CloseGame();
            }
        }
    }

    public int SpeedModificator()
    {
        return this.score / 10;
    }

    public void Initialize()
    {
        this.score = 0;
    }
}
