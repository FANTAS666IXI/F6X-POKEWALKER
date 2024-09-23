using UnityEngine;

public class Buttons : MonoBehaviour
{
    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    public void ButtonLeft()
    {
        ConsoleLog("Button Left Pushed!");
    }

    public void ButtonCenter()
    {
        ConsoleLog("Button Center Pushed!");
    }

    public void ButtonRight()
    {
        ConsoleLog("Button Right Pushed!");
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}