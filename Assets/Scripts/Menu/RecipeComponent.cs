using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipeComponent : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    /// <summary>
    /// Called when a recipe component is clicked
    /// </summary>
    public event EventHandler<Recipe> OnClick;

    private Recipe recipe;

    public void Init(Recipe recipe)
    {
        this.recipe = recipe;

        icon.sprite = recipe.Icon;

        // also set up rating as a separate thing

        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (recipe != null)
            OnClick.Invoke(this, recipe);
        else
            Debug.LogError("There is no recipe on this component");
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}
