using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<TruckSkinItem> _trackSkinItems;
    [SerializeField] private List<CargoSkinItem> _cargoSkinItems;

    public IEnumerable<TruckSkinItem> TrackSkinItems => _trackSkinItems;
    public IEnumerable<CargoSkinItem> CargoSkinItems => _cargoSkinItems;

    private void OnValidate()
    {
        var trackSkinsDublicates = _trackSkinItems.GroupBy(t => t.SkinType)
            .Where(array => array.Count() > 1);

        if (trackSkinsDublicates.Any())
            throw new InvalidOperationException(nameof(trackSkinsDublicates));
        
        var cargoSkinsDublicates = _cargoSkinItems.GroupBy(c => c.SkinType)
            .Where(array => array.Count() > 1);

        if (cargoSkinsDublicates.Any())
            throw new InvalidOperationException(nameof(cargoSkinsDublicates));
    }
}
