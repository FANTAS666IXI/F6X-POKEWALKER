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

    private void InitializeVariables()
    {
        maxObjects = 3;
        pokemons = PlayerPrefs.GetInt("POKEMONS", 0);
        items = PlayerPrefs.GetInt("ITEMS", 0);
    }

    private void InitializeComponents()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    public void AddPokemon()
    {
        if (pokemons < maxObjects)
        {
            pokemons++;
            SavePokemons();
        }
    }

    private void SavePokemons()
    {
        PlayerPrefs.SetInt("POKEMONS", pokemons);
        PlayerPrefs.Save();
    }

    public void SetPokemons(int targetPokemons)
    {
        pokemons = targetPokemons;
        SavePokemons();
    }

    public int GetPokemons()
    {
        return pokemons;
    }

    public void AddItem()
    {
        if (items < maxObjects)
        {
            items++;
            SaveItems();
        }
    }

    private void SaveItems()
    {
        PlayerPrefs.SetInt("ITEMS", items);
        PlayerPrefs.Save();
    }

    public void SetItems(int targetItems)
    {
        items = targetItems;
        SaveItems();
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