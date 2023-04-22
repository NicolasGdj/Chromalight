using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPanel : MonoBehaviour
{
    public Button backButton;

    public void Start() {
        if(backButton != null)
            backButton.onClick.AddListener(Back);
    }

    void Back() {
        gameObject.SetActive(false);
    }
}
