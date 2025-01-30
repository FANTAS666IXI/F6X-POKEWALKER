using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private int currentSetting;
    private int settingsQuantity;
    private GameObject[] settings;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeVariables();
        InitializeComponents();
    }

    private void InitializeVariables()
    {
        currentSetting = 0;
        settingsQuantity = 2;
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        InitializeSettings();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeSettings()
    {
        GameObject allSettings = transform.Find("Settings Options").gameObject;
        settings = new GameObject[settingsQuantity];
        for (int i = 0; i < settingsQuantity; i++)
            settings[i] = allSettings.transform.GetChild(i).gameObject.transform.Find("Setting Arrow").gameObject;
    }

    private void OnEnable()
    {
        DeactivateSetting();
        currentSetting = 0;
        ActivateSetting();
    }

    private void DeactivateSetting()
    {
        settings[currentSetting].SetActive(false);
    }

    private void ActivateSetting()
    {
        settings[currentSetting].SetActive(true);
    }

    public void LeftButton()
    {
        ChangeSetting(-1);
    }

    public void CenterButton()
    {
        switch (currentSetting)
        {
            case 0:
                ResetSetting();
                break;
            case 1:
                BackSetting();
                break;
        }
    }

    private void ResetSetting()
    {
        ConsoleLog("Reseting Game...");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackSetting()
    {
        screenController.SelectScreen(0);
    }

    public void RightButton()
    {
        ChangeSetting(1);
    }

    private void ChangeSetting(int targetSetting)
    {
        DeactivateSetting();
        currentSetting += targetSetting;
        CheckCurrentSetting();
        ActivateSetting();
    }

    private void CheckCurrentSetting()
    {
        if (currentSetting < 0)
            currentSetting = settingsQuantity - 1;
        if (currentSetting > settingsQuantity - 1)
            currentSetting = 0;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}