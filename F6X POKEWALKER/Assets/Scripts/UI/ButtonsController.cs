using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    private ScreenController screenController;
    private string lastButton;
    private IScreen[] screens;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeComponents();
        ConsoleLog("Starting Buttons Controller...", true);
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        InitializeScreens();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeScreens()
    {
        Transform screenParent = GameObject.FindGameObjectWithTag("ScreenController").transform;
        int screenCount = screenParent.childCount;
        screens = new IScreen[screenCount];
        for (int i = 0; i < screenCount; i++)
            screens[i] = screenParent.GetChild(i).GetComponent<IScreen>();
    }

    public void ButtonLeft()
    {
        lastButton = "LEFT";
        screens[screenController.GetCurrentScreen()].LeftButton();
    }

    public void ButtonCenter()
    {
        lastButton = "CENTER";
        screens[screenController.GetCurrentScreen()].CenterButton();
    }

    public void ButtonRight()
    {
        lastButton = "RIGHT";
        screens[screenController.GetCurrentScreen()].RightButton();
    }

    public string GetLastButton()
    {
        return lastButton;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}