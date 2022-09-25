using HarmonyLib;
using System.Collections.Generic;

namespace CobwebAPI.API;

public class Weapons
{

    [HarmonyPatch(typeof(WeaponManager))]
    public class WeaponManagerPatch
    {
        public static Weapon? EquippedWeapon { get; internal set; }
        [HarmonyPatch("EquipWeapon")]
        [HarmonyPostfix]
        internal static void EquipWeaponPostfix(Weapon weapon)
        {
            EquippedWeapon = weapon;
        }
        [HarmonyPatch("UnEquipWeapon")]
        [HarmonyPostfix]
        internal static void UnEquipWeaponPostfix()
        {
            EquippedWeapon = null;
        }
    }
    public static Weapon? GetEquippedWeapon()
    {
        return WeaponManagerPatch.EquippedWeapon;
    }/*
      * To be done
    public static void SetEquippedWeapon(Weapon weapon)
    {
        WeaponManagerPatch.EquippedWeapon = weapon;
    }*/
    // Add weapon
    public static Weapon CreateWeapon(int ammo, List<Weapon.WeaponType> weaponType, string label)
    {
        Weapon weapon = new()
        {
            maxAmmo = ammo,
            ammo = ammo,
            label = label,
            type = weaponType
        };

        return weapon;
    }

    /*
     * To be done
     
     private List<VersusWeapon> getVersusWeapons()
    {
        bool flag = GameSettings.Instance == null;
        List<VersusWeapon> result;
        if (flag)
        {
            result = null;
        }
        else
        {
            List<VersusWeapon> list = GameSettings.Instance.AvailableVersusWeapons();
            result = list;
        }
        return result;
    }
    
    private void spawnWeapon(int selectedWeapon)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerRigidbody");
        Vector2 a = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        bool flag = !Physics2D.Raycast(a + new Vector2(0f, 10f), gameObject.transform.up, 0.1f, GameController.instance.worldLayers);
        bool flag2 = flag;
        if (flag2)
        {
            Object.Instantiate(getVersusWeapons()[selectedWeapon].weapon, gameObject.transform.position, gameObject.transform.rotation);
        }
    }*/
}
