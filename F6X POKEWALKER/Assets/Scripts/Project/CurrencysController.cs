using System.Text;
using UnityEngine;

public class CurrencysController : MonoBehaviour
{
    private int steps;
    private int watts;
    private int stepsForWatt;
    private int untilNextWatt;

    private void Awake()
    {
        InitializeVariables();
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

    public int GetSteps()
    {
        return steps;
    }

    public int GetWatts()
    {
        return watts;
    }
}