using UnityEngine;

public class LaboratoryScreen : MonoBehaviour, IScreen
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
        // Button without functionality in this screen.
    }

    public void CenterButton()
    {
        screenController.SelectScreen(0);
    }

    public void RightButton()
    {
        // Button without functionality in this screen.
    }
}