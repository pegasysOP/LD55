using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    private Coroutine hoverCoroutine;

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

    private IEnumerator HandleMouseHover()
    {
        // first jiggle
        Sequence sequence = DOTween.Sequence();
        sequence.Join(recipeIcon.transform.DOPunchPosition(new Vector3(0f, 8f, 0f), 1.2f, 9));
        sequence.Join(recipeIcon.transform.DOPunchScale(new Vector3(-0.15f, 0.3f, 0f), 1.2f, 9));
        yield return sequence.WaitForCompletion();

        // then wiggle
        while (true)
        {
            recipeIcon.transform.DOShakeRotation(1f, new Vector3(0f, 0f, 15f), 1, 90f, false, ShakeRandomnessMode.Harmonic);
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);
        
        hoverCoroutine = StartCoroutine(HandleMouseHover());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);
    }
}
