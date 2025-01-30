using System.Text;
using UnityEngine;

public class CurrencysController : MonoBehaviour
{
    private int steps;
    private int watts;
    private int stepsForWatt;
    private int untilNextWatt;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeVariables();
        InitializeComponents();
        ConsoleLog("Starting Currencys Controller...", true);
    }

    private void InitializeComponents()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeVariables()
    {
        steps = PlayerPrefs.GetInt("STEPS", 0);
        watts = PlayerPrefs.GetInt("WATTS", 0);
        stepsForWatt = 5;
        untilNextWatt = stepsForWatt;
    }

    public void AddCurrencys()
    {
        AddStep();
        AddProgressWatt();
    }

    private void AddStep()
    {
        if (steps < 999999)
        {
            steps++;
            PlayerPrefs.SetInt("STEPS", steps);
            PlayerPrefs.Save();
        }
    }

    private void AddProgressWatt()
    {
        if (watts < 999999)
            untilNextWatt--;
        if (untilNextWatt == 0)
        {
            untilNextWatt = stepsForWatt;
            watts++;
            PlayerPrefs.SetInt("WATTS", watts);
            PlayerPrefs.Save();
        }
    }

    public string FormatNumber(int number)
    {
        StringBuilder formattedNumber = new();
        string numberString = number.ToString();
        int length = numberString.Length;
        for (int i = 0; i < length; i++)
        {
            if (i > 0 && (length - i) % 3 == 0)
                formattedNumber.Append(".");
            formattedNumber.Append(numberString[i]);
        }
        return formattedNumber.ToString();
    }

    public void SetSteps(int newSteps)
    {
        steps = newSteps;
        PlayerPrefs.SetInt("STEPS", steps);
        PlayerPrefs.Save();
    }

    public void SetWatts(int newWatts)
    {
        watts = newWatts;
        PlayerPrefs.SetInt("WATTS", watts);
        PlayerPrefs.Save();
    }

    public bool ExpendWatts(int wattsExpended)
    {
        if (wattsExpended <= watts)
        {
            watts -= wattsExpended;
            PlayerPrefs.SetInt("WATTS", watts);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public int GetSteps()
    {
        return steps;
    }

    public int GetWatts()
    {
        return watts;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}