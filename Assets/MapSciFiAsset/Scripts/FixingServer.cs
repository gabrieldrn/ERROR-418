using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FixingServer : MonoBehaviour
{
    public GameObject loadingBar;
    public ServerModel ServerModel;
    public GameObject HUD;
    public Sprite buttonController;
    public Sprite buttonKeyboard;

    private bool m_Interract;

    // Update is called once per frame
    void Update()
    {
        if(this.ServerModel != null)
        {
            if (!this.m_Interract)
            {
                this.m_Interract = CrossPlatformInputManager.GetButtonDown("Submit");
            }

            if (this.m_Interract)
            {
                float progress = this.ServerModel.getProgress();
                if (progress > 0)
                {
                    Debug.LogWarning("ADD PROGRESS");
                    this.ServerModel.addProgress(1f);
                }
            }
        }
        this.m_Interract = false;
    }
}
