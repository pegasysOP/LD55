using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Recipe
{
    [SerializeField] private Minigame minigame;
    [SerializeField] private Sprite icon;
    [SerializeField] private MinigameScore score;


    public Minigame Minigame { get { return minigame; } }
    public Sprite Icon { get { return icon; } }

    public MinigameScore GetScore()
    {
        return score;
    }

    public void SetScore(MinigameScore score)
    {
        this.score = score;
    }
}
