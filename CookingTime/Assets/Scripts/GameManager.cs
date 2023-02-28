using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WaitingForStart,
    DayStarted,
    DayEnded
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private List<Ingredient> availableIngredients = new List<Ingredient>();

    [SerializeField] private GameState gameState;

    public static event Action OnGameStateChanged;

    public static event Action OnWaitingForStartState;

    public static event Action OnDayStartedState;

    public static event Action OnDayEndedState;

    public bool IsIngredientButtonsInteractable { get; set; }

    public override void Awake()
    {
        base.Awake();
        SwitchGameStateTo(GameState.WaitingForStart);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwitchGameStateTo(GameState.DayStarted);
        }
    }

    public GameState GetCurrentGameState()
    {
        return gameState;
    }

    public void SwitchGameStateTo(GameState gameState)
    {
        if (gameState == GetCurrentGameState())
            return;

        this.gameState = gameState;
        OnGameStateChanged?.Invoke();
        UpdateGameState();
    }

    private void UpdateGameState()
    {
        switch (gameState)
        {
            case GameState.WaitingForStart:
                Debug.Log("Waiting for start day");
                OnWaitingForStartState?.Invoke();
                break;

            case GameState.DayStarted:
                Debug.Log("Day started");
                OnDayStartedState?.Invoke();
                break;

            case GameState.DayEnded:
                Debug.Log("Day ended");
                OnDayEndedState?.Invoke();
                break;
        }
    }

    public void AddToAvailableIngredientsList(Ingredient ingredientToAdd)
    {
        availableIngredients.Add(ingredientToAdd);
    }

    public Ingredient GetAvailableIngredientsByType(IngredientType ingredientType)
    {
        return availableIngredients.Find(ingredient => ingredient.GetIngredientType() == ingredientType);
    }

    public List<Ingredient> GetAvailableIngredients()
    {
        return availableIngredients;
    }

    public Ingredient GetRandomAvailableIngredient()
    {
        Ingredient randomIngredient = availableIngredients[UnityEngine.Random.Range(1, availableIngredients.Count - 1)];

        while (randomIngredient.GetIngredientType() == IngredientType.BreadSlice)
        {
            randomIngredient = availableIngredients[UnityEngine.Random.Range(1, availableIngredients.Count - 1)];
        }

        return randomIngredient;
    }
}