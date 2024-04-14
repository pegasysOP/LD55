using UnityEngine;
using UnityEngine.UI;

public class FurnaceHeatRegion : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] LayoutElement layoutElement;

    public void Init(Color colour, float width)
    {
        image.color = colour;
        layoutElement.preferredWidth = width;
    }
}
