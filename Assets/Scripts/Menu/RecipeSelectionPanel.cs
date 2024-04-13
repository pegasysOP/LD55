using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelectionPanel : MonoBehaviour
{
    [SerializeField] private RecipeComponent recipeComponentPrefab;

    private List<RecipeComponent> recipeComponents;


    /// <summary>
    /// Called when a recipe component is clicked
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
        component.OnClick += OnRecipeClicked;
    }

    private void OnRecipeClicked(object sender, Recipe recipe)
    {
        // do animations etc.

        OnRecipeChosen(this, recipe);
    }

    private void ClearRecipeComponents()
    {
        // delete old components
        if (recipeComponents != null)
        {
            foreach (RecipeComponent recipeComponent in recipeComponents)
            {
                recipeComponent.OnClick -= OnRecipeClicked;
                Destroy(recipeComponent);
            }
        }

        recipeComponents = new List<RecipeComponent>();
    }
}
