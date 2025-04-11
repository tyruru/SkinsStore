 public class SkinSelector : IShopItemVisitor
 {
     private readonly IPersistentData _persistentData;

     public SkinSelector(IPersistentData persistentData)
     {
         _persistentData = persistentData;
     }

     public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

     public void Visit(CargoSkinItem cargoSkinItem)
         => _persistentData.PlayerData.SelectedCargoSkin = cargoSkinItem.SkinType;

     public void Visit(TruckSkinItem truckSkinItem)
         => _persistentData.PlayerData.SelectedTruckSkin = truckSkinItem.SkinType;
 }
