using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private TeamController teamController;
    private CurrencysController currencysController;
    private Text score;
    private GameObject[] pokemons = new GameObject[3];
    private GameObject[] items = new GameObject[3];

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;

    private void Awake()
    {
        InitializeComponents();
        InitializeObjects();
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        teamController = GameObject.FindGameObjectWithTag("TeamController").GetComponent<TeamController>();
        currencysController = GameObject.FindGameObjectWithTag("CurrencysController").GetComponent<CurrencysController>();
        score = transform.Find("Score").GetComponent<Text>();
    }

    private void InitializeObjects()
    {
        InitializePokemons();
        InitializeItems();
    }

    private void InitializePokemons()
    {
        for (int i = 0; i < pokemons.Length; i++)
            pokemons[i] = transform.Find("Objects Bar").Find($"Pokeball {i + 1}").Find("Pokeball Image").gameObject;
    }

    private void InitializeItems()
    {
        for (int i = 0; i < items.Length; i++)
            items[i] = transform.Find("Objects Bar").Find($"Item {i + 1}").Find("Item Image").gameObject;
    }

    private void OnEnable()
    {
        LoadObjects();
    }

    public void LoadObjects()
    {
        LoadPokemons();
        LoadItems();
    }

    public void LoadPokemons()
    {
        ResetPokemons();
        for (int i = 0; i < teamController.GetPokemons(); i++)
            pokemons[i].SetActive(true);
    }

    private void ResetPokemons()
    {
        foreach (var pokemon in pokemons)
            pokemon.SetActive(false);
    }

    public void LoadItems()
    {
        ResetItems();
        for (int i = 0; i < teamController.GetItems(); i++)
            items[i].SetActive(true);
    }

    private void ResetItems()
    {
        foreach (var item in items)
            item.SetActive(false);
    }

    private void Update()
    {
        LoadScore();
    }

    private void LoadScore()
    {
        int currentScore = currencysController.GetSteps();
        score.text = currencysController.FormatNumber(currentScore);
    }

    public void LeftButton()
    {
        screenController.ChangeScreen(-1);
    }

    public void CenterButton()
    {
        screenController.ChangeScreen(1);
    }

    public void RightButton()
    {
        screenController.ChangeScreen(1);
    }
}