using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player")]
public class Player : ScriptableObject {
    public float money;

    public void Buy(Ingredient ing) {
        if(money > ing.price) {
            money -= ing.price;
            ing.quantity++;
        }
    }

    public void Sell(Recipe rec) {
        if(rec.checkIng()) {
            rec.makeRecipe();
            money += rec.sellPrice;
        }
    }
}
