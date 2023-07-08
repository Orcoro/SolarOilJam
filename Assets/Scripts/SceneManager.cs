using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NaughtyAttributes;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Canvas _pauseMenu;
    [SerializeField] private Canvas _gameOverMenu;
    [SerializeField] private Canvas _winMenu;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _statsMenu;

    void Start()
    {
        _pauseMenu.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(false);
        _winMenu.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }  
        if (Input.GetKeyDown(KeyCode.I))
        {
            Stats();
        } 
    }

    [Button ("GameOver")]
    public void GameOver()
    {
        _camera.GetComponent<CameraManager>().enabled = false;
        _gameOverMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    [Button ("Win")]
    public void Win()
    {
        _camera.GetComponent<CameraManager>().enabled = false;
        _winMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    [Button ("Resume")]
    public void Pause()
    {
        _camera.GetComponent<CameraManager>().enabled = false;
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    [Button ("Restart")]
    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ChangeToScene(int sceneToChangeTo)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToChangeTo);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        _camera.GetComponent<CameraManager>().enabled = true;
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Stats()
    {
        _statsMenu.SetActive(!_statsMenu.activeSelf);
    }
}
