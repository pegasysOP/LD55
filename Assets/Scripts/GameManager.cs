using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RecipeSelectionPanel recipeSelectionPanel;

    [Header("Recipes")]
    [SerializeField] List<Recipe> recipes = new List<Recipe>();

    // Start is called before the first frame update
    void Start()
    {
        recipeSelectionPanel.Init(recipes);
    }
}
