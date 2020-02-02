using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityStandardAssets.Characters.ThirdPerson;

public class HudController : MonoBehaviour
{
    public float TimeLeft = 30f;
    public bool TimeLeftStop = true;
    private float Minutes;
    private float Seconds;

    private int progressDown = 0;
    public GameObject[] ServersDownToggles;
    public GameObject ServerProgressBarPrefab;

    private GameObject player;
    private GameObject HudLayout;
    private GameObject TimeLeftText;
    private GameObject LevelClearedLayout;
    private GameObject GameOverLayout;
                
    public void ProgressServersDown()
    {
        if (progressDown < ServersDownToggles.Length)
        {
            ServersDownToggles[progressDown++].GetComponent<Toggle>().isOn = true;

            if (progressDown == ServersDownToggles.Length)
            {
                player.GetComponent<ThirdPersonUserControl>().change_status();
                ShowGameOver();
            }
        }
    }

    public void StartTimer(float from)
    {
        TimeLeftStop = false;
        TimeLeft = from;
        Update();
        StartCoroutine(UpdateCoroutine());
    }

    public void ResetProgress()
    {
        foreach (GameObject toggle in ServersDownToggles)
        {
            toggle.GetComponent<Toggle>().isOn = false;
        }
        progressDown = 0;
    }

    public void ResetHudForLevel()
    {
        ResetProgress();
        LevelClearedLayout.SetActive(false);
    }

    public void ShowServerProgressBar(GameObject server)
    {
        GameObject inst = Instantiate(
                ServerProgressBarPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity);

        inst.transform.parent = HudLayout.transform.parent;

        server.GetComponent<ServerModel>()
            .setProgressBar(inst);
    }

    private void ShowGameOver()
    {
        GameOverLayout.SetActive(true);
    }

    private void ShowLevelCleared()
    {
        LevelClearedLayout.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
     
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HudLayout = GameObject.Find("HUDLayout");
        TimeLeftText = GameObject.Find("TimeLeft");
        LevelClearedLayout = GameObject.Find("LevelClearedLayout");
        GameOverLayout = GameObject.Find("GameOverLayout");
        LevelClearedLayout.SetActive(false);
        GameOverLayout.SetActive(false);

        if (TimeLeftText == null)
        {
            Debug.LogError("TimeLeftText is not set");
        }

        if (ServersDownToggles.Length == 0)
        {
            Debug.LogError("ServersDownToggles is empty");
        }

        StartTimer(TimeLeft);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            ProgressServersDown();
        }

        if (Input.GetKeyDown("n"))
        {
            ResetHudForLevel();
        }

        if (Input.GetKeyDown("space"))
        {
            ShowGameOver();
        }

        if (!TimeLeftStop)
        {
            TimeLeft -= Time.deltaTime;

            Minutes = Mathf.Floor(TimeLeft / 60);
            Seconds = TimeLeft % 60;
            if (Seconds > 59) Seconds = 59;
            if (Minutes < 0)
            {
                TimeLeftStop = true;
                Minutes = 0;
                Seconds = 0;
                ShowLevelCleared();
            }
        }
    }

    private IEnumerator UpdateCoroutine()
    {
        while (!TimeLeftStop)
        {
            TimeLeftText.GetComponent<TMP_Text>().text = string.Format("{0:0}:{1:00}", Minutes, Seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
