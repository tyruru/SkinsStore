
using System.Linq;
using System.Security;

public class OpenSkinsChecker : IShopItemVisitor
{
    private readonly IPersistentData _persistentData;
    
    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CargoSkinItem cargoSkinItem)
        => IsOpened = _persistentData.PlayerData.OpenedCargoSkins.Contains(cargoSkinItem.SkinType);
    
    public void Visit(TruckSkinItem truckSkinItem)
        => IsOpened = _persistentData.PlayerData.OpenedTrackSkins.Contains(truckSkinItem.SkinType);
}
