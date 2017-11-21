using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Button startButton;
    public Button quitButton;

    // Use this for initialization
    void Start () {
        Button btn_start = startButton.GetComponent<Button>();
        Button btn_quit = quitButton.GetComponent<Button>();

        btn_start.onClick.AddListener(StartGame);
        btn_quit.onClick.AddListener(QuitGame);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            StartGame();
        }
        else if (Input.GetButton("Jump"))
        {
            QuitGame();
        }
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Game 3", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
