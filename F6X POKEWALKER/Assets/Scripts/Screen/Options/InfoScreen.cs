using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfoScreen : MonoBehaviour, IScreen
{
    // Developement Feature
    private List<string> inputSequence;
    private string[] cheatCode1;
    private string[] cheatCode2;
    // Developement Feature

    private ScreenController screenController;
    private ButtonsController buttonsController;
    private TeamController teamController;
    private CurrencysController currencysController;
    private Text score;
    private Text watts;
    private Text version;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeComponents();
        InitializeCheat();
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        buttonsController = GameObject.FindGameObjectWithTag("ButtonsController").GetComponent<ButtonsController>();
        teamController = GameObject.FindGameObjectWithTag("TeamController").GetComponent<TeamController>();
        currencysController = GameObject.FindGameObjectWithTag("CurrencysController").GetComponent<CurrencysController>();
        score = transform.Find("Score").GetComponent<Text>();
        watts = transform.Find("Watts").GetComponent<Text>();
        version = transform.Find("Version").GetComponent<Text>();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeCheat()
    {
        inputSequence = new List<string>();
        cheatCode1 = new string[] { "LEFT", "RIGHT", "LEFT", "RIGHT", "CENTER" };
        cheatCode2 = new string[] { "LEFT", "LEFT", "RIGHT", "RIGHT", "CENTER" };
    }

    private void Start()
    {
        SetVersionText();
    }

    private void SetVersionText()
    {
        version.text = Application.productName + " : v" + Application.version;
    }

    private void Update()
    {
        SetCurrencysTexts();
    }

    private void SetCurrencysTexts()
    {
        score.text = "STEPS : " + currencysController.FormatNumber(currencysController.GetSteps());
        watts.text = "WATTS : " + currencysController.FormatNumber(currencysController.GetWatts());
    }

    public void LeftButton()
    {
        CheckCheats();
    }

    public void CenterButton()
    {
        CheckCheats();
        screenController.SelectScreen(0);
    }

    public void RightButton()
    {
        CheckCheats();
    }

    private void CheckCheats()
    {
        inputSequence.Add(buttonsController.GetLastButton());
        if (inputSequence.Count > 5)
            inputSequence.RemoveAt(0);
        CheckCheat1();
        CheckCheat2();
    }

    private void CheckCheat1()
    {
        bool activateCheat1 = true;
        if (inputSequence.Count >= cheatCode1.Length)
        {
            for (int i = 0; i < cheatCode1.Length; i++)
            {
                if (inputSequence[i] != cheatCode1[i])
                {
                    activateCheat1 = false;
                    break;
                }
            }
            if (activateCheat1)
            {
                currencysController.SetSteps(999999);
                currencysController.SetWatts(999999);
                ConsoleLog("Cheat 1 Activated!");
            }
        }
    }

    private void CheckCheat2()
    {
        bool activateCheat2 = true;
        if (inputSequence.Count >= cheatCode2.Length)
        {
            for (int i = 0; i < cheatCode2.Length; i++)
            {
                if (inputSequence[i] != cheatCode2[i])
                {
                    activateCheat2 = false;
                    break;
                }
            }
            if (activateCheat2)
            {
                teamController.SetPokemons(3);
                teamController.SetItems(3);
                ConsoleLog("Cheat 2 Activated!");
            }
        }
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}