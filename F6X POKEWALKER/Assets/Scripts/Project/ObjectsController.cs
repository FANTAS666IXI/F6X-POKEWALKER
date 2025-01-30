using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    private int maxObjects;
    private int pokeballs;
    private int items;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeVariables();
        InitializeComponents();
        ConsoleLog("Starting Objects Controller...", true);
    }

    private void InitializeComponents()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeVariables()
    {
        maxObjects = 3;
        pokeballs = 2;
        items = 1;
    }

    public void AddPokeball()
    {
        if (pokeballs < maxObjects)
            pokeballs++;
    }

    public void RestPokeball()
    {
        if (pokeballs > 0)
            pokeballs--;
    }

    public int GetPokeballs()
    {
        return pokeballs;
    }

    public void AddItem()
    {
        if (items < maxObjects)
            items++;
    }

    public void RestItem()
    {
        if (items > 0)
            items--;
    }

    public int GetItems()
    {
        return items;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}