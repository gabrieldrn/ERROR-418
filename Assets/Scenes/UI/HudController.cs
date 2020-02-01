using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{

    private int progressFix = 0;
    private int progressDown = 0;
    public GameObject[] ServersFixedToggles;
    public GameObject[] ServersDownToggles;

    public void ProgressServersFixed()
    {
        if (progressFix >= ServersFixedToggles.Length) return;
        ServersFixedToggles[progressFix++].GetComponent<Toggle>().isOn = true;
    }

    public void ProgressServersDown()
    {
        if (progressDown >= ServersDownToggles.Length) return;
        ServersDownToggles[progressDown++].GetComponent<Toggle>().isOn = true;
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
    }
}
