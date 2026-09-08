using System;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    private void Start()
    {
        HPManager.Instance.OnDeath += GameOver;
        _obj.SetActive(false);
    }

    private void GameOver()
    {
        _obj.SetActive(true);
        Time.timeScale = 0f;
    }
}
