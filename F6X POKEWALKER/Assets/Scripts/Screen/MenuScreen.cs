using UnityEngine;

public class MenuScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
    }

    public void LeftButton()
    {
        screenController.ChangeScreen(-1);
    }

    public void CenterButton()
    {
        // Button without functionality in this screen.
    }

    public void RightButton()
    {
        screenController.ChangeScreen(1);
    }
}