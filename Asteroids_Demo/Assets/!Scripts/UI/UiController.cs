using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    private const string GameScene = "GameScene";

    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] UiPlayerStats playerStats;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject background;
    [SerializeField] Camera cam;


    private void Awake()
    {
        //todo
        //hiscoreText = load data

        Messenger<int>.AddListener(Messages.SetHiScore, SetHiScore);
    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(Messages.SetHiScore, SetHiScore);
    }

    private void SetHiScore(int score)
    {
        hiScoreText.SetText(score.ToString());
    }

    public void StartGame()
    {
        playerStats.gameObject.SetActive(true);
        mainPanel.SetActive(false);
        background.SetActive(false);

        SceneManager.LoadSceneAsync(GameScene, LoadSceneMode.Additive).completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation op)
    {
        // refresh camera so it gets updated... ??
        cam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
