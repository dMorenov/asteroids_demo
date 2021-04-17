using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiPlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject playerLife;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private Transform livesContainer;

    private List<GameObject> _lives = new List<GameObject>();

    private void OnEnable()
    {
        Messenger.AddListener(Messages.RemoveLife, RemoveLife);
        Messenger<int>.AddListener(Messages.SetLives, SetLives);
        Messenger<int>.AddListener(Messages.SetScore, SetScore);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.RemoveLife, RemoveLife);
        Messenger<int>.RemoveListener(Messages.SetLives, SetLives);
        Messenger<int>.RemoveListener(Messages.SetScore, SetScore);
    }

    private void SetLives(int lives)
    {
        ClearLives();

        for (int i = 0; i < lives; i++)
        {
            var instance =   Instantiate(playerLife, livesContainer);
            instance.SetActive(true);
            _lives.Add(instance);
        }
    }

    private void SetScore(int score)
    {
        currentScore.SetText(score.ToString());
    }

    private void RemoveLife()
    {
        Destroy(_lives[_lives.Count - 1]);
        _lives.RemoveAt(_lives.Count - 1);
    }

    private void ClearLives()
    {
        foreach (var life in _lives)
            Destroy(life);

        _lives.Clear();
    }

}
