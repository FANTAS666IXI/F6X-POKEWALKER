using UnityEngine;

public class Buttons : MonoBehaviour
{
    private IScreen[] screens;
    private ScreenController screenController;
    private ScoreController scoreController;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        InitializeScreens();
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
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
        scoreController.AddScore();
        screens[screenController.GetCurrentScreen()].LeftButton();
    }

    public void ButtonCenter()
    {
        scoreController.AddScore();
        screens[screenController.GetCurrentScreen()].CenterButton();
    }

    public void ButtonRight()
    {
        scoreController.AddScore();
        screens[screenController.GetCurrentScreen()].RightButton();
    }
}