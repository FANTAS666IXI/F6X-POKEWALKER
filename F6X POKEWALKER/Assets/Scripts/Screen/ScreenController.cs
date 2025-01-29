using UnityEngine;

public class ScreenController : MonoBehaviour
{
    private int currentScreen;
    private int mainScreensQuantity;
    private GameObject[] screens;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeComponents();
        InitializeObjects();
        InitializeVariables();
    }

    private void InitializeComponents()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeObjects()
    {
        int screensQuantity = transform.childCount;
        screens = new GameObject[screensQuantity];
        for (int i = 0; i < screensQuantity; i++)
            screens[i] = transform.GetChild(i).gameObject;
    }

    private void InitializeVariables()
    {
        currentScreen = 0;
        mainScreensQuantity = 2;
    }

    private void Start()
    {
        ActivateScreen();
    }

    private void ActivateScreen()
    {
        screens[currentScreen].SetActive(true);
        ConsoleLog("Activating screen: " + currentScreen.ToString());
    }

    public void ChangeScreen(int targetScreen)
    {
        DeactivateScreen();
        currentScreen = (currentScreen + targetScreen + mainScreensQuantity) % mainScreensQuantity;
        ActivateScreen();
    }

    private void DeactivateScreen()
    {
        screens[currentScreen].SetActive(false);
    }

    public void SelectScreen(int targetScreen)
    {
        DeactivateScreen();
        currentScreen = targetScreen;
        ActivateScreen();
    }

    public int GetCurrentScreen()
    {
        return currentScreen;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}