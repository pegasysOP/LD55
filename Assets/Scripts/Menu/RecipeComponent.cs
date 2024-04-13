using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipeComponent : MonoBehaviour
{
    [SerializeField] private Image recipeIcon;
    [SerializeField] private Button button;
    [SerializeField] private Image ratingIcon;

    [Header("Ratings")]
    [SerializeField] private Sprite bronzeImage;
    [SerializeField] private Sprite silverImage;
    [SerializeField] private Sprite goldImage;
    [SerializeField] private Sprite jadeImage;

    /// <summary>
    /// Called when a recipe component is clicked
    /// </summary>
    public event EventHandler<Recipe> OnClick;

    private Recipe recipe;

    public void Init(Recipe recipe)
    {
        this.recipe = recipe;

        recipeIcon.sprite = recipe.Icon;

        if (recipe.GetScore() != MinigameScore.None )
            ratingIcon.sprite = GetRatingSprite(recipe.GetScore());
        else
            ratingIcon.gameObject.SetActive(false);

        button.onClick.AddListener(OnButtonClick);
    }

    private Sprite GetRatingSprite(MinigameScore score)
    {
        switch (score)
        {
            case MinigameScore.Jade:
                return jadeImage;
            case MinigameScore.Gold: 
                return goldImage;
            case MinigameScore.Silver:
                return silverImage;
            case MinigameScore.Bronze:
                return bronzeImage;
            default:
                return null;
        }
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
