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
        steps = 0;
        watts = 0;
        stepsForWatt = 10;
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
            steps++;
    }

    private void AddProgressWatt()
    {
        if (watts < 999999)
            untilNextWatt--;
        if (untilNextWatt == 0)
        {
            untilNextWatt = stepsForWatt;
            watts++;
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
    }

    public void SetWatts(int newWatts)
    {
        watts = newWatts;
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