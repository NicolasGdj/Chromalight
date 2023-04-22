using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject playPanel;
    public GameObject rulesPanel;
    public Button playButton;
    public Button rulesButton;
    public Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(ShowPlayPanel);
        rulesButton.onClick.AddListener(ShowRulesPanel);
        quitButton.onClick.AddListener(Quit);
    }
    
    void ShowPlayPanel() {
        playPanel.SetActive(true);
    }
    
    void ShowRulesPanel() {
        rulesPanel.SetActive(true);
    }

    void Quit() {
        Application.Quit();
    }
}
