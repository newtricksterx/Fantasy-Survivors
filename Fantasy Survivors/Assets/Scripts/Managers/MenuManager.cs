using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> panels;

    public GameObject mapSelected;

    public AudioClip buttonSoundEffect;

    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Slider musicSlider;
    public Slider soundSlider;

    public GameObject acceptSettings;

    private void Awake()
    {
        Time.timeScale = 1f;

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }

    private void Update()
    {
        //Debug.Log("yo");
        //Debug.Log(Screen.currentResolution.ToString());
    }

    public void SetActivePanel(GameObject panel)
    {
        SoundManager.instance.PlaySoundClip(buttonSoundEffect);

        foreach(GameObject p in panels)
        {
            p.SetActive(false);
        }

        panel.SetActive(true);
    }

    public void Exit()
    {
        PlayerPrefs.SetFloat("MusicVolume", SoundManager.instance.GetMusicSource().volume);
        PlayerPrefs.SetFloat("SoundVolume", SoundManager.instance.GetSoundSource().volume);


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
        SoundManager.instance.PlaySoundClip(buttonSoundEffect);

        if (mapSelected != null)
        {
            SetActivePanel(panel);
        }
    }

    public void OnConfirmCharacterSelection()
    {
        PlayerPrefs.SetFloat("MusicVolume", SoundManager.instance.GetMusicSource().volume);
        PlayerPrefs.SetFloat("SoundVolume", SoundManager.instance.GetSoundSource().volume);

        SoundManager.instance.PlaySoundClip(buttonSoundEffect);

        if (CharacterInfoManager.instance.characterSprite != null)
        {
            SceneManager.LoadScene(mapSelected.name);
        }
    }

    public void MainMenu(AudioClip music)
    {
        PlayerPrefs.SetFloat("MusicVolume", SoundManager.instance.GetMusicSource().volume);
        PlayerPrefs.SetFloat("SoundVolume", SoundManager.instance.GetSoundSource().volume);

        SoundManager.instance.StopMusic();
        SoundManager.instance.SetMusic(music);
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

    public void SetSprite(Sprite sprite)
    {
        CharacterInfoManager.instance.SetSprite(sprite);
    }

    public void SetAnimController(RuntimeAnimatorController anim)
    {
        CharacterInfoManager.instance.SetAnimController(anim);
    }

    public void SetStartingAbility(GameObject go)
    {
        CharacterInfoManager.instance.SetStartingAbility(go);
    }

    public void SetFlipPlayer(bool flip)
    {
        CharacterInfoManager.instance.SetFlipPlayer(flip);
    }

    public void OnSettingsChanged()
    {
        acceptSettings.SetActive(true);
    }

    public void Accept()
    {
        string[] splitStrings = resolutionDropdown.options[resolutionDropdown.value].text.Split('x');
        int width =  int.Parse(splitStrings[0]);
        int height = int.Parse(splitStrings[1]);

        Debug.Log(width);

        Screen.SetResolution(width, height, fullScreenToggle.isOn, 60);

        SoundManager.instance.GetMusicSource().volume = musicSlider.value;
        SoundManager.instance.GetSoundSource().volume = soundSlider.value;

        acceptSettings.SetActive(false);
    }
}
