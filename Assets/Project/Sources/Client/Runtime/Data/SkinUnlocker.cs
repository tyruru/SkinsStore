
public class SkinUnlocker : IShopItemVisitor
{
    private readonly IPersistentData _persistentData;

    public SkinUnlocker(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CargoSkinItem cargoSkinItem) 
        => _persistentData.PlayerData.OpenCargoSkin(cargoSkinItem.SkinType);

    public void Visit(TruckSkinItem truckSkinItem) =>
        _persistentData.PlayerData.OpenTrackSkin(truckSkinItem.SkinType);
}
