using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemConditions/EnoughMoneyCondition")]
public class EnoughMoneyCondition : ConditionData
{
    public override bool IsConditionMet(ItemButton itemButton)
    {
        Debug.Log($"EnoughMoneyCondition: {itemButton.wallet.money} >= {itemButton.itemData.basePrice}");
        return itemButton.wallet.money >= itemButton.itemData.basePrice;
    }
}
