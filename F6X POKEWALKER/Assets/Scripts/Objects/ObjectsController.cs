using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    private int maxObjects;
    private int pokeballs;
    private int items;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        maxObjects = 3;
        pokeballs = 2;
        items = 1;
    }

    public void AddPokeball()
    {
        if (pokeballs < maxObjects)
            pokeballs++;
    }

    public void RestPokeball()
    {
        if (pokeballs > 0)
            pokeballs--;
    }

    public int GetPokeballs()
    {
        return pokeballs;
    }

    public void AddItem()
    {
        if (items < maxObjects)
            items++;
    }

    public void RestItem()
    {
        if (items > 0)
            items--;
    }

    public int GetItems()
    {
        return items;
    }
}