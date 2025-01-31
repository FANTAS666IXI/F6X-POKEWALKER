using UnityEngine;

public class TeamController : MonoBehaviour
{
    private int maxObjects;
    private int pokemons;
    private int items;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeVariables();
        InitializeComponents();
        ConsoleLog("Starting Team Controller...", true);
    }

    private void InitializeComponents()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeVariables()
    {
        maxObjects = 3;
        pokemons = PlayerPrefs.GetInt("POKEMONS", 0);
        items = 1;
    }

    public void AddPokemon()
    {
        if (pokemons < maxObjects)
        {
            pokemons++;
            PlayerPrefs.SetInt("POKEMONS", pokemons);
            PlayerPrefs.Save();
        }
    }

    public void SetPokemons(int targetPokemons)
    {
        pokemons = targetPokemons;
        PlayerPrefs.SetInt("POKEMONS", pokemons);
        PlayerPrefs.Save();
    }

    public int GetPokemons()
    {
        return pokemons;
    }

    public void AddItem()
    {
        if (items < maxObjects)
            items++;
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