using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    private ScreenController screenController;
    private CurrencysController currencysController;
    private string lastButton;
    private IScreen[] screens;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        InitializeScreens();
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        currencysController = GameObject.FindGameObjectWithTag("CurrencysController").GetComponent<CurrencysController>();
    }

    private void InitializeScreens()
    {
        Transform screenParent = GameObject.FindGameObjectWithTag("ScreenController").transform;
        int screenCount = screenParent.childCount;
        screens = new IScreen[screenCount];
        for (int i = 0; i < screenCount; i++)
            screens[i] = screenParent.GetChild(i).GetComponent<IScreen>();
    }

    public void ButtonLeft()
    {
        lastButton = "LEFT";
        screens[screenController.GetCurrentScreen()].LeftButton();
    }

    public void ButtonCenter()
    {
        lastButton = "CENTER";
        screens[screenController.GetCurrentScreen()].CenterButton();
    }

    public void ButtonRight()
    {
        lastButton = "RIGHT";
        screens[screenController.GetCurrentScreen()].RightButton();
    }

    public string GetLastButton()
    {
        return lastButton;
    }
}