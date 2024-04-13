using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelectionPanel : MonoBehaviour
{
    [SerializeField] private RecipeComponent recipeComponentPrefab;

    private List<RecipeComponent> recipeComponents;


    /// <summary>
    /// Thrown when the minigame is over, gives the score achieved
    /// </summary>
    public event EventHandler<Recipe> OnRecipeChosen;

    public void Init(List<Recipe> recipes)
    {
        ClearRecipeComponents();

        foreach (Recipe recipe in recipes)
            CreateRecipeComponent(recipe);
    }

    private void CreateRecipeComponent(Recipe recipe)
    {
        RecipeComponent component = Instantiate(recipeComponentPrefab, transform);
        component.Init(recipe);
    }

    private void ClearRecipeComponents()
    {
        // delete old components
        if (recipeComponents != null)
        {
            foreach (RecipeComponent recipeComponent in recipeComponents)
                Destroy(recipeComponent);
        }

        recipeComponents = new List<RecipeComponent>();
    }
}
