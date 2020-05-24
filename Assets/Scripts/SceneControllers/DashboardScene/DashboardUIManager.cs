using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashboardUIManager : MonoBehaviour {
    public Button PlayButton;
    private void Start() {
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(delegate() {
            Loader.Load(SceneName.DemoScene);
        });
    }
}
