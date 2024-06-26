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
    [SerializeField] private Image medalIcon;

    [Header("Ratings")]
    [SerializeField] private Sprite bronzeImage;
    [SerializeField] private Sprite silverImage;
    [SerializeField] private Sprite goldImage;
    [SerializeField] private Sprite jadeImage;

    /// <summary>
    /// Called when a recipe component is clicked
    /// </summary>
    public event EventHandler<Recipe> OnClick;
    /// <summary>
    /// Called when the mouse goes over a recipe component
    /// </summary>
    public event EventHandler<Recipe> OnHover;

    private Recipe recipe;
    private bool unlocked;

    private Coroutine hoverCoroutine;
    private Sequence jiggleSequence;

    public void Init(Recipe recipe, bool unlocked)
    {
        this.recipe = recipe;
        this.unlocked = unlocked;

        recipeIcon.sprite = recipe.Icon;

        button.onClick.AddListener(OnButtonClick);
        button.interactable = unlocked;

        SetMedalIcon();
    }

    private void SetMedalIcon()
    {
        if (recipe.GetScore() != MedalType.None)
            medalIcon.sprite = GetMedalSprite(recipe.GetScore());
        else
            medalIcon.gameObject.SetActive(false);
    }

    private Sprite GetMedalSprite(MedalType score)
    {
        switch (score)
        {
            case MedalType.Jade:
                return jadeImage;
            case MedalType.Gold: 
                return goldImage;
            case MedalType.Silver:
                return silverImage;
            case MedalType.Bronze:
                return bronzeImage;
            default:
                return null;
        }
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }

    private void OnButtonClick()
    {
        if (unlocked)
        {
            if (recipe != null)
                OnClick.Invoke(this, recipe);
            else
                Debug.LogError("There is no recipe on this component");
        }
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

    private IEnumerator HandleMouseHover()
    {
        // first jiggle
        jiggleSequence = DOTween.Sequence();
        jiggleSequence.Join(recipeIcon.transform.DOPunchPosition(new Vector3(0f, 8f, 0f), 1.2f, 9));
        jiggleSequence.Join(recipeIcon.transform.DOPunchScale(new Vector3(-0.15f, 0.3f, 0f), 1.2f, 9));
        yield return jiggleSequence.WaitForCompletion();

        // then wiggle
        while (true)
        {
            recipeIcon.transform.DOShakeRotation(1.2f, new Vector3(0f, 0f, 15f), 1, 90f, false, ShakeRandomnessMode.Harmonic);
            yield return new WaitForSeconds(1.2f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (recipe != null)
            OnHover.Invoke(this, recipe);
        else
            Debug.LogError("There is no recipe on this component");

        if (unlocked)
        {
            if (hoverCoroutine != null)
            {
                StopCoroutine(hoverCoroutine);

                jiggleSequence.Kill();
                recipeIcon.transform.localPosition = Vector3.zero;
                recipeIcon.transform.localScale = Vector3.one;
                recipeIcon.transform.localRotation = Quaternion.identity;
            }

            hoverCoroutine = StartCoroutine(HandleMouseHover());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);
    }
}
