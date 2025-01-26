using UnityEngine;

public class Buttons : MonoBehaviour
{
    private bool objectsSelected;

    private ObjectsController objectsController;
    private ScoreController scoreController;
    public MainScreen mainScreen;

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
        objectsSelected = true;
    }

    private void InitializeComponents()
    {
        objectsController = GameObject.FindGameObjectWithTag("ObjectsController").GetComponent<ObjectsController>();
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    public void ButtonLeft()
    {
        ConsoleLog("Button Left Pushed!");
        UpdateScore();
        if (objectsSelected)
        {
            objectsController.RestPokeball();
            mainScreen.LoadPokeballs();
        }
        else
        {
            objectsController.RestItem();
            mainScreen.LoadItems();
        }
    }

    public void ButtonCenter()
    {
        ConsoleLog("Button Center Pushed!");
        UpdateScore();
        objectsSelected = !objectsSelected;
    }

    public void ButtonRight()
    {
        ConsoleLog("Button Right Pushed!");
        UpdateScore();
        if (objectsSelected)
        {
            objectsController.AddPokeball();
            mainScreen.LoadPokeballs();
        }
        else
        {
            objectsController.AddItem();
            mainScreen.LoadItems();
        }
    }

    private void UpdateScore()
    {
        scoreController.AddScore();
        mainScreen.LoadScore();
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}