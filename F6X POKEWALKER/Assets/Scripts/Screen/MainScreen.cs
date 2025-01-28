using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private ObjectsController objectsController;
    private ScoreController scoreController;
    private Text score;
    private GameObject[] pokeballs = new GameObject[3];
    private GameObject[] items = new GameObject[3];

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeComponents();
        InitializeObjects();
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        objectsController = GameObject.FindGameObjectWithTag("ObjectsController").GetComponent<ObjectsController>();
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeObjects()
    {
        InitializePokeballs();
        InitializeItems();
        InitializeScore();
    }

    private void InitializePokeballs()
    {
        for (int i = 0; i < pokeballs.Length; i++)
            pokeballs[i] = transform.Find("Objects Bar").Find($"Pokeball {i + 1}").Find("Pokeball Image").gameObject;
    }

    private void InitializeItems()
    {
        for (int i = 0; i < items.Length; i++)
            items[i] = transform.Find("Objects Bar").Find($"Item {i + 1}").Find("Item Image").gameObject;
    }

    private void InitializeScore()
    {
        score = transform.Find("Score").GetComponent<Text>();
    }

    private void Start()
    {
        LoadObjects();
    }

    public void LoadObjects()
    {
        LoadPokeballs();
        LoadItems();
    }

    public void LoadPokeballs()
    {
        ResetPokeballs();
        for (int i = 0; i < objectsController.GetPokeballs(); i++)
            pokeballs[i].SetActive(true);
    }

    private void ResetPokeballs()
    {
        foreach (var pokeball in pokeballs)
            pokeball.SetActive(false);
    }

    public void LoadItems()
    {
        ResetItems();
        for (int i = 0; i < objectsController.GetItems(); i++)
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
        int currentScore = scoreController.GetScore();
        score.text = FormatScore(currentScore);
    }

    private string FormatScore(int score)
    {
        StringBuilder formattedScore = new();
        string scoreString = score.ToString();
        int length = scoreString.Length;
        for (int i = 0; i < length; i++)
        {
            if (i > 0 && (length - i) % 3 == 0)
                formattedScore.Append(".");
            formattedScore.Append(scoreString[i]);
        }
        return formattedScore.ToString();
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

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}