using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // the player
    public GameObject player;

    public Text timeText;
    public FloatingTextManager floatingTextManager;
    
    // XP category
    public List<int> xpTable; 
    public int xpTableIndex;
    public ExperienceBar experienceBar;

    // Player attributes
    public int playerExperience;

    // Selectors
    public SelectorManager selector1;
    public SelectorManager selector2;
    public SelectorManager selector3;

    // AbilitySelectCanvas stuff
    public GameObject abilitySelectCanvas;

    // array/list of abilities as options already
    public List<GameObject> abilitiesPicked;

    // lootbox
    public GameObject lootboxPanel;

    // pause menu
    public GameObject pauseMenu;

    // game over panel
    public GameObject gameOverPanel;

    // set the music
    public AudioClip audioClip;

    // sound effect on level up
    public AudioClip levelUpSound;

    //game over bool
    private bool gameOverBool;

    // menumanager
    public MenuManager menuManager;

    // list of maps
    public List<GameObject> maps;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        Time.timeScale = 1f;

        if (CharacterInfoManager.instance != null)
        {
            Debug.Log("set character");
            SetCharacterInfo();
        }

        xpTableIndex = 0;
        
        experienceBar.ResetExpBar();
        experienceBar.SetLevelText(xpTableIndex);
        experienceBar.SetMaxExp(xpTable[xpTableIndex]);

        abilitySelectCanvas.SetActive(false);

        player = GameObject.Find("Player");

        abilitiesPicked = new List<GameObject>();

        if(SoundManager.instance != null)
        {
            if(audioClip != null)
            {
                SoundManager.instance.SetMusic(audioClip);
            }

            SoundManager.instance.GetSoundSource().Stop();
        }

        gameOverBool = false;

        SetMap();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

        IsGameOver();

        bool isPanelActive = false;

        foreach(GameObject panel in menuManager.panels)
        {
            if (panel.activeInHierarchy)
            {
                isPanelActive = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPanelActive)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPanelActive && pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(Time.timeSinceLevelLoad/60);
        int seconds = Mathf.FloorToInt(Time.timeSinceLevelLoad - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeText.text = niceTime;
        //timeText.text = (Mathf.Round(Time.timeSinceLevelLoad)).ToString();
    }

    public void ShowText(string msg, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontsize, color, position, motion, duration);
    }

    public void GrantXP(int experienceToAdd)
    {
        playerExperience += experienceToAdd;

        if(xpTableIndex < xpTable.Count - 1 && playerExperience >= xpTable[xpTableIndex])
        {
            xpTableIndex++;
            playerExperience = 0;
            OnLevelUp();
        }

        experienceBar.SetExp(playerExperience);
    }

    public void OnLevelUp()
    {
        //Debug.Log(xpTableIndex);
        SoundManager.instance.PlaySoundClip(levelUpSound);

        experienceBar.SetMaxExp(xpTable[xpTableIndex]);
        experienceBar.SetLevelText(xpTableIndex);

        selector1.gameObject.SetActive(true);
        selector2.gameObject.SetActive(true);
        selector3.gameObject.SetActive(true);

        selector1.GetAbility();
        selector2.GetAbility();
        selector3.GetAbility();
        abilitySelectCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnSelection()
    {
        abilitySelectCanvas.SetActive(false);
        abilitiesPicked.Clear();
        Time.timeScale = 1f;
    }

    public void OnConfirm()
    {
        lootboxPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IsGameOver()
    {
        if(GameObject.Find("Player") == null && !gameOverBool)
        {
            SoundManager.instance.StopMusic();
            SoundManager.instance.SetMusic(SoundManager.instance.gameOverSound);

            gameOverBool = true;

            StartCoroutine(GameOver());
        }
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetCharacterInfo()
    {
        player.GetComponent<SpriteRenderer>().sprite = CharacterInfoManager.instance.characterSprite;
        player.GetComponent<Animator>().runtimeAnimatorController = CharacterInfoManager.instance.animController;

        GameObject startingAbility = CharacterInfoManager.instance.startingAbility;
        foreach(SpawnAbilities s in FindObjectsOfType<SpawnAbilities>())
        {
            if(s.gameObject.name == startingAbility.name)
            {
                s.EnableAbility();
            }
            else
            {
                s.DisableAbility();
            }
        }

        player.GetComponent<SpriteRenderer>().flipX = CharacterInfoManager.instance.flipPlayer;
    }

    public void SetMap() 
    { 
        foreach(GameObject map in maps)
        {
            if(map.name == CharacterInfoManager.instance.mapSelected)
            {
                map.SetActive(true);
            }
            else
            {
                map.SetActive(false);
            }
        }
    }
}
