using UnityEngine;
using TMPro;

public class CharacterCarousel : MonoBehaviour
{
    public GameObject wizardImage;
    public GameObject knightImage;

    public TMP_Text characterNameText;
    public RectTransform characterNameTextRect;

    public RectTransform arrowButton;

    [Header("Wizard Positions")]
    public Vector2 wizardArrowPosition;
    public Vector2 wizardTextPosition;

    [Header("Knight Positions")]
    public Vector2 knightArrowPosition;
    public Vector2 knightTextPosition;

    private bool showingWizard = true;

    public void NextCharacter()
{
    showingWizard = !showingWizard;

    // Switch character image
    wizardImage.SetActive(showingWizard);
    knightImage.SetActive(!showingWizard);

    // Change character name
    characterNameText.text = showingWizard ? "Wizard" : "Knight";

    // Move arrow and text
    if (showingWizard)
    {
        arrowButton.anchoredPosition = wizardArrowPosition;
        characterNameTextRect.anchoredPosition = wizardTextPosition;

        // Normal arrow: >
        arrowButton.localScale = new Vector3(1, 1, 1);
    }
    else
    {
        arrowButton.anchoredPosition = knightArrowPosition;
        characterNameTextRect.anchoredPosition = knightTextPosition;

        // Flipped arrow: <
        arrowButton.localScale = new Vector3(-1, 1, 1);
    }
}
}