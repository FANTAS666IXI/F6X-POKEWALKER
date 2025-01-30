using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private ButtonsController buttonsController;
    private CurrencysController currencysController;
    private int currentOption;
    private int optionsQuantity;
    private OptionData[] optionsData;
    private Text title;
    private Text watts;
    private GameObject[] options;

    private struct OptionData
    {
        public string Title;
        public int Price;
        public OptionData(string title, int price)
        {
            Title = title;
            Price = price;
        }
    }

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
        optionsQuantity = 6;
        optionsData = new OptionData[]
        {
            new OptionData("LABORATORY", 0),
            new OptionData("TEAM", 0),
            new OptionData("POKEMONS", 10),
            new OptionData("ITEMS", 3),
            new OptionData("SETTINGS", 0),
            new OptionData("INFO", 0)
        };
    }

    private void InitializeOptions()
    {
        GameObject menu = transform.Find("Menu").gameObject;
        options = new GameObject[optionsQuantity];
        for (int i = 0; i < optionsQuantity; i++)
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
        title.text = optionsData[currentOption].Title;
        options[currentOption].SetActive(true);
    }

    private void Update()
    {
        LoadWatts();
    }

    private void LoadWatts()
    {
        watts.text = (optionsData[currentOption].Price.ToString() + " W / " + currencysController.FormatNumber(currencysController.GetWatts()) + " W");
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
        if (currencysController.ExpendWatts(optionsData[currentOption].Price))
            screenController.SelectScreen(currentOption + 2);
    }

    public void RightButton()
    {
        if (currentOption < optionsQuantity - 1)
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