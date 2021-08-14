using UnityEngine;
using UnityEngine.UI;

public class StanceHUD : MonoBehaviour
{

    [Tooltip("Image component for the stance sprites")]
    public Image StanceImage;

    [Tooltip("Sprite to display when standing")]
    public Sprite StandingSprite;

    [Tooltip("Sprite to display when crouching")]
    public Sprite CrouchingSprite;

    [Tooltip("Sprite to display when crouching")]
    public Sprite SprintingSprite;

    [SerializeField]
    private PlayerController character;

    internal void SetCharacter(PlayerController player)
    {
        this.character = player;
        character.OnStanceChanged += OnStanceChanged;

        OnStanceChanged(character.IsCrouching , character.IsSprinting);
    }

    void OnStanceChanged(bool crouched, bool sprinting)
    {
        StanceImage.sprite = crouched ? CrouchingSprite : sprinting? SprintingSprite : StandingSprite;
    }
}
