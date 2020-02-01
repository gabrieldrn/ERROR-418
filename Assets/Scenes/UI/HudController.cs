using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AmazingUI;

public class HudController : MonoBehaviour
{

    private int progressFix = 0;
    private int progressDown = 0;
    public GameObject[] ServersFixedToggles;
    public GameObject[] ServersDownToggles;

    public GameObject HudLayout;
    public GameObject GameOverLayout;

    public void ProgressServersFixed()
    {
        if (progressFix < ServersFixedToggles.Length)
        {
            ServersFixedToggles[progressFix++].GetComponent<Toggle>().isOn = true;
        }
        else
        {
            //TODO
        }
    }

    public void ProgressServersDown()
    {
        if (progressDown < ServersDownToggles.Length)
        {
            ServersDownToggles[progressDown++].GetComponent<Toggle>().isOn = true;

            if (progressDown == ServersDownToggles.Length)
            {
                ShowGameOver();
            }
        }
    }

    public void ResetProgress()
    {
        foreach(GameObject toggle in ServersFixedToggles)
        {
            toggle.GetComponent<Toggle>().isOn = false;
        }

        foreach (GameObject toggle in ServersDownToggles)
        {
            toggle.GetComponent<Toggle>().isOn = false;
        }
        progressFix = 0;
        progressDown = 0;
    }

    private void ShowGameOver()
    {
        GameOverLayout.SetActive(true);
    }

    private void Start()
    {
        if(GameOverLayout == null)
        {
            Debug.LogError("GameOverLayout is not set");
        }

        if (HudLayout == null)
        {
            Debug.LogError("HudLayout is not set");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            ProgressServersFixed();
        }

        if (Input.GetKeyDown("p"))
        {
            ProgressServersDown();
        }

        if (Input.GetKeyDown("space"))
        {
            ShowGameOver();
        }
    }
}
