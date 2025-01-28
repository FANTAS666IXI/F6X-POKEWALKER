using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private ScoreController scoreController;
    private int currentOption;
    private Text title;
    private Text watts;
    private GameObject[] options;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
        title = transform.Find("Header").transform.Find("Center").transform.Find("Title").GetComponent<Text>();
        watts = transform.Find("Watts").transform.Find("Watts Text").GetComponent<Text>();
        InitializeOptions();
    }

    private void InitializeOptions()
    {
        GameObject menu = transform.Find("Menu").gameObject;
        options = new GameObject[6];
        for (int i = 0; i < 6; i++)
            options[i] = menu.transform.GetChild(i).gameObject.transform.Find("Option Arrow").gameObject;
    }

    private void OnEnable()
    {
        InitializeVariables();
        DeactivateOption();
        ActivateOption();
    }

    private void InitializeVariables()
    {
        currentOption = 2;
    }

    private void DeactivateOption()
    {
        options[currentOption].SetActive(false);
    }

    private void ActivateOption()
    {
        switch (currentOption)
        {
            case 0:
                title.text = "#001";
                break;
            case 1:
                title.text = "#002";
                break;
            case 2:
                title.text = "#003";
                break;
            case 3:
                title.text = "#004";
                break;
            case 4:
                title.text = "#005";
                break;
            case 5:
                title.text = "#006";
                break;
        }
        options[currentOption].SetActive(true);
    }

    private void Update()
    {
        LoadWatts();
    }

    private void LoadWatts()
    {
        int currentWatts = scoreController.GetScore() / 20;
        watts.text = (((currentOption + 1) * 10).ToString() + " W / " + FormatWatts(currentWatts) + " W");
    }

    private string FormatWatts(int watts)
    {
        StringBuilder formattedWatts = new();
        string wattsString = watts.ToString();
        int length = wattsString.Length;
        for (int i = 0; i < length; i++)
        {
            if (i > 0 && (length - i) % 3 == 0)
                formattedWatts.Append(".");
            formattedWatts.Append(wattsString[i]);
        }
        return formattedWatts.ToString();
    }

    public void LeftButton()
    {
        if (currentOption > 0)
            ChangeOption(-1);
        else
        {
            DeactivateOption();
            screenController.ChangeScreen(-1);
        }
    }

    public void CenterButton()
    {
        // Button without functionality in this screen.
    }

    public void RightButton()
    {
        if (currentOption < 5)
            ChangeOption(1);
        else
        {
            DeactivateOption();
            screenController.ChangeScreen(1);
        }
    }

    private void ChangeOption(int targetOption)
    {
        DeactivateOption();
        currentOption += targetOption;
        ActivateOption();
    }
}