using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private int currentScreen = 0;
    private GameObject mainScreen;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
        mainScreen = transform.Find("Main Screen").gameObject;
    }

    private void Start()
    {
        ActiveScreen(currentScreen);
    }

    private void ActiveScreen(int targetScreen)
    {
        switch (targetScreen)
        {
            case 0:
                ConsoleLog("Activating Main Screen...");
                mainScreen.SetActive(true);
                break;
        }
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}