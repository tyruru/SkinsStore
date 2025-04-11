
public interface IShopItemVisitor
{
    void Visit(ShopItem shopItem);
    void Visit(CargoSkinItem cargoSkinItem);
    void Visit(TruckSkinItem truckSkinItem);
}
