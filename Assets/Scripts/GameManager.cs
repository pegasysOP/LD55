using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RecipeSelectionPanel recipeSelectionPanel;

    [Header("Recipes")]
    [SerializeField] List<Recipe> recipes = new List<Recipe>();

    private Recipe chosenRecipe;
    private Minigame minigameInstance;

    private void Awake()
    {
        recipeSelectionPanel.OnRecipeChosen += OnRecipeChosen;
    }

    private void Start()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        recipeSelectionPanel.Init(recipes);
        recipeSelectionPanel.gameObject.SetActive(true);
    }

    private void ShowMinigame(Minigame minigame)
    {
        recipeSelectionPanel.gameObject.SetActive(false);

        minigameInstance = Instantiate(minigame);
        minigameInstance.OnMinigameOver += OnMinigameOver;
    }

    private void OnRecipeChosen(object sender, Recipe recipe)
    {
        chosenRecipe = recipe;
        
        if (recipe.Minigame != null)
            ShowMinigame(recipe.Minigame);
        else
            Debug.LogError("There is no minigame for this recipe");
    }

    private void OnMinigameOver(object sender, MinigameScore score)
    {
        minigameInstance.OnMinigameOver -= OnMinigameOver;
        Destroy(minigameInstance.gameObject);

        // TODO: not sure if the correct class instance will be changed or if it's a clone, will need to test once a minigame is in
        foreach (Recipe recipe in recipes)
        {
            if (recipe == chosenRecipe)
            {
                recipe.SetScore(score);
                break;
            }
        }

        Debug.Log($"Recipe [{chosenRecipe.Minigame.name}] score changed to {score}");

        ShowMenu();
    }
}
