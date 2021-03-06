using Audio;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
public class UiMenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] AudioClip selectSound;


    private void Awake()
    {
        AudioManager.Instance.SetMenuBackground();

        var storage = new DataStorageService(new PlayerPrefsStorage());
        var hiScore = storage.GetInt(ConstStrings.HiScore);

        hiScoreText.SetText(hiScore.ToString());
    }


    public void StartGame()
    {
        AudioManager.Instance.PlayClip(selectSound);
        SceneManager.LoadSceneAsync(ConstStrings.GameScene, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
