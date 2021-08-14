using System.Collections.Generic;
using UnityEngine;

public class GunsScript : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();
    private int ActiveWeaponIndex = 0;
    private int BombIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        Weapons.ForEach(item =>
        {
            if (item.CompareTag("Bomb") && BombIndex == -1)
            {
                BombIndex = Weapons.IndexOf(item);
            }
            else if (item.CompareTag("Bomb"))
            {
                Weapons.Remove(item);
                Destroy(item);
            }
            else
            {
                item.SetActive(false);
            }
        });
        Weapons[0].SetActive(true);
    }

    public int getActiveWeaponIndex()
    {
        return ActiveWeaponIndex;
    }

    internal void ChangeActualWeapon(int index)
    {
        if (index < 0)
        {
            index = Weapons.Count - 1;

        }
        else if (index > Weapons.Count - 1)
        {
            index = 0;

        }
        Weapons[ActiveWeaponIndex].SetActive(false);
        ActiveWeaponIndex = index;
        Weapons[ActiveWeaponIndex].SetActive(true);
    }

    internal void ChangeActualWeaponFoward()
    {
        this.ChangeActualWeapon(ActiveWeaponIndex + 1);
    }

    internal void ChangeActualWeaponBackward()
    {
        this.ChangeActualWeapon(ActiveWeaponIndex - 1);
    }

    internal void UseBomb()
    {
        Weapons.RemoveAt(BombIndex);
    }
}
