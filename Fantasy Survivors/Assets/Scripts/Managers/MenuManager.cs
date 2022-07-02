using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject startingPanel;
    public GameObject settingsPanel;
    public GameObject mapSelectionPanel;

    public GameObject mapSelected;

    public void SetActivePanel(GameObject panel)
    {
        if(startingPanel != null)
        {
            startingPanel.SetActive(false);
        }

        if(settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if(mapSelectionPanel != null){
            mapSelectionPanel.SetActive(false);
        }

        panel.SetActive(true);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnMapSelected(GameObject map)
    {
        mapSelected = map;
    }

    public void OnConfirmMapSelection(GameObject panel)
    {
        if(mapSelected != null)
        {
            SetActivePanel(panel);
        }
    }

    public void OnConfirmCharacterSelection()
    {
        if(CharacterInfoManager.instance.characterSprite != null)
        {
            SceneManager.LoadScene(mapSelected.name);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Resume(GameObject pausePanel)
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
