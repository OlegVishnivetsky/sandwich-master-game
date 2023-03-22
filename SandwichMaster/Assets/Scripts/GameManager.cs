using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WaitingForStart,
    DayStarted,
    DayIsGoing,
    DayEnded
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private List<Ingredient> availableIngredients = new List<Ingredient>();

    [SerializeField] private GameState gameState;

    public static event Action OnGameStateChanged;

    public static event Action OnWaitingForStartState;

    public static event Action OnDayStartedState;

    public static event Action OnDayIsGoingState;

    public static event Action OnDayEndedState;

    public bool IsIngredientButtonsInteractable { get; set; }

    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        SwitchGameStateTo(GameState.WaitingForStart);
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        SwitchGameStateTo(GameState.DayStarted);
    }

    public GameState GetCurrentGameState()
    {
        return gameState;
    }

    private void UpdateGameState()
    {
        switch (gameState)
        {
            case GameState.WaitingForStart:
                OnWaitingForStartState?.Invoke();
                break;

            case GameState.DayStarted:
                OnDayStartedState?.Invoke();
                SwitchGameStateTo(GameState.DayIsGoing);
                break;

            case GameState.DayIsGoing:
                OnDayIsGoingState?.Invoke();
                break;

            case GameState.DayEnded:
                OnDayEndedState?.Invoke();
                break;
        }
    }

    public void SwitchGameStateTo(GameState gameState)
    {
        if (gameState == GetCurrentGameState())
            return;

        this.gameState = gameState;
        OnGameStateChanged?.Invoke();
        UpdateGameState();
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