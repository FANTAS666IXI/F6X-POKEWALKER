using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private ButtonsController buttonsController;
    private CurrencysController currencysController;
    private int currentOption;
    private string[] optionsTitles;
    private Text title;
    private Text watts;
    private GameObject[] options;

    private void Awake()
    {
        InitializeVariables();
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        buttonsController = GameObject.FindGameObjectWithTag("ButtonsController").GetComponent<ButtonsController>();
        currencysController = GameObject.FindGameObjectWithTag("CurrencysController").GetComponent<CurrencysController>();
        title = transform.Find("Header").transform.Find("Center").transform.Find("Title").GetComponent<Text>();
        watts = transform.Find("Watts").transform.Find("Watts Text").GetComponent<Text>();
        InitializeOptions();
    }

    private void InitializeVariables()
    {
        currentOption = 2;
        optionsTitles = new string[] { "LABORATORY", "TEAM", "POKEMONS", "ITEMS", "SETTINGS", "INFO" };
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
        DeactivateOption();
        ReloadCurrentOption();
        ActivateOption();
    }

    private void DeactivateOption()
    {
        options[currentOption].SetActive(false);
    }

    private void ReloadCurrentOption()
    {
        if (buttonsController.GetLastButton() == "RIGHT")
            currentOption = 0;
        else if (buttonsController.GetLastButton() == "LEFT")
            currentOption = 5;
        else
            currentOption = 2;
    }

    private void ActivateOption()
    {
        title.text = optionsTitles[currentOption];
        options[currentOption].SetActive(true);
    }

    private void Update()
    {
        LoadWatts();
    }

    private void LoadWatts()
    {
        watts.text = (((currentOption + 1) * 10).ToString() + " W / " + currencysController.FormatNumber(currencysController.GetWatts()) + " W");
    }

    public void LeftButton()
    {
        if (currentOption > 0)
            ChangeOption(-1);
        else
            screenController.ChangeScreen(-1);
    }

    public void CenterButton()
    {
        screenController.SelectScreen(currentOption + 2);
    }

    public void RightButton()
    {
        if (currentOption < 5)
            ChangeOption(1);
        else
            screenController.ChangeScreen(1);
    }

    private void ChangeOption(int targetOption)
    {
        DeactivateOption();
        currentOption += targetOption;
        ActivateOption();
    }
}