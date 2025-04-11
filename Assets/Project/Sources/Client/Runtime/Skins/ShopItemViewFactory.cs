using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _trackSkinItemPrefab;
    [SerializeField] private ShopItemView _cargoSkinItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_cargoSkinItemPrefab, _trackSkinItemPrefab);
        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Initialize(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _cargoSkinPrefab;
        private ShopItemView _trackSkinPrefab;

        public ShopItemVisitor(ShopItemView trackSkinPrefab, ShopItemView cargoSkinPrefab)
        {
            _trackSkinPrefab = trackSkinPrefab;
            _cargoSkinPrefab = cargoSkinPrefab;
        }

        public ShopItemView Prefab { get; private set; }
        
        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CargoSkinItem cargoSkinItem) => Prefab = _cargoSkinPrefab;

        public void Visit(TruckSkinItem truckSkinItem) => Prefab = _trackSkinPrefab;
    }
}
