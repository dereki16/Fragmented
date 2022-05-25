using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;

    public TextMeshProUGUI amount;
    public TextMeshProUGUI onSound;
    public TextMeshProUGUI offSound;

    public GameObject onButton;
    public GameObject offButton;

    public static int soundBool = 1;
    public static float volumeLevel = 0.7f;

    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public static int settingDisplay = 7;
    private int screenBool;

    public Toggle fullscreenToggle;
    const string prefName = "optionValue";
    const string resName = "resOption";

    public AudioSource se;

    private void Awake()
    {
        screenBool = PlayerPrefs.GetInt("toggleInt");

        qualityDropdown.value = PlayerPrefs.GetInt("res");
        qualityDropdown.RefreshShownValue();

        if (screenBool == 1)
            fullscreenToggle.isOn = true;
        else if (screenBool == 0)
            fullscreenToggle.isOn = false;

        resolutionDropdown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, resolutionDropdown.value);
            PlayerPrefs.Save();
        }));

        qualityDropdown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(prefName, qualityDropdown.value);
            PlayerPrefs.Save();
        }));
    }

    public void Start()
    {
        ListResolution();
        SoundCheck();

        amount.text = settingDisplay.ToString();
        se.volume = volumeLevel;

        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        audioMixer.SetFloat("vol", PlayerPrefs.GetFloat("volume"));
    }

    // Graphics
    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex == 0)
            PlayerPrefs.SetInt("res", qualityIndex);
        else if (qualityIndex == 1)
            PlayerPrefs.SetInt("res", qualityIndex);
        else if (qualityIndex == 2)
            PlayerPrefs.SetInt("res", qualityIndex);

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("res"));
    }

    // Fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (!isFullscreen)
            PlayerPrefs.SetInt("toggleInt", 0);
        else if (isFullscreen)
            PlayerPrefs.SetInt("toggleInt", 1);
    }

    // Resolution
    public void ListResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int ii = 0; ii < resolutions.Length; ii++)
        {
            string option = resolutions[ii].width + " x " + resolutions[ii].height;
            options.Add(option);

            if (resolutions[ii].width == Screen.currentResolution.width &&
                resolutions[ii].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = ii;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Volume
    public void OnSound()
    {
        offButton.gameObject.SetActive(true);
        onButton.gameObject.SetActive(false);

        soundBool = 0;
        offSound.text = "OFF";
        AudioListener.pause = true;
    }

    public void OffSound()
    {
        offButton.gameObject.SetActive(false);
        onButton.gameObject.SetActive(true);

        soundBool = 1;
        onSound.text = "ON";
        AudioListener.pause = false;
    }

    // Volume buttons
    public void LeftButton()
    {
        if (settingDisplay > 0)
        {
            settingDisplay -= 1;
            amount.text = settingDisplay.ToString();
            volumeLevel = settingDisplay * 0.1f;
            se.volume = volumeLevel;
        }
    }

    public void RightButton()
    {
        if (settingDisplay < 9)
        {
            settingDisplay += 1;
            amount.text = settingDisplay.ToString();
            volumeLevel = settingDisplay * 0.1f;
            se.volume = volumeLevel;
        }
    }

    public void SoundCheck()
    {
        if (soundBool == 1)
            OffSound();
        else if (soundBool == 0)
            OnSound();
    }

    // Volume slider
    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("vol", PlayerPrefs.GetFloat("volume"));
        PlayerPrefs.Save();
    }
}
