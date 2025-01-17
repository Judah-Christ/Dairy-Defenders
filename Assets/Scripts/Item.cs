using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Shop/Item", order = 1)]
public class GameItem : ScriptableObject
{
    public string itemName;
    public GameObject itemObject;
    public Sprite itemSprite;
    public Image itemImage;
    public int itemCost;
    public UpgradeLevel upgradeLevel;
    public int Health;
    public Transform itemTransform;
    public int itemUpgradeCost;

}

public enum UpgradeLevel
{
    LVL_ONE,
    LVL_TWO,
    LVL_THREE,
    NONE
     
}