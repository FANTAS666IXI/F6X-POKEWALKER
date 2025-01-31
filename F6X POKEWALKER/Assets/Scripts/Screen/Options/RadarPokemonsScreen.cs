using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadarPokemonsScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private TeamController teamController;
    private int currentGrass;
    private int grassQuantity;
    private int pokemonGrass;
    private bool searchingPokemon;
    private Text textBox;
    private GameObject[] grassPatchs;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeVariables();
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        teamController = GameObject.FindGameObjectWithTag("TeamController").GetComponent<TeamController>();
        textBox = GameObject.Find("Text Box").transform.Find("Text").GetComponent<Text>();
        InitializeGrassPatchs();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeGrassPatchs()
    {
        GameObject allgrassPatchs = transform.Find("Grass Patchs").gameObject;
        grassPatchs = new GameObject[grassQuantity];
        for (int i = 0; i < grassQuantity; i++)
            grassPatchs[i] = allgrassPatchs.transform.GetChild(i).gameObject.transform.Find("Grass Arrow").gameObject;
    }

    private void InitializeVariables()
    {
        currentGrass = 0;
        grassQuantity = 4;
        pokemonGrass = 0;
        searchingPokemon = true;
    }

    private void OnEnable()
    {
        searchingPokemon = true;
        textBox.text = "Search Pokemon!";
        DeactivateGrass();
        currentGrass = 0;
        ActivateGrass();
        RandomPokemonGrass();
    }

    private void DeactivateGrass()
    {
        grassPatchs[currentGrass].SetActive(false);
    }

    private void ActivateGrass()
    {
        grassPatchs[currentGrass].SetActive(true);
    }

    private void RandomPokemonGrass()
    {
        int randomGrass = Random.Range(0, 4);
        ConsoleLog("Pokemon Spawned In Grass: " + randomGrass);
        pokemonGrass = randomGrass;
    }

    public void LeftButton()
    {
        if (searchingPokemon)
            ChangeGrass(-1);
    }

    public void CenterButton()
    {
        if (searchingPokemon)
        {
            searchingPokemon = false;
            if (currentGrass == pokemonGrass)
            {
                ConsoleLog("Pokemon Found!");
                textBox.text = "Pokemon Found!";
                teamController.AddPokemon();
            }
            else
                textBox.text = "Pokemon Not Found!";
            StartCoroutine(WaitAndExitScreen());
        }
    }

    private IEnumerator WaitAndExitScreen()
    {
        yield return new WaitForSeconds(3);
        screenController.SelectScreen(0);
    }

    public void RightButton()
    {
        if (searchingPokemon)
            ChangeGrass(1);
    }

    private void ChangeGrass(int targetGrass)
    {
        DeactivateGrass();
        currentGrass += targetGrass;
        CheckCurrentGrass();
        ActivateGrass();
    }

    private void CheckCurrentGrass()
    {
        if (currentGrass < 0)
            currentGrass = grassQuantity - 1;
        if (currentGrass > grassQuantity - 1)
            currentGrass = 0;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}