using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadarItemsScreen : MonoBehaviour, IScreen
{
    private ScreenController screenController;
    private TeamController teamController;
    private int currentGrass;
    private int grassQuantity;
    private int itemGrass;
    private int reserchedGrass;
    private int remainingAttempts;
    private bool searchingItem;
    private Text textBox;
    private Image[] iconGrassPatchs;
    private GameObject[] arrowGrassPatchs;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeVariables();
        InitializeComponents();
    }

    private void InitializeVariables()
    {
        currentGrass = 1;
        grassQuantity = 6;
        itemGrass = 0;
        reserchedGrass = -1;
        remainingAttempts = 2;
        searchingItem = true;
    }

    private void InitializeComponents()
    {
        screenController = GameObject.FindGameObjectWithTag("ScreenController").GetComponent<ScreenController>();
        teamController = GameObject.FindGameObjectWithTag("TeamController").GetComponent<TeamController>();
        textBox = GameObject.Find("Text Box").transform.Find("Text").GetComponent<Text>();
        InitializeGrassPatchs();
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void InitializeGrassPatchs()
    {
        GameObject allgrassPatchs = transform.Find("Grass Patchs").gameObject;
        iconGrassPatchs = new Image[grassQuantity];
        for (int i = 0; i < grassQuantity; i++)
            iconGrassPatchs[i] = allgrassPatchs.transform.GetChild(i).gameObject.transform.Find("Grass Icon").gameObject.GetComponent<Image>();
        arrowGrassPatchs = new GameObject[grassQuantity];
        for (int i = 0; i < grassQuantity; i++)
            arrowGrassPatchs[i] = allgrassPatchs.transform.GetChild(i).gameObject.transform.Find("Grass Arrow").gameObject;
    }

    private void OnEnable()
    {
        reserchedGrass = -1;
        remainingAttempts = 2;
        searchingItem = true;
        textBox.text = "Search Item!";
        DeactivateGrass();
        currentGrass = 4;
        ActivateGrass();
        RecolorGrassPatchs();
        RandomItemGrass();
    }

    private void DeactivateGrass()
    {
        arrowGrassPatchs[currentGrass].SetActive(false);
    }

    private void ActivateGrass()
    {
        arrowGrassPatchs[currentGrass].SetActive(true);
    }

    private void RecolorGrassPatchs()
    {
        for (int i = 0; i < grassQuantity; i++)
            iconGrassPatchs[i].color = Color.white;
    }

    private void RandomItemGrass()
    {
        int randomGrass = Random.Range(0, 6);
        ConsoleLog("Item Spawned In Grass: " + randomGrass);
        itemGrass = randomGrass;
    }

    public void LeftButton()
    {
        if (searchingItem)
            ChangeGrass(-1);
    }

    public void CenterButton()
    {
        if (searchingItem)
        {
            searchingItem = false;
            if (currentGrass == itemGrass)
            {
                ConsoleLog("Item Found!");
                textBox.text = "Item Found!";
                teamController.AddItem();
                StartCoroutine(WaitAndExitScreen());
            }
            else
                FinishSearchingAttempt();
        }
    }

    public void RightButton()
    {
        if (searchingItem)
            ChangeGrass(1);
    }

    private void ChangeGrass(int targetGrass)
    {
        int nextGrass = currentGrass + targetGrass;
        nextGrass = RoundNextGrass(nextGrass);
        if (nextGrass == reserchedGrass)
        {
            nextGrass += targetGrass;
            nextGrass = RoundNextGrass(nextGrass);
        }
        DeactivateGrass();
        currentGrass = nextGrass;
        CheckCurrentGrass();
        ActivateGrass();
    }

    private int RoundNextGrass(int nextGrass)
    {
        if (nextGrass < 0)
            nextGrass = grassQuantity - 1;
        if (nextGrass >= grassQuantity)
            nextGrass = 0;
        return nextGrass;
    }

    private void CheckCurrentGrass()
    {
        if (currentGrass < 0)
            currentGrass = grassQuantity - 1;
        if (currentGrass > grassQuantity - 1)
            currentGrass = 0;
    }

    private IEnumerator WaitAndExitScreen()
    {
        yield return new WaitForSeconds(3);
        screenController.SelectScreen(0);
    }

    private void FinishSearchingAttempt()
    {
        remainingAttempts--;
        reserchedGrass = currentGrass;
        textBox.text = "Item Not Found!";
        if (remainingAttempts == 0)
            StartCoroutine(WaitAndExitScreen());
        else
            StartCoroutine(SearchAgain());
    }

    private IEnumerator SearchAgain()
    {
        DeactivateGrass();
        iconGrassPatchs[currentGrass].color = new Color(iconGrassPatchs[currentGrass].color.r, iconGrassPatchs[currentGrass].color.g, iconGrassPatchs[currentGrass].color.b, 200f / 255f);
        yield return new WaitForSeconds(3);
        searchingItem = true;
        bool itemClose = CheckItemClose();
        if (itemClose)
            textBox.text = "Item is close!";
        else
            textBox.text = "Item is far!";
        if (reserchedGrass == 4)
            currentGrass = 1;
        else
            currentGrass = 4;
        ActivateGrass();
    }

    private bool CheckItemClose()
    {
        int leftGrass = currentGrass - 1;
        int rightGrass = currentGrass + 1;
        if (leftGrass < 0)
            leftGrass = grassQuantity - 1;
        if (rightGrass > grassQuantity - 1)
            rightGrass = 0;
        if (leftGrass == itemGrass || rightGrass == itemGrass)
            return true;
        return false;
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}