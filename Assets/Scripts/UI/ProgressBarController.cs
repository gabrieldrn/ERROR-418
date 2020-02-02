using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image progress;

    public void SetProgress(float percentage)
    {
        if (percentage > 1f)
            progress.fillAmount = 1f;
        else if (percentage < 0f)
            progress.fillAmount = 0;
        else
            progress.fillAmount = percentage;
    }

    public void AddProgress(float progress)
    {
        SetProgress(this.progress.fillAmount + progress);
    }

    public float GetProgress()
    {
        return progress.fillAmount;
    }
}
