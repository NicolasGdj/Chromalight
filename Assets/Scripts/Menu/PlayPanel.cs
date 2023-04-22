using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayPanel : SubPanel
{
    public Button[] levelsButton;

    void Start() {
        base.Start();
         for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            Debug.Log(scenePath);
        }
        foreach (Button button in levelsButton) {
            
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) {
                string levelName = "Level" + buttonText.text;
                int buildIndex = SceneUtility.GetBuildIndexByScenePath(levelName);
                if (buildIndex != -1)
                {
                    button.onClick.AddListener(() => Level(levelName));
                    continue;
                } 
            }
            button.interactable = false;

        }
    }

    public void Level(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}
