using Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class UiMenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject background;


    private void Awake()
    {
        var storage = new DataStorageService(new PlayerPrefsStorage());
        var hiScore = storage.GetInt(ConstStrings.HiScore);

        hiScoreText.SetText(hiScore.ToString());
    }


    public void StartGame()
    {
        SceneManager.LoadSceneAsync(ConstStrings.GameScene, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
