using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class FixingServer : MonoBehaviour
{
    public GameObject loadingBar;
    public ServerModel ServerModel;
    public GameObject HUD;
    public Sprite buttonController;
    public Sprite buttonKeyboard;


    private int speed; 
    private float currentAmount;
    float timestamp = 0.2f;
    private bool m_Interract;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        currentAmount = 0;
    }

    public void IsFinished()
    {
        if(currentAmount >= 100)
        {
            ServerModel.GetLight().color = Color.green;
        }
    }

    public void ResetQTE()
    {
        currentAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.ServerModel != null)
        {
            if (!this.m_Interract)
            {
                this.m_Interract = CrossPlatformInputManager.GetButtonDown("Submit");
            }
        }

        if (this.m_Interract)
        {
            timestamp = Time.time + 0.2f;
            currentAmount += speed;
        }
        if (Time.time >= timestamp && currentAmount >= 0 && currentAmount < 100)
        {
            currentAmount -= (speed * 2) * Time.deltaTime;
        }
        if (currentAmount / 100 != 1)
        {
            loadingBar.transform.GetComponent<Image>().fillAmount = currentAmount / 100;
        }
        this.m_Interract = false;
        this.IsFinished();
    }

    private void FixedUpdate()
    {
        
    }
}
