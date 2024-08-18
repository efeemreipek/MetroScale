using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static Action<int> OnDayPassed;

    [SerializeField] private float dayThreshold = 15f;
    [SerializeField] private float speedMultiplier = 1f;

    private float dayTimer = 0f;
    private int dayCount = 0;
    private bool isGameStopped = false;
    private bool isGameSpedUp = false;
    private bool isGameSlowedDown = false;
    private bool isGameNormal = true;
    private bool isGameStarted = false;
    private int screenshotIndex = 0;

    private DayManagerUI dayManagerUI;

    private void Awake()
    {
        dayManagerUI = GetComponent<DayManagerUI>();
    }
    private void Start()
    {
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.NormalImage, isGameNormal);
    }
    private void Update()
    {
        if (!isGameStarted) return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            ScreenCapture.CaptureScreenshot($"screenshot{screenshotIndex++}.png");
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            PauseTime();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !isGameSpedUp)
        {
            SpeedUpTime();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isGameSlowedDown)
        {
            SlowDownTime();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isGameNormal)
        {
            NormalTime();
        }

        dayTimer += Time.deltaTime * speedMultiplier;

        if(dayTimer >= dayThreshold)
        {
            dayTimer = 0f;
            OnDayPassed?.Invoke(++dayCount);
        }
    }
    public void PauseTime()
    {
        isGameStopped = !isGameStopped;

        if (isGameStopped)
        {
            speedMultiplier = 0f;
        }
        else if(isGameSpedUp)
        {
            speedMultiplier = 2f;
        }
        else if (isGameSlowedDown)
        {
            speedMultiplier = 0.5f;
        }
        else
        {
            speedMultiplier = 1f;
        }

        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.PauseImage, isGameStopped);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SpeedUpImage, isGameSpedUp);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SlowDownImage, isGameSlowedDown);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.NormalImage, isGameNormal);
    }
    public void PauseTimeButton()
    {
        AudioManager.Instance.CreateAudioGO(AudioManager.Instance.ButtonClickAudioPrefab, 0.5f);
        PauseTime();
    }
    public void SpeedUpTime()
    {
        isGameSpedUp = true;
        isGameSlowedDown = false;
        isGameNormal = false;

        speedMultiplier = 2f;

        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SpeedUpImage, isGameSpedUp);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SlowDownImage, isGameSlowedDown);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.NormalImage, isGameNormal);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.PauseImage, isGameStopped);
    }
    public void SpeedUpTimeButton()
    {
        AudioManager.Instance.CreateAudioGO(AudioManager.Instance.ButtonClickAudioPrefab, 0.5f);
        SpeedUpTime();
    }
    public void SlowDownTime()
    {
        isGameSlowedDown = true;
        isGameSpedUp = false;
        isGameNormal = false;

        speedMultiplier = 0.5f;

        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SlowDownImage, isGameSlowedDown);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SpeedUpImage, isGameSpedUp);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.NormalImage, isGameNormal);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.PauseImage, isGameStopped);
    }
    public void SlowDownTimeButton()
    {
        AudioManager.Instance.CreateAudioGO(AudioManager.Instance.ButtonClickAudioPrefab, 0.5f);
        SlowDownTime();
    }
    public void NormalTime()
    {
        isGameNormal = true;
        isGameSpedUp = false;
        isGameSlowedDown = false;

        speedMultiplier = 1f;

        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.NormalImage, isGameNormal);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SlowDownImage, isGameSlowedDown);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.SpeedUpImage, isGameSpedUp);
        dayManagerUI.ActivateOrDeactivateImage(dayManagerUI.PauseImage, isGameStopped);
    }
    public void NormalTimeButton()
    {
        AudioManager.Instance.CreateAudioGO(AudioManager.Instance.ButtonClickAudioPrefab, 0.5f);
        NormalTime();
    }
    public float GetDayTimer() => dayTimer;
    public float GetDayThreshold() => dayThreshold;
    public void ChangeIsGameStarted(bool cond) => isGameStarted = cond;
    public bool GetIsGameStarted() => isGameStarted;
}
