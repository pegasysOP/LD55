using UnityEngine;

[System.Serializable]
public class Recipe
{
    [SerializeField] private string name;
    [SerializeField] private Minigame minigame;
    [SerializeField] private Sprite icon;
    [SerializeField] private MedalType score;

    public string Name { get { return name; } }
    public Minigame Minigame { get { return minigame; } }
    public Sprite Icon { get { return icon; } }

    public MedalType GetScore()
    {
        return score;
    }

    public void SetScore(MedalType score)
    {
        this.score = score;
    }
}

public enum MedalType
{
    None,
    Bronze,
    Silver,
    Gold,
    Jade
}
