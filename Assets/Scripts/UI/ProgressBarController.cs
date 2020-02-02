using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image progress;

    public void SetProgress(float percentage)
    {
        progress.fillAmount = percentage;
    }
}
