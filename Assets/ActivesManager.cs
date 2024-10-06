using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ActivesManager : MonoBehaviour
{
    public Image restImage;
    public Image trainImage;

    private Tween restTween;
    private Tween trainTween;

    public void StartRestFade(float duration)
    {
        restImage.color = Color.white;
        
        // Kill the existing tween to ensure it restarts fresh
        if (restTween != null && restTween.IsActive())
        {
            restTween.Kill();
        }

        // Start the new fade-in and store the tween reference
        restTween = restImage.DOFade(0f, duration + 2);
    }

    public void StartTrainFade(float duration)
    {
        trainImage.color = Color.white;
        // Kill the existing tween to ensure it restarts fresh
        if (trainTween != null && trainTween.IsActive())
        {
            trainTween.Kill();
        }

        // Start the new fade-in and store the tween reference
        trainTween = trainImage.DOFade(0f, duration + 2);
    }
}
