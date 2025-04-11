
public class SelectedSkinChecker : IShopItemVisitor
{
    private readonly IPersistentData _persistentData;

    public bool IsSelected { get; private set; }
    
    public SelectedSkinChecker(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CargoSkinItem cargoSkinItem)
        => IsSelected = _persistentData.PlayerData.SelectedCargoSkin == cargoSkinItem.SkinType;

    public void Visit(TruckSkinItem truckSkinItem)
        => IsSelected = _persistentData.PlayerData.SelectedTruckSkin == truckSkinItem.SkinType;
    
}
