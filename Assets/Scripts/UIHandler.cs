using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject failuresPanel;
    [SerializeField]
    private GameObject damagedPrefab;

    public Button playButton;
    public Button restartButton;

    internal void UpdateScore(int score)
    {
        scoreText.text = string.Format("Score: {0}", score);
    }

    internal void PlayGame()
    {
        mainPanel.SetActive(false);
        playButton.gameObject.SetActive(false);
        failuresPanel.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }

    internal void GameOver(int score)
    {
        scoreText.gameObject.SetActive(false);
        foreach (Transform icon in failuresPanel.transform)
        {
            Destroy(icon.gameObject);
        }
        failuresPanel.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScreen.transform.GetChild(0).GetComponent<Text>().text = string.Format("Scored: {0}", score);
    }

    internal void RestartGame()
    {
        failuresPanel.SetActive(true);
        scoreText.gameObject.SetActive(true);
        UpdateScore(0);
        gameOverScreen.SetActive(false);
    }

    internal void UpdateFailures()
    {
        var damagedIcon = Instantiate(damagedPrefab, failuresPanel.transform);
    }
}
