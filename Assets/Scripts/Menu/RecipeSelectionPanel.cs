using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeSelectionPanel : MonoBehaviour
{
    [SerializeField] private RecipeComponent recipeComponentPrefab;
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private TextMeshProUGUI topText;

    private List<RecipeComponent> recipeComponents;


    /// <summary>
    /// Called when a recipe component is clicked
    /// </summary>
    public event EventHandler<Recipe> OnRecipeChosen;

    public void Init(List<Recipe> recipes)
    {
        ClearRecipeComponents();

        for (int i = 0; i < recipes.Count; i++)
        {
            bool unlocked = i == 0 || recipes[i - 1].GetScore() != MedalType.None;

            CreateRecipeComponent(recipes[i], unlocked);
        }
    }

    private void CreateRecipeComponent(Recipe recipe, bool unlocked)
    {
        RecipeComponent component = Instantiate(recipeComponentPrefab, recipeContainer);
        component.Init(recipe, unlocked);
        component.OnHover += OnRecipeHover;
        component.OnClick += OnRecipeClicked;

        recipeComponents.Add(component);
    }

    private void OnRecipeHover(object sender, Recipe recipe)
    {
        topText.text = recipe.Name;
    }

    private void OnRecipeClicked(object sender, Recipe recipe)
    {
        // do animations etc.

        OnRecipeChosen(this, recipe);
    }

    private void ClearRecipeComponents()
    {
        // delete old components
        if (recipeComponents != null && recipeComponents.Count > 0)
        {
            foreach (RecipeComponent recipeComponent in recipeComponents)
            {
                recipeComponent.OnHover -= OnRecipeHover;
                recipeComponent.OnClick -= OnRecipeClicked;
                Destroy(recipeComponent.gameObject);
            }
        }

        recipeComponents = new List<RecipeComponent>();
    }
}
