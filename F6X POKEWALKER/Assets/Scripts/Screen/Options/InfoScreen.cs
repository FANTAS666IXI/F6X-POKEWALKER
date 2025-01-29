using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfoScreen : MonoBehaviour, IScreen
{
    // Developement Feature
    private List<string> inputSequence;
    private string[] cheatCode;

    private ScreenController screenController;
    private ButtonsController buttonsController;
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
        currencysController = GameObject.FindGameObjectWithTag("CurrencysController").GetComponent<CurrencysController>();
        score = transform.Find("Score").GetComponent<Text>();
        watts = transform.Find("Watts").GetComponent<Text>();
        version = transform.Find("Version").GetComponent<Text>();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeCheat()
    {
        inputSequence = new List<string>();
        cheatCode = new string[] { "LEFT", "RIGHT", "LEFT", "RIGHT", "CENTER" };
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
        CheckCheat();
    }

    public void CenterButton()
    {
        CheckCheat();
        screenController.SelectScreen(0);
    }

    public void RightButton()
    {
        CheckCheat();
    }

    private void CheckCheat()
    {
        if (inputSequence.Count >= 5)
            inputSequence.RemoveAt(0);
        inputSequence.Add(buttonsController.GetLastButton());
        bool activateCheat = true;
        if (inputSequence.Count == 5)
        {
            for (int i = 0; i < cheatCode.Length; i++)
            {
                if (inputSequence[i] != cheatCode[i])
                {
                    activateCheat = false;
                    break;
                }
            }
            if (activateCheat)
            {
                currencysController.SetSteps(999999);
                currencysController.SetWatts(999999);
                ConsoleLog("Cheat Activated!");
            }
        }
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}