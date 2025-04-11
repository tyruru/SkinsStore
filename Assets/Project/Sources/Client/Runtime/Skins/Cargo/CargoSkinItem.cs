
using UnityEngine;

[CreateAssetMenu(fileName = "CargoSkinItem", menuName = "Shop/CargoSkinItem")]
public class CargoSkinItem : ShopItem
{
    [field: SerializeField] public CargoSkins SkinType { get; private set; }
}
