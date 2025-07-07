using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private bool hasRed, hasGreen, hasBlue;

    [Header("Dynamic Keycard UI")]
    public Transform keycardLayoutContainer;       // UI layout container
    public GameObject keycardIconPrefab;           // Prefab with an Image component

    [Header("Keycard Sprites")]
    public Sprite redKeySprite;
    public Sprite greenKeySprite;
    public Sprite blueKeySprite;

    public void CollectKeycard(KeyCard.KeycardColor color)
    {
        switch (color)
        {
            case KeyCard.KeycardColor.Red:
                if (!hasRed)
                {
                    hasRed = true;
                    AddKeycardToLayout(redKeySprite);
                }
                break;
            case KeyCard.KeycardColor.Green:
                if (!hasGreen)
                {
                    hasGreen = true;
                    AddKeycardToLayout(greenKeySprite);
                }
                break;
            case KeyCard.KeycardColor.Blue:
                if (!hasBlue)
                {
                    hasBlue = true;
                    AddKeycardToLayout(blueKeySprite);
                }
                break;
        }
    }

    public bool HasKey(KeyCard.KeycardColor color)
    {
        return (color == KeyCard.KeycardColor.Red && hasRed) ||
               (color == KeyCard.KeycardColor.Green && hasGreen) ||
               (color == KeyCard.KeycardColor.Blue && hasBlue);
    }

    private void AddKeycardToLayout(Sprite sprite)
    {
        if (keycardIconPrefab == null || keycardLayoutContainer == null || sprite == null) return;

        GameObject icon = Instantiate(keycardIconPrefab, keycardLayoutContainer);
        Image image = icon.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprite;
        }
    }
}
