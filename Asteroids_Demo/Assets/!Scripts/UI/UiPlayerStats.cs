using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiPlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject playerLife;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI hiScore;
    [SerializeField] private Transform livesContainer;

    private List<GameObject> _lives = new List<GameObject>();

    public void SetLives(int lives)
    {
        ClearLives();

        for (int i = 0; i < lives; i++)
        {
            var instance =   Instantiate(playerLife, livesContainer);
            instance.SetActive(true);
            _lives.Add(instance);
        }
    }

    public void SetScore(int score)
    {
        currentScore.SetText(score.ToString());
    }

    public void SetHiScore(int score)
    {
        hiScore.SetText(score.ToString());
    }

    public void RemoveLife()
    {
        Destroy(_lives[_lives.Count - 1]);
        _lives.RemoveAt(_lives.Count - 1);
    }

    public void ClearLives()
    {
        foreach (var life in _lives)
            Destroy(life);

        _lives.Clear();
    }

}
