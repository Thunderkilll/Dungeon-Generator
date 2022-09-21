using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Item" , menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    private int id;
    [TextArea]
    public string itemName = "placeholder for name ";
    [TextArea]
    public string itemDescription = "placeholder for description";

    public float sellPrice = 0f; // price of this item if we are selling it 
    public float buyPrice = 0f; // price if we buy it from a store

    public enum Type
    {
        Default,
        Consumable,
        Weapon,
        Ammunition,
        Equipment
    }

    public Type type = Type.Default; // all items by default are in default type
}
