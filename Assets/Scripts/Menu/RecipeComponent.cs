using UnityEngine;
using UnityEngine.UI;

public class RecipeComponent : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void Init(Recipe recipe)
    {
        icon.sprite = recipe.Icon;

        // also set up rating as a separate thing
    }
}
