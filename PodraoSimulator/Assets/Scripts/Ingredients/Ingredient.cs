using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient")]
public class Ingredient : ScriptableObject {
    public string name;
    public int quantity;
    public float price;
}
