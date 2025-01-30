using UnityEngine;

public class PedometerController : MonoBehaviour
{
    private CurrencysController currencysController;
    private float stepThreshold;
    private float resetTime;
    private float lastStepTime;
    private Vector3 previousAcceleration;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeComponents();
        InitializeVariables();
        ConsoleLog("Starting Pedometer Controller...", true);
    }

    private void InitializeComponents()
    {
        currencysController = GameObject.FindGameObjectWithTag("CurrencysController").GetComponent<CurrencysController>();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeVariables()
    {
        stepThreshold = 1.2f;
        resetTime = 0.5f;
        previousAcceleration = Input.acceleration;
        lastStepTime = Time.time;
    }

    private void Update()
    {
        CountSteps();
    }

    private void CountSteps()
    {
        Vector3 acceleration = Input.acceleration;
        float accelerationChange = (acceleration - previousAcceleration).magnitude;
        if (accelerationChange > stepThreshold && (Time.time - lastStepTime) > resetTime)
        {
            currencysController.AddCurrencys();
            lastStepTime = Time.time;
        }
        previousAcceleration = acceleration;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}
