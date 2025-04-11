
using UnityEngine;

[CreateAssetMenu(fileName = "TrackSkinItem", menuName = "Shop/TrackSkinItem")]
public class TruckSkinItem : ShopItem
{
    [field: SerializeField] public TruckSkins SkinType { get; private set; }
}
