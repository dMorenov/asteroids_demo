using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMenuController : MonoBehaviour
{
    private const string GameScene = "GameScene";

    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject background;


    private void Awake()
    {
        //todo
        //hiscoreText = load data

    }


    public void StartGame()
    {
        SceneManager.LoadSceneAsync(GameScene, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
