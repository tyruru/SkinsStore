
using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private WalletView _walletView;
    
    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    private Wallet _wallet;

    private void Awake()
    {
        InitializeData();
        InitializeWallet();
        InitializeShop();
    }

    private void InitializeShop()
    {
        OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentData);
        SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentData);
        SkinSelector skinSelector = new SkinSelector(_persistentData);
        SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentData);
        
        _shop.Initialize(_dataProvider, _wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker);
    }

    private void InitializeData()
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);

        LoadDataOrInit();
    }

    private void InitializeWallet()
    {
        _wallet = new Wallet(_persistentData);
        _walletView.Initialize(_wallet);
    }
    
    private void LoadDataOrInit()
    {
        if (!_dataProvider.TryLoad())
            _persistentData.PlayerData = new PlayerData();
    }
}
