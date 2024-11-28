using UnityEngine;
using UnityEngine.UI;

public class CardLayoutManager : LayoutGroup
{
    public int rows = 2;
    public int columns = 2;

    [Space]
    public int paddingTop = 20;
    public Vector2 spacing = new(40, 40);
    public Vector2 cardSize;

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        LayoutChildren();
    }

    public override void SetLayoutVertical()
    {
        LayoutChildren();
    }

    private void LayoutChildren()
    {
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cardHeight = (parentHeight - 2 * paddingTop - spacing.y * (rows - 1)) / rows;
        float cardWidth = cardHeight;

        if (cardWidth * columns + spacing.x * (columns - 1) > parentWidth)
        {
            cardWidth = (parentWidth - 2 * paddingTop - spacing.x * (columns - 1)) / columns;
            cardHeight = cardWidth;
        }

        cardSize = new Vector2(cardWidth, cardHeight);
        padding.left = Mathf.FloorToInt((parentWidth - columns * cardWidth - spacing.x * (columns - 1)) / 2);
        padding.top = Mathf.FloorToInt((parentHeight - rows * cardHeight - spacing.y * (rows - 1)) / 2);
        padding.bottom = padding.top;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            int rowCount = i / columns;
            int columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = padding.left + cardSize.x * columnCount + spacing.x * (columnCount);
            var yPos = padding.top + cardSize.y * rowCount + spacing.y * (rowCount);

            SetChildAlongAxis(item, 0, xPos, cardSize.x);
            SetChildAlongAxis(item, 1, yPos, cardSize.y);
        }
    }
}
