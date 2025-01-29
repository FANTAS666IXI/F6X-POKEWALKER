using System.Text;
using UnityEngine;

public class CurrencysController : MonoBehaviour
{
    private int score;
    private int watts;
    private int untilNextWatt;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        score = 0;
        watts = 0;
        untilNextWatt = 20;
    }

    public void AddCurrencys()
    {
        AddScore();
        AddProgressWatt();
    }

    private void AddScore()
    {
        if (score < 999999)
            score++;
    }

    private void AddProgressWatt()
    {
        if (watts < 999999)
            untilNextWatt--;
        if (untilNextWatt == 0)
        {
            untilNextWatt = 20;
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

    public int GetScore()
    {
        return score;
    }

    public int GetWatts()
    {
        return watts;
    }
}