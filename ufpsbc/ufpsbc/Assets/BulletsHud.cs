using UnityEngine;
using UnityEngine.UI;

public class BulletsHud : MonoBehaviour
{
    [Tooltip("Label displaying weapon ammo")]
    public Text AmmoCounterLabel;

    PlayerShooting m_PlayerGuns;

    void Update()
    {
        AmmoCounterLabel.text = m_PlayerGuns.Bullets.ToString();
    }

    internal void InitBulletsComponent(PlayerShooting gunsScript)
    {
        m_PlayerGuns = gunsScript;
    }
}
